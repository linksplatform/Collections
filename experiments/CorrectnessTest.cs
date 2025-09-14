using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Experiments
{
    class CorrectnessTest
    {
        private static readonly byte[][] _bitsSetIn16BitsLookup;
        
        static CorrectnessTest()
        {
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
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Testing correctness of lookup table vs on-demand calculation");
            Console.WriteLine("============================================================");

            bool allTestsPassed = true;

            // Test all possible 16-bit values (this might take a moment...)
            Console.WriteLine("Testing all 65,536 possible 16-bit values...");
            
            int errorsFound = 0;
            for (int i = 0; i < 65536; i++)
            {
                ushort value = (ushort)i;
                var lookupResult = _bitsSetIn16BitsLookup[value];
                var calculatedResult = CalculateBitsOnDemandOptimized(value);

                if (!lookupResult.SequenceEqual(calculatedResult))
                {
                    if (errorsFound < 10) // Show first 10 errors
                    {
                        Console.WriteLine($"MISMATCH for value {value:X4}:");
                        Console.WriteLine($"  Lookup:     [{string.Join(", ", lookupResult)}]");
                        Console.WriteLine($"  Calculated: [{string.Join(", ", calculatedResult)}]");
                    }
                    errorsFound++;
                    allTestsPassed = false;
                }

                if (i % 10000 == 0)
                {
                    Console.WriteLine($"  Tested {i + 1:N0}/65,536 values...");
                }
            }

            if (errorsFound > 10)
            {
                Console.WriteLine($"  ... and {errorsFound - 10} more errors");
            }

            Console.WriteLine($"\\nCompleted testing all 65,536 values");
            Console.WriteLine($"Errors found: {errorsFound}");

            // Test specific interesting patterns
            Console.WriteLine("\\nTesting specific bit patterns:");
            TestSpecificPattern(0x0000, "No bits set");
            TestSpecificPattern(0x0001, "Single bit (LSB)");
            TestSpecificPattern(0x8000, "Single bit (MSB)");
            TestSpecificPattern(0x5555, "Alternating bits (0101...)");
            TestSpecificPattern(0xAAAA, "Alternating bits (1010...)");
            TestSpecificPattern(0x00FF, "Lower byte set");
            TestSpecificPattern(0xFF00, "Upper byte set");
            TestSpecificPattern(0xFFFF, "All bits set");

            // Test GetBits method equivalence
            Console.WriteLine("\\nTesting GetBits method equivalence:");
            TestGetBitsEquivalence(0x123456789ABCDEF0L);
            TestGetBitsEquivalence(0x0000000000000000L);
            TestGetBitsEquivalence(unchecked((long)0xFFFFFFFFFFFFFFFF));
            TestGetBitsEquivalence(0x5555555555555555L);

            Console.WriteLine($"\\n=== FINAL RESULT ===");
            if (allTestsPassed && errorsFound == 0)
            {
                Console.WriteLine("✅ ALL TESTS PASSED - Both approaches produce identical results");
            }
            else
            {
                Console.WriteLine("❌ TESTS FAILED - Results differ between approaches");
            }
        }

        static void TestSpecificPattern(ushort value, string description)
        {
            var lookupResult = _bitsSetIn16BitsLookup[value];
            var calculatedResult = CalculateBitsOnDemandOptimized(value);
            
            bool matches = lookupResult.SequenceEqual(calculatedResult);
            string status = matches ? "✅" : "❌";
            
            Console.WriteLine($"  {status} {description} (0x{value:X4}): Lookup=[{string.Join(",", lookupResult)}], Calculated=[{string.Join(",", calculatedResult)}]");
        }

        static void TestGetBitsEquivalence(long testWord)
        {
            // Test lookup approach
            GetBitsLookup(testWord, out var lookup00to15, out var lookup16to31, out var lookup32to47, out var lookup48to63);
            
            // Test on-demand approach
            GetBitsOnDemand(testWord, out var ondemand00to15, out var ondemand16to31, out var ondemand32to47, out var ondemand48to63);

            bool matches = lookup00to15.SequenceEqual(ondemand00to15) &&
                          lookup16to31.SequenceEqual(ondemand16to31) &&
                          lookup32to47.SequenceEqual(ondemand32to47) &&
                          lookup48to63.SequenceEqual(ondemand48to63);

            string status = matches ? "✅" : "❌";
            Console.WriteLine($"  {status} GetBits(0x{testWord:X16})");
            
            if (!matches)
            {
                Console.WriteLine($"    00-15: Lookup=[{string.Join(",", lookup00to15)}], OnDemand=[{string.Join(",", ondemand00to15)}]");
                Console.WriteLine($"    16-31: Lookup=[{string.Join(",", lookup16to31)}], OnDemand=[{string.Join(",", ondemand16to31)}]");
                Console.WriteLine($"    32-47: Lookup=[{string.Join(",", lookup32to47)}], OnDemand=[{string.Join(",", ondemand32to47)}]");
                Console.WriteLine($"    48-63: Lookup=[{string.Join(",", lookup48to63)}], OnDemand=[{string.Join(",", ondemand48to63)}]");
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
            bits00to15 = _bitsSetIn16BitsLookup[(int)(word & 0xffffu)];
            bits16to31 = _bitsSetIn16BitsLookup[(int)((word >> 16) & 0xffffu)];
            bits32to47 = _bitsSetIn16BitsLookup[(int)((word >> 32) & 0xffffu)];
            bits48to63 = _bitsSetIn16BitsLookup[(int)((word >> 48) & 0xffffu)];
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