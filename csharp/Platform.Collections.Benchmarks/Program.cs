using BenchmarkDotNet.Running;

namespace Platform.Collections.Benchmarks
{
    /// <summary>
    /// <para>
    /// Represents the program.
    /// </para>
    /// <para></para>
    /// </summary>
    static class Program
    {
        /// <summary>
        /// <para>Initializing the Benchmark test.</para>
        /// <para>Инициализация Бенчмарк теста.</para>
        /// </summary>
        static void Main() => BenchmarkRunner.Run<BitStringBenchmarks>();
    }
}
