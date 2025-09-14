using BenchmarkDotNet.Running;
using System;

namespace Platform.Collections.Benchmarks
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "intersection")
            {
                BenchmarkRunner.Run<IntersectionPerformanceComparison>();
            }
            else if (args.Length > 0 && args[0] == "bitstring")
            {
                BenchmarkRunner.Run<BitStringBenchmarks>();
            }
            else
            {
                Console.WriteLine("Usage: dotnet run [intersection|bitstring]");
                Console.WriteLine("  intersection - Run HashSet vs BitString intersection performance comparison");
                Console.WriteLine("  bitstring    - Run BitString operation benchmarks");
                Console.WriteLine("  (no args)    - Show this help");
            }
        }
    }
}
