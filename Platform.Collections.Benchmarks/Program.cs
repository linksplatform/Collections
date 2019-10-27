using BenchmarkDotNet.Running;

namespace Platform.Collections.Benchmarks
{
    static class Program
    {
        static void Main() => BenchmarkRunner.Run<BitStringBenchmarks>();
    }
}
