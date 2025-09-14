using System;
using System.Collections;
using Xunit;
using Platform.Random;

namespace Platform.Collections.Tests
{
    public static class BitStringTests
    {
        [Fact]
        public static void BitGetSetTest()
        {
            const int n = 250;
            var bitArray = new BitArray(n);
            var bitString = new BitString(n);
            for (var i = 0; i < n; i++)
            {
                var value = RandomHelpers.Default.NextBoolean();
                bitArray.Set(i, value);
                bitString.Set(i, value);
                Assert.Equal(value, bitArray.Get(i));
                Assert.Equal(value, bitString.Get(i));
            }
        }

        [Fact]
        public static void BitVectorNotTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorNot();
                w.Not();
            });
        }

        [Fact]
        public static void BitParallelNotTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelNot();
                w.Not();
            });
        }

        [Fact]
        public static void BitParallelVectorNotTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorNot();
                w.Not();
            });
        }

        [Fact]
        public static void BitVectorAndTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorAnd(y);
                w.And(v);
            });
        }

        [Fact]
        public static void BitParallelAndTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelAnd(y);
                w.And(v);
            });
        }

        [Fact]
        public static void BitParallelVectorAndTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorAnd(y);
                w.And(v);
            });
        }

        [Fact]
        public static void BitVectorOrTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorOr(y);
                w.Or(v);
            });
        }

        [Fact]
        public static void BitParallelOrTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelOr(y);
                w.Or(v);
            });
        }

        [Fact]
        public static void BitParallelVectorOrTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorOr(y);
                w.Or(v);
            });
        }

        [Fact]
        public static void BitVectorXorTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorXor(y);
                w.Xor(v);
            });
        }

        [Fact]
        public static void BitParallelXorTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelXor(y);
                w.Xor(v);
            });
        }

        [Fact]
        public static void BitParallelVectorXorTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorXor(y);
                w.Xor(v);
            });
        }
        private static void TestToOperationsWithSameMeaning(Action<BitString, BitString, BitString, BitString> test)
        {
            const int n = 5654;
            var x = new BitString(n);
            var y = new BitString(n);
            while (x.Equals(y))
            {
                x.SetRandomBits();
                y.SetRandomBits();
            }
            var w = new BitString(x);
            var v = new BitString(y);
            Assert.False(x.Equals(y));
            Assert.False(w.Equals(v));
            Assert.True(x.Equals(w));
            Assert.True(y.Equals(v));
            test(x, y, w, v);
            Assert.True(x.Equals(w));
        }

        [Fact]
        public static void BitStringChainBasicTest()
        {
            const int n = 100;
            var bitString1 = new BitString(n);
            var bitString2 = new BitString(n);
            var bitString3 = new BitString(n);
            
            bitString1.SetRandomBits();
            bitString2.SetRandomBits();
            bitString3.SetRandomBits();

            // Test that chaining produces same result as individual operations
            var expected = new BitString(bitString1);
            expected.Not().And(bitString2).Or(bitString3);

            var actual = new BitString(bitString1);
            actual.Chain().Not().And(bitString2).Or(bitString3).Execute();

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public static void BitStringChainEmptyTest()
        {
            const int n = 100;
            var bitString = new BitString(n);
            bitString.SetRandomBits();
            var original = new BitString(bitString);

            // Test that empty chain does not modify the original
            bitString.Chain().Execute();
            Assert.True(original.Equals(bitString));
        }

        [Fact]
        public static void BitStringChainComplexOperationsTest()
        {
            const int n = 250;
            var a = new BitString(n);
            var b = new BitString(n);
            var c = new BitString(n);
            var d = new BitString(n);

            a.SetRandomBits();
            b.SetRandomBits();
            c.SetRandomBits();
            d.SetRandomBits();

            // Complex sequence: NOT a, then AND with b, then XOR with c, then OR with d
            var expected = new BitString(a);
            expected.Not().And(b).Xor(c).Or(d);

            var actual = new BitString(a);
            actual.Chain().Not().And(b).Xor(c).Or(d).Execute();

            Assert.True(expected.Equals(actual));
        }

        [Fact]
        public static void BitStringChainSingleOperationTest()
        {
            const int n = 100;
            var bitString1 = new BitString(n);
            var bitString2 = new BitString(n);

            bitString1.SetRandomBits();
            bitString2.SetRandomBits();

            // Test single NOT operation
            var expectedNot = new BitString(bitString1);
            expectedNot.Not();
            var actualNot = new BitString(bitString1);
            actualNot.Chain().Not().Execute();
            Assert.True(expectedNot.Equals(actualNot));

            // Test single AND operation  
            var expectedAnd = new BitString(bitString1);
            expectedAnd.And(bitString2);
            var actualAnd = new BitString(bitString1);
            actualAnd.Chain().And(bitString2).Execute();
            Assert.True(expectedAnd.Equals(actualAnd));

            // Test single OR operation
            var expectedOr = new BitString(bitString1);
            expectedOr.Or(bitString2);
            var actualOr = new BitString(bitString1);
            actualOr.Chain().Or(bitString2).Execute();
            Assert.True(expectedOr.Equals(actualOr));

            // Test single XOR operation
            var expectedXor = new BitString(bitString1);
            expectedXor.Xor(bitString2);
            var actualXor = new BitString(bitString1);
            actualXor.Chain().Xor(bitString2).Execute();
            Assert.True(expectedXor.Equals(actualXor));
        }

        [Fact]
        public static void BitStringChainReusabilityTest()
        {
            const int n = 100;
            var bitString1 = new BitString(n);
            var bitString2 = new BitString(n);

            bitString1.SetRandomBits();
            bitString2.SetRandomBits();

            var original = new BitString(bitString1);

            // Execute chain multiple times - should be able to reuse
            var chain = bitString1.Chain().Not().And(bitString2);
            var result1 = chain.Execute();
            
            // Reset to original state
            bitString1 = new BitString(original);
            var result2 = bitString1.Chain().Not().And(bitString2).Execute();

            Assert.True(result1.Equals(result2));
        }
    }
}
