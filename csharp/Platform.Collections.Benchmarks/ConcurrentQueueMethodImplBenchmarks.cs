using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Benchmarks
{
    /// <summary>
    /// Benchmark to test the impact of MethodImpl(MethodImplOptions.AggressiveInlining) 
    /// on ConcurrentQueue extension methods performance.
    /// </summary>
    [SimpleJob(invocationCount: 1, warmupCount: 3, targetCount: 5)]
    [MemoryDiagnoser]
    public class ConcurrentQueueMethodImplBenchmarks
    {
        [Params(10, 100, 1000)]
        public int QueueSize { get; set; }

        /// <summary>
        /// Test DequeueAll with MethodImpl(MethodImplOptions.AggressiveInlining)
        /// </summary>
        [Benchmark(Baseline = true)]
        public int DequeueAllWithMethodImpl()
        {
            var queue = new ConcurrentQueue<int>();
            for (int i = 0; i < QueueSize; i++)
            {
                queue.Enqueue(i);
            }
            
            int count = 0;
            foreach (var item in DequeueAllWithInlining(queue))
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// Test DequeueAll without MethodImpl attribute
        /// </summary>
        [Benchmark]
        public int DequeueAllWithoutMethodImpl()
        {
            var queue = new ConcurrentQueue<int>();
            for (int i = 0; i < QueueSize; i++)
            {
                queue.Enqueue(i);
            }
            
            int count = 0;
            foreach (var item in DequeueAllWithoutInlining(queue))
            {
                count++;
            }
            return count;
        }

        /// <summary>
        /// Version WITH MethodImpl(MethodImplOptions.AggressiveInlining)
        /// Copy of the original implementation from ConcurrentQueueExtensions
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<T> DequeueAllWithInlining<T>(ConcurrentQueue<T> queue)
        {
            while (queue.TryDequeue(out T item))
            {
                yield return item;
            }
        }

        /// <summary>
        /// Version WITHOUT MethodImpl attribute for comparison
        /// </summary>
        private static IEnumerable<T> DequeueAllWithoutInlining<T>(ConcurrentQueue<T> queue)
        {
            while (queue.TryDequeue(out T item))
            {
                yield return item;
            }
        }
    }
}