using System;
using Platform.Collections;

namespace Examples
{
    /// <summary>
    /// This example demonstrates the BitString chaining mechanism that allows multiple operations
    /// to be applied in a single memory pass, improving performance by reducing memory bandwidth usage.
    /// </summary>
    public class BitStringChainExample
    {
        public static void RunExample()
        {
            Console.WriteLine("BitString Chaining Example");
            Console.WriteLine("==========================");

            // Create three BitStrings for demonstration
            var bitString1 = new BitString(1000);
            var bitString2 = new BitString(1000); 
            var bitString3 = new BitString(1000);

            // Fill them with random data
            bitString1.SetRandomBits();
            bitString2.SetRandomBits();
            bitString3.SetRandomBits();

            Console.WriteLine($"BitString1 set bits: {bitString1.CountSetBits()}");
            Console.WriteLine($"BitString2 set bits: {bitString2.CountSetBits()}");
            Console.WriteLine($"BitString3 set bits: {bitString3.CountSetBits()}");

            // Traditional approach: Multiple memory passes
            Console.WriteLine("\nTraditional approach (multiple memory passes):");
            var traditionalResult = new BitString(bitString1);
            var start = DateTime.UtcNow;
            
            traditionalResult.Not();           // Pass 1: Invert all bits
            traditionalResult.And(bitString2); // Pass 2: AND with bitString2
            traditionalResult.Or(bitString3);  // Pass 3: OR with bitString3
            
            var traditionalTime = DateTime.UtcNow - start;
            Console.WriteLine($"Result set bits: {traditionalResult.CountSetBits()}");
            Console.WriteLine($"Time taken: {traditionalTime.TotalMicroseconds:F2} μs");

            // Optimized approach: Single memory pass using chaining
            Console.WriteLine("\nOptimized approach (single memory pass with chaining):");
            var optimizedResult = new BitString(bitString1);
            start = DateTime.UtcNow;

            optimizedResult.Chain()
                .Not()              // Queue: Invert all bits
                .And(bitString2)    // Queue: AND with bitString2  
                .Or(bitString3)     // Queue: OR with bitString3
                .Execute();         // Execute all operations in single pass

            var optimizedTime = DateTime.UtcNow - start;
            Console.WriteLine($"Result set bits: {optimizedResult.CountSetBits()}");
            Console.WriteLine($"Time taken: {optimizedTime.TotalMicroseconds:F2} μs");

            // Verify both approaches produce identical results
            Console.WriteLine($"\nResults are identical: {traditionalResult.Equals(optimizedResult)}");

            // Complex chaining example
            Console.WriteLine("\nComplex chaining example:");
            var complexResult = new BitString(bitString1);
            
            complexResult.Chain()
                .Not()                  // Invert all bits
                .And(bitString2)        // AND with bitString2
                .Not()                  // Invert again
                .Or(bitString3)         // OR with bitString3
                .Xor(bitString1)        // XOR with original bitString1
                .Execute();             // Execute all in single pass

            Console.WriteLine($"Complex result set bits: {complexResult.CountSetBits()}");
            Console.WriteLine("\nChaining allows complex operations to be efficiently executed in a single memory traversal!");
        }
    }
}