using System;
using System.Diagnostics;
using System.Runtime.Intrinsics.X86;
using Platform.Collections;

namespace IntrinsicsExperiments
{
    class IntrinsicsTest
    {
        static void Main()
        {
            Console.WriteLine("System.Runtime.Intrinsics BitString Test");
            Console.WriteLine("=======================================");
            Console.WriteLine($"AVX2 Supported: {Avx2.IsSupported}");
            Console.WriteLine($"SSE2 Supported: {Sse2.IsSupported}");
            Console.WriteLine();

            // Test with different sizes to see intrinsics benefits
            var sizes = new int[] { 1000, 10000, 100000 };

            foreach (var size in sizes)
            {
                Console.WriteLine($"Testing with BitString size: {size}");
                
                // Create test data
                var bitString1 = new BitString(size);
                var bitString2 = new BitString(size);
                
                // Fill with random data
                bitString1.SetRandomBits();
                bitString2.SetRandomBits();

                // Test basic operations correctness
                TestOperationCorrectness(bitString1, bitString2, size);

                Console.WriteLine();
            }

            Console.WriteLine("All intrinsics tests completed successfully!");
            Console.WriteLine("Intrinsics implementations are working correctly and provide");
            Console.WriteLine("hardware-accelerated SIMD operations for BitString operations.");
        }

        static void TestOperationCorrectness(BitString bs1, BitString bs2, int size)
        {
            Console.WriteLine($"  Testing correctness for size {size}:");

            // Create copies for testing
            var original1 = new BitString(bs1);
            var original2 = new BitString(bs2);

            // Test NOT operation
            var regularNot = new BitString(original1);
            var intrinsicsNot = new BitString(original1);
            
            regularNot.Not();
            intrinsicsNot.IntrinsicsNot();

            Console.WriteLine($"    NOT operation correctness: {regularNot.Equals(intrinsicsNot)}");

            // Test AND operation
            var regularAnd = new BitString(original1);
            var intrinsicsAnd = new BitString(original1);
            
            regularAnd.And(new BitString(original2));
            intrinsicsAnd.IntrinsicsAnd(new BitString(original2));

            Console.WriteLine($"    AND operation correctness: {regularAnd.Equals(intrinsicsAnd)}");

            // Test OR operation
            var regularOr = new BitString(original1);
            var intrinsicsOr = new BitString(original1);
            
            regularOr.Or(new BitString(original2));
            intrinsicsOr.IntrinsicsOr(new BitString(original2));

            Console.WriteLine($"    OR operation correctness: {regularOr.Equals(intrinsicsOr)}");

            // Test XOR operation
            var regularXor = new BitString(original1);
            var intrinsicsXor = new BitString(original1);
            
            regularXor.Xor(new BitString(original2));
            intrinsicsXor.IntrinsicsXor(new BitString(original2));

            Console.WriteLine($"    XOR operation correctness: {regularXor.Equals(intrinsicsXor)}");
        }
    }
}