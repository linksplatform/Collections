using System;
using System.Diagnostics;
using Platform.Collections;

namespace OptimizationVerificationExperiment
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Optimization Verification Test");
            Console.WriteLine("==============================");
            Console.WriteLine($"Hardware Acceleration Available: {System.Numerics.Vector.IsHardwareAccelerated}");
            Console.WriteLine($"Vector<long>.Count: {System.Numerics.Vector<long>.Count}");
            Console.WriteLine();

            // Test that very small sizes now use regular operations (should be fast)
            TestSmallSize();
            
            // Test that very large sizes now use regular operations (should be fast)
            TestLargeSize();
            
            // Test that medium sizes still use vector operations (should be fast)
            TestMediumSize();
        }

        static void TestSmallSize()
        {
            Console.WriteLine("Testing small size (should now use regular operations):");
            const int size = 64; // Below VectorMinThreshold
            const int iterations = 1000;
            
            var left = new BitString(size);
            var right = new BitString(size);
            left.SetRandomBits();
            right.SetRandomBits();

            var time = MeasureTime(() => {
                for (int i = 0; i < iterations; i++)
                {
                    new BitString(left).VectorAnd(right); // This should internally call regular And()
                }
            });

            Console.WriteLine($"  VectorAnd (64 bits, {iterations} iterations): {time:F2}ms");
            Console.WriteLine($"  Expected: Fast (should use regular operations internally)");
            Console.WriteLine();
        }

        static void TestLargeSize()
        {
            Console.WriteLine("Testing large size (should now use regular operations):");
            const int size = 1000000; // Above VectorMaxThreshold
            const int iterations = 5;
            
            var left = new BitString(size);
            var right = new BitString(size);
            left.SetRandomBits();
            right.SetRandomBits();

            var time = MeasureTime(() => {
                for (int i = 0; i < iterations; i++)
                {
                    new BitString(left).VectorAnd(right); // This should internally call regular And()
                }
            });

            Console.WriteLine($"  VectorAnd (1M bits, {iterations} iterations): {time:F2}ms");
            Console.WriteLine($"  Expected: Fast (should use regular operations internally)");
            Console.WriteLine();
        }

        static void TestMediumSize()
        {
            Console.WriteLine("Testing medium size (should still use vector operations):");
            const int size = 10000; // Within optimal range
            const int iterations = 50;
            
            var left = new BitString(size);
            var right = new BitString(size);
            left.SetRandomBits();
            right.SetRandomBits();

            var vectorTime = MeasureTime(() => {
                for (int i = 0; i < iterations; i++)
                {
                    new BitString(left).VectorAnd(right); // This should use actual vector operations
                }
            });

            var regularTime = MeasureTime(() => {
                for (int i = 0; i < iterations; i++)
                {
                    new BitString(left).And(right); // Regular operations
                }
            });

            Console.WriteLine($"  VectorAnd (10K bits, {iterations} iterations): {vectorTime:F2}ms");
            Console.WriteLine($"  Regular And (10K bits, {iterations} iterations): {regularTime:F2}ms");
            Console.WriteLine($"  Speedup: {regularTime/vectorTime:F2}x");
            Console.WriteLine($"  Expected: Vector should be faster (speedup > 1.0)");
            Console.WriteLine();
        }

        static double MeasureTime(Action action)
        {
            // Warm up
            action();
            
            var stopwatch = Stopwatch.StartNew();
            action();
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }
    }
}