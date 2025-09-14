using System;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace Platform.Collections.Tests
{
    public class PerformanceComparisonTest
    {
        private readonly ITestOutputHelper _output;

        public PerformanceComparisonTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void BitStringInitializationPerformanceComparison()
        {
            const int iterations = 100;
            const long bitStringLength = 10000;
            
            _output.WriteLine($"Testing BitString initialization performance with {iterations} iterations for length {bitStringLength}");
            
            // Test old method (SetRandomBits)
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                var bitString = new BitString(bitStringLength);
                bitString.SetRandomBits();
            }
            sw.Stop();
            var oldMethodTime = sw.ElapsedMilliseconds;
            _output.WriteLine($"Old method (SetRandomBits): {oldMethodTime} ms");
            
            // Test new method (array constructor)
            sw.Restart();
            for (int i = 0; i < iterations; i++)
            {
                var bitString = BitStringExtensions.CreateWithRandomBits(bitStringLength);
            }
            sw.Stop();
            var newMethodTime = sw.ElapsedMilliseconds;
            _output.WriteLine($"New method (CreateWithRandomBits): {newMethodTime} ms");
            
            if (oldMethodTime > 0 && newMethodTime > 0)
            {
                var improvement = (double)oldMethodTime / newMethodTime;
                _output.WriteLine($"Performance improvement: {improvement:F2}x faster");
                
                // Assert that the new method is faster or at least not significantly slower
                Assert.True(newMethodTime <= oldMethodTime, $"New method should be faster or equal. Old: {oldMethodTime}ms, New: {newMethodTime}ms");
            }
            else
            {
                _output.WriteLine("Performance test completed, but times were too small to measure accurately");
            }
        }
    }
}