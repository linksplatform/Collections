using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;

namespace Platform.Collections.Benchmarks
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "arraypool")
            {
                BenchmarkRunner.Run<ArrayPoolBenchmarks>();
            }
            else if (args.Length > 0 && args[0] == "simple")
            {
                BenchmarkRunner.Run<SimpleArrayPoolBenchmarks>();
            }
            else if (args.Length > 0 && args[0] == "bitstring")
            {
                BenchmarkRunner.Run<BitStringBenchmarks>();
            }
            else
            {
                // Run simple array pool benchmark by default
                BenchmarkRunner.Run<SimpleArrayPoolBenchmarks>();
            }
        }
    }
}
