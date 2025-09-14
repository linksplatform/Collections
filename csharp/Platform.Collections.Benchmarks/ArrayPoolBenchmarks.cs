using System;
using System.Buffers;
using BenchmarkDotNet.Attributes;
using Platform.Collections.Arrays;

namespace Platform.Collections.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public class ArrayPoolBenchmarks
    {
        [Params(16, 64, 256, 1024, 4096, 16384)]
        public int ArraySize { get; set; }

        [Params(100, 1000, 10000)]
        public int OperationCount { get; set; }

        [Benchmark(Baseline = true)]
        public void StandardArrayAllocation()
        {
            for (int i = 0; i < OperationCount; i++)
            {
                var array = new int[ArraySize];
                // Simulate some work
                array[0] = i;
                array[ArraySize - 1] = i;
            }
        }

        [Benchmark]
        public void PlatformArrayPool()
        {
            for (int i = 0; i < OperationCount; i++)
            {
                var array = ArrayPool.Allocate<int>(ArraySize);
                try
                {
                    // Simulate some work
                    array[0] = i;
                    array[ArraySize - 1] = i;
                }
                finally
                {
                    ArrayPool.Free(array);
                }
            }
        }

        [Benchmark]
        public void SystemBuffersArrayPool()
        {
            var pool = System.Buffers.ArrayPool<int>.Shared;
            for (int i = 0; i < OperationCount; i++)
            {
                var array = pool.Rent(ArraySize);
                try
                {
                    // Simulate some work
                    array[0] = i;
                    array[ArraySize - 1] = i;
                }
                finally
                {
                    pool.Return(array);
                }
            }
        }

        [Benchmark]
        public void PlatformArrayPoolDisposable()
        {
            var platformPool = new Platform.Collections.Arrays.ArrayPool<int>();
            for (int i = 0; i < OperationCount; i++)
            {
                using var disposable = platformPool.AllocateDisposable(ArraySize);
                int[] array = disposable;  // Implicit conversion
                // Simulate some work
                array[0] = i;
                array[ArraySize - 1] = i;
            }
        }

        [Benchmark]
        public void PlatformArrayPoolReusabilityTest()
        {
            // Test how well the pool reuses arrays of the same size
            var arrays = new int[10][];
            
            for (int cycle = 0; cycle < OperationCount / 10; cycle++)
            {
                // Allocate multiple arrays
                for (int i = 0; i < arrays.Length; i++)
                {
                    arrays[i] = ArrayPool.Allocate<int>(ArraySize);
                    arrays[i][0] = i;
                }
                
                // Free them all
                for (int i = 0; i < arrays.Length; i++)
                {
                    ArrayPool.Free(arrays[i]);
                }
            }
        }

        [Benchmark]
        public void SystemBuffersArrayPoolReusabilityTest()
        {
            // Test how well System.Buffers.ArrayPool reuses arrays
            var pool = System.Buffers.ArrayPool<int>.Shared;
            var arrays = new int[10][];
            
            for (int cycle = 0; cycle < OperationCount / 10; cycle++)
            {
                // Rent multiple arrays
                for (int i = 0; i < arrays.Length; i++)
                {
                    arrays[i] = pool.Rent(ArraySize);
                    arrays[i][0] = i;
                }
                
                // Return them all
                for (int i = 0; i < arrays.Length; i++)
                {
                    pool.Return(arrays[i]);
                }
            }
        }

        [Benchmark]
        public void PlatformArrayPoolInstanceReuse()
        {
            // Test behavior with a single instance
            var pool = new Platform.Collections.Arrays.ArrayPool<int>();
            
            for (int i = 0; i < OperationCount; i++)
            {
                var array1 = pool.Allocate(ArraySize);
                var array2 = pool.Allocate(ArraySize);
                
                array1[0] = i;
                array2[0] = i;
                
                pool.Free(array1);
                pool.Free(array2);
            }
        }

        [Benchmark]
        public void PlatformArrayPoolVaryingSizes()
        {
            // Test performance with varying array sizes to stress the pool's size management
            var sizes = new[] { 16, 64, 256, 1024, 16, 64, 256, 1024 };
            
            for (int i = 0; i < OperationCount; i++)
            {
                var size = sizes[i % sizes.Length];
                var array = ArrayPool.Allocate<int>(size);
                array[0] = i;
                ArrayPool.Free(array);
            }
        }

        [Benchmark]
        public void SystemBuffersArrayPoolVaryingSizes()
        {
            // Test System.Buffers.ArrayPool with varying sizes
            var pool = System.Buffers.ArrayPool<int>.Shared;
            var sizes = new[] { 16, 64, 256, 1024, 16, 64, 256, 1024 };
            
            for (int i = 0; i < OperationCount; i++)
            {
                var size = sizes[i % sizes.Length];
                var array = pool.Rent(size);
                array[0] = i;
                pool.Return(array);
            }
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            // Note: Cannot access ThreadInstance from external assembly as it's internal
            // Individual test instances will be garbage collected
        }
    }
}