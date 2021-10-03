using System;
using System.Collections;
using Xunit;
using Platform.Random;

namespace Platform.Collections.Tests
{
    /// <summary>
    /// <para>
    /// Represents the bit string tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class BitStringTests
    {
        /// <summary>
        /// <para>
        /// Tests that bit get set test.
        /// </para>
        /// <para></para>
        /// </summary>
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

        /// <summary>
        /// <para>
        /// Tests that bit vector not test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitVectorNotTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorNot();
                w.Not();
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit parallel not test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitParallelNotTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelNot();
                w.Not();
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit parallel vector not test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitParallelVectorNotTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorNot();
                w.Not();
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit vector and test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitVectorAndTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorAnd(y);
                w.And(v);
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit parallel and test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitParallelAndTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelAnd(y);
                w.And(v);
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit parallel vector and test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitParallelVectorAndTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorAnd(y);
                w.And(v);
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit vector or test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitVectorOrTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorOr(y);
                w.Or(v);
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit parallel or test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitParallelOrTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelOr(y);
                w.Or(v);
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit parallel vector or test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitParallelVectorOrTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelVectorOr(y);
                w.Or(v);
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit vector xor test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitVectorXorTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.VectorXor(y);
                w.Xor(v);
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit parallel xor test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void BitParallelXorTest()
        {
            TestToOperationsWithSameMeaning((x, y, w, v) =>
            {
                x.ParallelXor(y);
                w.Xor(v);
            });
        }

        /// <summary>
        /// <para>
        /// Tests that bit parallel vector xor test.
        /// </para>
        /// <para></para>
        /// </summary>
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
    }
}
