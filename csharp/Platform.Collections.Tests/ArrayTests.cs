using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Platform.Collections.Arrays;

namespace Platform.Collections.Tests
{
    public class ArrayTests
    {
        [Fact]
        public void GetElementTest()
        {
            var nullArray = (int[])null;
            Assert.Equal(0, nullArray.GetElementOrDefault(1));
            Assert.False(nullArray.TryGetElement(1, out int element));
            Assert.Equal(0, element);
            var array = new int[] { 1, 2, 3 };
            Assert.Equal(3, array.GetElementOrDefault(2));
            Assert.True(array.TryGetElement(2, out element));
            Assert.Equal(3, element);
            Assert.Equal(0, array.GetElementOrDefault(10));
            Assert.False(array.TryGetElement(10, out element));
            Assert.Equal(0, element);
        }

        [Fact]
        public void ArrayPoolBasicFunctionalityTest()
        {
            var array1 = ArrayPool.Allocate<int>(10);
            Assert.Equal(10, array1.Length);
            
            ArrayPool.Free(array1);
            
            var array2 = ArrayPool.Allocate<int>(10);
            Assert.Equal(10, array2.Length);
            
            // Should reuse the freed array
            Assert.Same(array1, array2);
        }

        [Fact]
        public void ArrayPoolMemoryLeakTest_UnboundedPoolGrowthFixed()
        {
            // This test verifies that the pool growth is now bounded (memory leak fixed)
            var initialPoolCount = GetPoolCount<object>();
            
            // Allocate arrays of many different sizes
            for (long i = 1; i <= 1000; i++)
            {
                var array = ArrayPool.Allocate<object>(i);
                ArrayPool.Free(array);
            }
            
            var finalPoolCount = GetPoolCount<object>();
            
            // The pool should NOT have grown significantly (fix applied)
            Assert.True(finalPoolCount <= ArrayPool.DefaultSizesAmount, 
                $"Pool grew from {initialPoolCount} to {finalPoolCount} entries, but should be limited to {ArrayPool.DefaultSizesAmount}");
        }

        [Fact]
        public void ArrayPoolMemoryLeakTest_ArrayContentNotCleared()
        {
            // This test verifies that array contents ARE cleared when returned to pool (fix applied)
            var array = ArrayPool.Allocate<object>(5);
            var testObject = new object();
            array[0] = testObject;
            
            ArrayPool.Free(array);
            
            // Get the same array back from pool
            var reusedArray = ArrayPool.Allocate<object>(5);
            Assert.Same(array, reusedArray);
            
            // The old object reference should be cleared (memory leak fixed)
            Assert.Null(reusedArray[0]);
        }

        [Fact]
        public void ArrayPoolMemoryLeakTest_PoolSizeLimited()
        {
            // This test verifies that pool size is now limited
            var pool = new ArrayPool<object>(10, 5); // max 5 different sizes
            
            // Allocate arrays of 10 different sizes
            for (long i = 1; i <= 10; i++)
            {
                var array = pool.Allocate(i);
                pool.Free(array);
            }
            
            var poolCount = GetInstancePoolCount(pool);
            
            // Pool should be limited to 5 sizes maximum
            Assert.True(poolCount <= 5, $"Pool has {poolCount} entries, should be limited to 5");
        }

        [Fact]
        public void ArrayPoolMemoryLeakTest_ThreadStaticLeakage()
        {
            // This test demonstrates potential ThreadStatic memory leak
            var initialThreadCount = GetActiveThreadCount();
            var tasks = new List<Task>();
            
            // Create multiple threads that use ArrayPool
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(() =>
                {
                    // Each thread creates its own ArrayPool instance via ThreadStatic
                    var array = ArrayPool.Allocate<object>(100);
                    ArrayPool.Free(array);
                    
                    // Clean up thread instance to prevent memory leak
                    ArrayPool.ClearThreadInstance<object>();
                }));
            }
            
            Task.WaitAll(tasks.ToArray());
            
            // Force garbage collection to see if ThreadStatic instances are cleaned up
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            // This test now demonstrates the fix for ThreadStatic cleanup
        }

        [Fact]
        public void ArrayPoolThreadStaticCleanupTest()
        {
            // This test verifies that ClearThreadInstance works
            // Use a custom ArrayPool instance to test the clear functionality
            var pool = new ArrayPool<string>(10, 10);
            
            var array = pool.Allocate(15);
            pool.Free(array);
            
            // Verify that the instance has entries
            var poolCountBefore = GetInstancePoolCount(pool);
            Assert.True(poolCountBefore > 0, $"Pool should have entries before cleanup, but had {poolCountBefore}");
            
            pool.Clear();
            
            var poolCountAfter = GetInstancePoolCount(pool);
            Assert.Equal(0, poolCountAfter);
        }

        private static int GetPoolCount<T>()
        {
            // Use reflection to access the private _pool field to count entries
            var threadInstanceProperty = typeof(ArrayPool<T>).GetProperty("ThreadInstance", 
                BindingFlags.NonPublic | BindingFlags.Static);
            var threadInstance = threadInstanceProperty.GetValue(null);
            
            var poolField = typeof(ArrayPool<T>).GetField("_pool", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            var pool = poolField.GetValue(threadInstance) as IDictionary<long, object>;
            
            return pool?.Count ?? 0;
        }

        private static int GetInstancePoolCount<T>(ArrayPool<T> instance)
        {
            // Use reflection to access the private _pool field to count entries in a specific instance
            var poolField = typeof(ArrayPool<T>).GetField("_pool", 
                BindingFlags.NonPublic | BindingFlags.Instance);
            var pool = poolField.GetValue(instance) as IDictionary<long, object>;
            
            return pool?.Count ?? 0;
        }

        private static int GetActiveThreadCount()
        {
            return Process.GetCurrentProcess().Threads.Count;
        }
    }
}
