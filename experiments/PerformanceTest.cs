using System;
using System.Diagnostics;
using Platform.Collections;

class Program
{
    static void Main(string[] args)
    {
        const int iterations = 1000;
        const long bitStringLength = 100000;
        
        Console.WriteLine($"Testing BitString initialization performance with {iterations} iterations for length {bitStringLength}");
        
        // Test old method (SetRandomBits)
        var sw = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            var bitString = new BitString(bitStringLength);
            bitString.SetRandomBits();
        }
        sw.Stop();
        var oldMethodTime = sw.ElapsedMilliseconds;
        Console.WriteLine($"Old method (SetRandomBits): {oldMethodTime} ms");
        
        // Test new method (array constructor)
        sw.Restart();
        for (int i = 0; i < iterations; i++)
        {
            var bitString = BitStringExtensions.CreateWithRandomBits(bitStringLength);
        }
        sw.Stop();
        var newMethodTime = sw.ElapsedMilliseconds;
        Console.WriteLine($"New method (CreateWithRandomBits): {newMethodTime} ms");
        
        var improvement = oldMethodTime > 0 ? (double)oldMethodTime / newMethodTime : 1.0;
        Console.WriteLine($"Performance improvement: {improvement:F2}x faster");
    }
}