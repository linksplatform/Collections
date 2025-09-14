using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Benchmarks
{
    [SimpleJob]
    [MemoryDiagnoser]
    public class BitsSetIn16BitsBenchmark
    {
        private static readonly byte[][] _bitsSetIn16BitsLookup;
        
        // Test data with various bit patterns
        private readonly ushort[] _testData = new ushort[]
        {
            0x0000, // No bits set
            0x0001, // Single bit
            0x8000, // Single high bit
            0x5555, // Alternating bits
            0xAAAA, // Alternating bits
            0x00FF, // Lower byte set
            0xFF00, // Upper byte set
            0xFFFF, // All bits set
            0x1248, // Sparse bits
            0x7FFE, // Many bits set
        };

        static BitsSetIn16BitsBenchmark()
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

        [Params(1000, 10000, 100000, 1000000)]
        public int IterationCount { get; set; }

        [Benchmark(Baseline = true)]
        public void UsingLookupTable()
        {
            for (int iteration = 0; iteration < IterationCount; iteration++)
            {
                foreach (var value in _testData)
                {
                    var bits = _bitsSetIn16BitsLookup[value];
                    // Use bits to prevent optimization
                    _ = bits.Length;
                }
            }
        }

        [Benchmark]
        public void UsingOnDemandCalculation()
        {
            for (int iteration = 0; iteration < IterationCount; iteration++)
            {
                foreach (var value in _testData)
                {
                    var bits = CalculateBitsOnDemand(value);
                    // Use bits to prevent optimization
                    _ = bits.Length;
                }
            }
        }

        [Benchmark]
        public void UsingOnDemandCalculationOptimized()
        {
            for (int iteration = 0; iteration < IterationCount; iteration++)
            {
                foreach (var value in _testData)
                {
                    var bits = CalculateBitsOnDemandOptimized(value);
                    // Use bits to prevent optimization
                    _ = bits.Length;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] CalculateBitsOnDemand(ushort value)
        {
            // Count set bits first
            int count = System.Numerics.BitOperations.PopCount(value);
            if (count == 0)
                return Array.Empty<byte>();

            var result = new byte[count];
            int index = 0;
            for (byte bitPos = 0; bitPos < 16; bitPos++)
            {
                if ((value & (1 << bitPos)) != 0)
                {
                    result[index++] = bitPos;
                }
            }
            return result;
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

        // Benchmark specifically for GetBits method pattern used in BitString
        [Benchmark]
        public void GetBitsWithLookupTable()
        {
            for (int iteration = 0; iteration < IterationCount; iteration++)
            {
                long word = 0x123456789ABCDEF0L; // Test pattern
                GetBitsLookup(word, out var bits00to15, out var bits16to31, out var bits32to47, out var bits48to63);
                // Use results to prevent optimization
                _ = bits00to15.Length + bits16to31.Length + bits32to47.Length + bits48to63.Length;
            }
        }

        [Benchmark]
        public void GetBitsWithOnDemandCalculation()
        {
            for (int iteration = 0; iteration < IterationCount; iteration++)
            {
                long word = 0x123456789ABCDEF0L; // Test pattern
                GetBitsOnDemand(word, out var bits00to15, out var bits16to31, out var bits32to47, out var bits48to63);
                // Use results to prevent optimization
                _ = bits00to15.Length + bits16to31.Length + bits32to47.Length + bits48to63.Length;
            }
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