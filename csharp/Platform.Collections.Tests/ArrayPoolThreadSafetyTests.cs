using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Platform.Collections.Arrays;
using SystemArrayPool = System.Buffers.ArrayPool<byte>;

namespace Platform.Collections.Tests
{
    public class ArrayPoolThreadSafetyTests
    {
        [Fact]
        public async Task PlatformArrayPool_ConcurrentAccess_IsThreadSafe()
        {
            const int threadCount = 10;
            const int operationsPerThread = 1000;
            const int arraySize = 1024;
            
            var exceptions = new ConcurrentBag<Exception>();
            var tasks = new Task[threadCount];
            
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    try
                    {
                        for (int j = 0; j < operationsPerThread; j++)
                        {
                            var array = Platform.Collections.Arrays.ArrayPool.Allocate<byte>(arraySize);
                            Assert.NotNull(array);
                            Assert.True(array.Length >= arraySize);
                            
                            // Write to array to ensure it's valid
                            array[0] = (byte)(j % 256);
                            array[arraySize - 1] = (byte)(j % 256);
                            
                            Platform.Collections.Arrays.ArrayPool.Free(array);
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                });
            }
            
            await Task.WhenAll(tasks);
            
            Assert.Empty(exceptions);
        }

        [Fact]
        public async Task DotNetArrayPool_ConcurrentAccess_IsThreadSafe()
        {
            const int threadCount = 10;
            const int operationsPerThread = 1000;
            const int arraySize = 1024;
            
            var exceptions = new ConcurrentBag<Exception>();
            var tasks = new Task[threadCount];
            
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    try
                    {
                        for (int j = 0; j < operationsPerThread; j++)
                        {
                            var array = SystemArrayPool.Shared.Rent(arraySize);
                            Assert.NotNull(array);
                            Assert.True(array.Length >= arraySize);
                            
                            // Write to array to ensure it's valid
                            array[0] = (byte)(j % 256);
                            array[arraySize - 1] = (byte)(j % 256);
                            
                            SystemArrayPool.Shared.Return(array);
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                });
            }
            
            await Task.WhenAll(tasks);
            
            Assert.Empty(exceptions);
        }

        [Fact]
        public async Task ConfigurableArrayPool_ConcurrentAccess_IsThreadSafe()
        {
            const int threadCount = 10;
            const int operationsPerThread = 1000;
            const int arraySize = 1024;
            
            using var pool = new ConfigurableArrayPool<byte>();
            var exceptions = new ConcurrentBag<Exception>();
            var tasks = new Task[threadCount];
            
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    try
                    {
                        for (int j = 0; j < operationsPerThread; j++)
                        {
                            var array = pool.Rent(arraySize);
                            Assert.NotNull(array);
                            Assert.True(array.Length >= arraySize);
                            
                            // Write to array to ensure it's valid
                            array[0] = (byte)(j % 256);
                            array[arraySize - 1] = (byte)(j % 256);
                            
                            pool.Return(array);
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                });
            }
            
            await Task.WhenAll(tasks);
            
            Assert.Empty(exceptions);
        }

        [Fact]
        public void PlatformArrayPool_DifferentThreads_GetSeparateInstances()
        {
            const int threadCount = 5;
            var instances = new ConcurrentBag<object>();
            var tasks = new Task[threadCount];
            
            for (int i = 0; i < threadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    // Access the ThreadStatic instance
                    var instance = Platform.Collections.Arrays.ArrayPool<byte>.ThreadInstance;
                    instances.Add(instance);
                });
            }
            
            Task.WaitAll(tasks);
            
            // Verify each thread gets its own instance
            var uniqueInstances = new HashSet<object>(instances);
            Assert.Equal(threadCount, uniqueInstances.Count);
        }

        [Fact]
        public void ArrayPools_MemoryLeakTest_NoExcessiveMemoryGrowth()
        {
            const int iterations = 10000;
            const int arraySize = 1024;
            
            // Warm up
            for (int i = 0; i < 100; i++)
            {
                var warmupArray = Platform.Collections.Arrays.ArrayPool.Allocate<byte>(arraySize);
                Platform.Collections.Arrays.ArrayPool.Free(warmupArray);
            }
            
            // Force GC to get baseline
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            var initialMemory = GC.GetTotalMemory(false);
            
            // Run test
            for (int i = 0; i < iterations; i++)
            {
                var array = Platform.Collections.Arrays.ArrayPool.Allocate<byte>(arraySize);
                array[0] = (byte)(i % 256); // Use the array
                Platform.Collections.Arrays.ArrayPool.Free(array);
            }
            
            // Force GC after test
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            var finalMemory = GC.GetTotalMemory(false);
            var memoryGrowth = finalMemory - initialMemory;
            
            // Memory growth should be reasonable (less than 1MB for this test)
            Assert.True(memoryGrowth < 1024 * 1024, 
                $"Excessive memory growth detected: {memoryGrowth} bytes");
        }

        [Fact]
        public async Task ArrayPools_StressTest_HighConcurrency()
        {
            const int threadCount = 20;
            const int operationsPerThread = 500;
            var random = new System.Random(42);
            var exceptions = new ConcurrentBag<Exception>();
            
            var tasks = new Task[threadCount];
            
            for (int i = 0; i < threadCount; i++)
            {
                int threadId = i;
                tasks[i] = Task.Run(() =>
                {
                    var localRandom = new System.Random(42 + threadId);
                    try
                    {
                        for (int j = 0; j < operationsPerThread; j++)
                        {
                            // Vary array sizes to test different pool buckets
                            int arraySize = 16 << (localRandom.Next(0, 12)); // 16 to 65536
                            
                            var platformArray = Platform.Collections.Arrays.ArrayPool.Allocate<int>(arraySize);
                            var dotnetArray = System.Buffers.ArrayPool<int>.Shared.Rent(arraySize);
                            
                            // Do some work with the arrays
                            platformArray[0] = j;
                            dotnetArray[0] = j;
                            
                            // Return arrays
                            Platform.Collections.Arrays.ArrayPool.Free(platformArray);
                            System.Buffers.ArrayPool<int>.Shared.Return(dotnetArray);
                        }
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                });
            }
            
            await Task.WhenAll(tasks);
            Assert.Empty(exceptions);
        }
    }
}