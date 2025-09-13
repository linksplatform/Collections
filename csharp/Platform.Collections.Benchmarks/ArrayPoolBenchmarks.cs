using System;
using System.Buffers;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Platform.Collections.Arrays;

namespace Platform.Collections.Benchmarks
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob]
    public class ArrayPoolBenchmarks
    {
        private readonly int[] _testSizes = { 16, 64, 256, 1024, 4096, 16384, 65536 };
        private readonly ConfigurableArrayPool<byte> _configurablePool = new();
        
        [Params(16, 64, 256, 1024, 4096, 16384)]
        public int ArraySize { get; set; }

        [Params(1, 4, 8)]
        public int ThreadCount { get; set; }

        [Benchmark(Baseline = true)]
        public byte[] DirectAllocation()
        {
            var array = new byte[ArraySize];
            // Simulate some work
            array[0] = 42;
            return array;
        }

        [Benchmark]
        public byte[] PlatformArrayPool()
        {
            var array = Platform.Collections.Arrays.ArrayPool.Allocate<byte>(ArraySize);
            try
            {
                // Simulate some work
                array[0] = 42;
                return array;
            }
            finally
            {
                Platform.Collections.Arrays.ArrayPool.Free(array);
            }
        }

        [Benchmark]
        public byte[] DotNetArrayPoolShared()
        {
            var array = ArrayPool<byte>.Shared.Rent(ArraySize);
            try
            {
                // Simulate some work
                array[0] = 42;
                return array;
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(array);
            }
        }

        [Benchmark]
        public byte[] ConfigurableArrayPool()
        {
            var array = _configurablePool.Rent(ArraySize);
            try
            {
                // Simulate some work
                array[0] = 42;
                return array;
            }
            finally
            {
                _configurablePool.Return(array);
            }
        }

        [Benchmark]
        public async Task<int> ConcurrentPlatformArrayPool()
        {
            var tasks = new Task<int>[ThreadCount];
            for (int i = 0; i < ThreadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    int operations = 1000;
                    for (int j = 0; j < operations; j++)
                    {
                        var array = Platform.Collections.Arrays.ArrayPool.Allocate<byte>(ArraySize);
                        array[0] = (byte)j;
                        Platform.Collections.Arrays.ArrayPool.Free(array);
                    }
                    return operations;
                });
            }
            
            var results = await Task.WhenAll(tasks);
            int total = 0;
            foreach (var result in results)
            {
                total += result;
            }
            return total;
        }

        [Benchmark]
        public async Task<int> ConcurrentDotNetArrayPool()
        {
            var tasks = new Task<int>[ThreadCount];
            for (int i = 0; i < ThreadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    int operations = 1000;
                    for (int j = 0; j < operations; j++)
                    {
                        var array = ArrayPool<byte>.Shared.Rent(ArraySize);
                        array[0] = (byte)j;
                        ArrayPool<byte>.Shared.Return(array);
                    }
                    return operations;
                });
            }
            
            var results = await Task.WhenAll(tasks);
            int total = 0;
            foreach (var result in results)
            {
                total += result;
            }
            return total;
        }

        [Benchmark]
        public async Task<int> ConcurrentConfigurableArrayPool()
        {
            var tasks = new Task<int>[ThreadCount];
            for (int i = 0; i < ThreadCount; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    int operations = 1000;
                    for (int j = 0; j < operations; j++)
                    {
                        var array = _configurablePool.Rent(ArraySize);
                        array[0] = (byte)j;
                        _configurablePool.Return(array);
                    }
                    return operations;
                });
            }
            
            var results = await Task.WhenAll(tasks);
            int total = 0;
            foreach (var result in results)
            {
                total += result;
            }
            return total;
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            _configurablePool.Dispose();
        }
    }
}