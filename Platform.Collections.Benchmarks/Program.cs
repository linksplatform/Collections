using BenchmarkDotNet.Running;

namespace Platform.Collections.Benchmarks
{
    class Program
    {
        static void Main() => BenchmarkRunner.Run<BitStringBenchmarks>();
    }
}
