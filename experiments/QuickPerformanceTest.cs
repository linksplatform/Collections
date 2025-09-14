using System;
using System.Buffers;
using System.Diagnostics;
using Platform.Collections.Arrays;

namespace Platform.Collections.Experiments
{
    class QuickPerformanceTest
    {
        static void Main()
        {
            Console.WriteLine("ArrayPool Performance Test");
            Console.WriteLine("========================");
            
            int iterations = 10000;
            int[] sizes = { 64, 256, 1024 };
            
            foreach (int size in sizes)
            {
                Console.WriteLine($"\nArray Size: {size} elements");
                Console.WriteLine("-------------------");
                
                // Test standard allocation
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < iterations; i++)
                {
                    var array = new int[size];
                    array[0] = i; // Simulate usage
                    array[size - 1] = i;
                }
                sw.Stop();
                Console.WriteLine($"Standard allocation:    {sw.ElapsedMilliseconds:N0} ms");
                
                // Test Platform ArrayPool
                sw.Restart();
                for (int i = 0; i < iterations; i++)
                {
                    var array = ArrayPool.Allocate<int>(size);
                    array[0] = i;
                    array[size - 1] = i;
                    ArrayPool.Free(array);
                }
                sw.Stop();
                Console.WriteLine($"Platform ArrayPool:     {sw.ElapsedMilliseconds:N0} ms");
                
                // Test System.Buffers ArrayPool
                var pool = System.Buffers.ArrayPool<int>.Shared;
                sw.Restart();
                for (int i = 0; i < iterations; i++)
                {
                    var array = pool.Rent(size);
                    array[0] = i;
                    array[size - 1] = i;
                    pool.Return(array);
                }
                sw.Stop();
                Console.WriteLine($"System.Buffers.ArrayPool: {sw.ElapsedMilliseconds:N0} ms");
            }
            
            Console.WriteLine("\nMemory Pressure Test (GC Collections)");
            Console.WriteLine("====================================");
            
            // Force GC and measure collections
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            int gen0Before = GC.CollectionCount(0);
            int gen1Before = GC.CollectionCount(1);
            int gen2Before = GC.CollectionCount(2);
            
            // Heavy allocation test
            for (int i = 0; i < 50000; i++)
            {
                var array = new int[256];
            }
            
            int gen0After = GC.CollectionCount(0);
            int gen1After = GC.CollectionCount(1);
            int gen2After = GC.CollectionCount(2);
            
            Console.WriteLine($"Standard allocation GC - Gen0: {gen0After - gen0Before}, Gen1: {gen1After - gen1Before}, Gen2: {gen2After - gen2Before}");
            
            // Reset counters
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            
            gen0Before = GC.CollectionCount(0);
            gen1Before = GC.CollectionCount(1);
            gen2Before = GC.CollectionCount(2);
            
            // ArrayPool test
            var testPool = System.Buffers.ArrayPool<int>.Shared;
            for (int i = 0; i < 50000; i++)
            {
                var array = testPool.Rent(256);
                testPool.Return(array);
            }
            
            gen0After = GC.CollectionCount(0);
            gen1After = GC.CollectionCount(1);
            gen2After = GC.CollectionCount(2);
            
            Console.WriteLine($"System.Buffers.ArrayPool GC - Gen0: {gen0After - gen0Before}, Gen1: {gen1After - gen1Before}, Gen2: {gen2After - gen2Before}");
        }
    }
}