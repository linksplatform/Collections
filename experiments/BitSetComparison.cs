using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Experiments
{
    class BitSetComparison
    {
        private static readonly byte[][] _bitsSetIn16BitsLookup;
        
        static BitSetComparison()
        {
            Console.WriteLine("Initializing lookup table...");
            var sw = Stopwatch.StartNew();
            
            _bitsSetIn16BitsLookup = new byte[65536][];
            int i, c, k;
            byte bitIndex;
            for (i = 0; i < 65536; i++)
            {
                // Calculating size of array (number of positive bits)
                for (c = 0, k = 1; k <= 65536; k <<= 1)
                {
                    if ((i & k) == k)
                    {
                        c++;
                    }
                }
                var array = new byte[c];
                // Adding positive bits indices into array
                for (bitIndex = 0, c = 0, k = 1; k <= 65536; k <<= 1)
                {
                    if ((i & k) == k)
                    {
                        array[c++] = bitIndex;
                    }
                    bitIndex++;
                }
                _bitsSetIn16BitsLookup[i] = array;
            }
            
            sw.Stop();
            Console.WriteLine($"Lookup table initialization took: {sw.ElapsedMilliseconds}ms");
            
            // Calculate memory usage
            long totalMemory = 0;
            for (int j = 0; j < 65536; j++)
            {
                totalMemory += _bitsSetIn16BitsLookup[j].Length;
            }
            totalMemory += 65536 * IntPtr.Size; // Array references
            Console.WriteLine($"Lookup table memory usage: ~{totalMemory / 1024}KB");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Comparing lookup table vs on-demand calculation for BitsSetIn16Bits");
            Console.WriteLine("================================================================");

            // Test different patterns
            ushort[] testPatterns = {
                0x0000, // No bits
                0x0001, // Single bit
                0x8000, // Single high bit  
                0x5555, // Alternating
                0xAAAA, // Alternating inverse
                0x00FF, // Lower byte
                0xFF00, // Upper byte
                0xFFFF, // All bits
                0x1248, // Sparse
                0x7FFE  // Dense
            };

            const int iterations = 1_000_000;
            
            // Warm up
            for (int i = 0; i < 10000; i++)
            {
                _ = _bitsSetIn16BitsLookup[0x5555];
                _ = CalculateBitsOnDemandOptimized(0x5555);
            }

            // Benchmark lookup table approach
            Console.WriteLine("\\nBenchmarking lookup table approach...");
            var sw = Stopwatch.StartNew();
            for (int iter = 0; iter < iterations; iter++)
            {
                foreach (var pattern in testPatterns)
                {
                    var bits = _bitsSetIn16BitsLookup[pattern];
                    _ = bits.Length; // Use to prevent optimization
                }
            }
            sw.Stop();
            var lookupTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"Lookup table: {lookupTime}ms for {iterations * testPatterns.Length} operations");

            // Benchmark on-demand calculation
            Console.WriteLine("\\nBenchmarking on-demand calculation...");
            sw.Restart();
            for (int iter = 0; iter < iterations; iter++)
            {
                foreach (var pattern in testPatterns)
                {
                    var bits = CalculateBitsOnDemandOptimized(pattern);
                    _ = bits.Length; // Use to prevent optimization
                }
            }
            sw.Stop();
            var onDemandTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"On-demand calc: {onDemandTime}ms for {iterations * testPatterns.Length} operations");

            // Results
            Console.WriteLine("\\n=== RESULTS ===");
            Console.WriteLine($"Lookup table:     {lookupTime}ms");
            Console.WriteLine($"On-demand calc:   {onDemandTime}ms");
            
            if (lookupTime < onDemandTime)
            {
                var speedup = (double)onDemandTime / lookupTime;
                Console.WriteLine($"Lookup table is {speedup:F2}x faster");
            }
            else
            {
                var speedup = (double)lookupTime / onDemandTime;
                Console.WriteLine($"On-demand calculation is {speedup:F2}x faster");
            }

            // Test with realistic BitString GetBits pattern
            Console.WriteLine("\\n=== Testing GetBits Pattern ===");
            TestGetBitsPattern();
        }

        static void TestGetBitsPattern()
        {
            const int iterations = 100_000;
            long testWord = 0x123456789ABCDEF0L;

            // Warm up
            for (int i = 0; i < 1000; i++)
            {
                GetBitsLookup(testWord, out _, out _, out _, out _);
                GetBitsOnDemand(testWord, out _, out _, out _, out _);
            }

            // Benchmark lookup approach
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                GetBitsLookup(testWord, out var bits00to15, out var bits16to31, out var bits32to47, out var bits48to63);
                _ = bits00to15.Length + bits16to31.Length + bits32to47.Length + bits48to63.Length;
            }
            sw.Stop();
            var lookupTime = sw.ElapsedMilliseconds;

            // Benchmark on-demand approach
            sw.Restart();
            for (int i = 0; i < iterations; i++)
            {
                GetBitsOnDemand(testWord, out var bits00to15, out var bits16to31, out var bits32to47, out var bits48to63);
                _ = bits00to15.Length + bits16to31.Length + bits32to47.Length + bits48to63.Length;
            }
            sw.Stop();
            var onDemandTime = sw.ElapsedMilliseconds;

            Console.WriteLine($"GetBits with lookup:     {lookupTime}ms");
            Console.WriteLine($"GetBits with on-demand:  {onDemandTime}ms");
            
            if (lookupTime < onDemandTime)
            {
                var speedup = (double)onDemandTime / lookupTime;
                Console.WriteLine($"GetBits with lookup is {speedup:F2}x faster");
            }
            else
            {
                var speedup = (double)lookupTime / onDemandTime;
                Console.WriteLine($"GetBits with on-demand is {speedup:F2}x faster");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] CalculateBitsOnDemandOptimized(ushort value)
        {
            if (value == 0)
                return Array.Empty<byte>();

            // Count set bits using built-in PopCount
            int count = System.Numerics.BitOperations.PopCount(value);
            var result = new byte[count];
            int index = 0;
            
            // Use bit scanning to find set bits more efficiently
            while (value != 0)
            {
                int bitPos = System.Numerics.BitOperations.TrailingZeroCount(value);
                result[index++] = (byte)bitPos;
                value &= (ushort)(value - 1); // Clear the lowest set bit
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GetBitsLookup(long word, out byte[] bits00to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63)
        {
            bits00to15 = _bitsSetIn16BitsLookup[word & 0xffffu];
            bits16to31 = _bitsSetIn16BitsLookup[(word >> 16) & 0xffffu];
            bits32to47 = _bitsSetIn16BitsLookup[(word >> 32) & 0xffffu];
            bits48to63 = _bitsSetIn16BitsLookup[(word >> 48) & 0xffffu];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GetBitsOnDemand(long word, out byte[] bits00to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63)
        {
            bits00to15 = CalculateBitsOnDemandOptimized((ushort)(word & 0xffffu));
            bits16to31 = CalculateBitsOnDemandOptimized((ushort)((word >> 16) & 0xffffu));
            bits32to47 = CalculateBitsOnDemandOptimized((ushort)((word >> 32) & 0xffffu));
            bits48to63 = CalculateBitsOnDemandOptimized((ushort)((word >> 48) & 0xffffu));
        }
    }
}