using BenchmarkDotNet.Running;

namespace Platform.Collections.Benchmarks
{
    static class Program
    {
        /// <summary>
        /// <para>Initializing the Benchmark test.</para>
        /// <para>Инициализация Бенчмарк теста.</para>
        /// </summary>
        static void Main() => BenchmarkRunner.Run<BitStringBenchmarks>();
    }
}
