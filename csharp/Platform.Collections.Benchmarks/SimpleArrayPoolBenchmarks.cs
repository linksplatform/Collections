using System;
using System.Buffers;
using BenchmarkDotNet.Attributes;
using Platform.Collections.Arrays;

namespace Platform.Collections.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public class SimpleArrayPoolBenchmarks
    {
        [Params(64, 256, 1024)]
        public int ArraySize { get; set; }

        private const int OperationCount = 1000;

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
        public void PlatformArrayPoolWithInstance()
        {
            var platformPool = new Platform.Collections.Arrays.ArrayPool<int>();
            for (int i = 0; i < OperationCount; i++)
            {
                var array = platformPool.Allocate(ArraySize);
                try
                {
                    // Simulate some work
                    array[0] = i;
                    array[ArraySize - 1] = i;
                }
                finally
                {
                    platformPool.Free(array);
                }
            }
        }
    }
}