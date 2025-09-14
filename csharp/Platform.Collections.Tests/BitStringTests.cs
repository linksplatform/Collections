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

        [Fact]
        public static void LengthIncreaseTest()
        {
            var bitString = new BitString(32);
            bitString[5] = true;
            bitString[15] = true;
            bitString[31] = true;
            
            Assert.Equal(32, bitString.Length);
            
            // Increase length
            bitString.Length = 64;
            Assert.Equal(64, bitString.Length);
            
            // Original bits should be preserved
            Assert.True(bitString[5]);
            Assert.True(bitString[15]);
            Assert.True(bitString[31]);
            
            // New bits should be false
            for (int i = 32; i < 64; i++)
            {
                Assert.False(bitString[i]);
            }
        }
        
        [Fact]
        public static void LengthDecreaseTest()
        {
            var bitString = new BitString(64);
            bitString[5] = true;
            bitString[15] = true;
            bitString[25] = true;
            bitString[45] = true;
            bitString[55] = true;
            
            // Decrease length
            bitString.Length = 32;
            Assert.Equal(32, bitString.Length);
            
            // Bits within new length should be preserved
            Assert.True(bitString[5]);
            Assert.True(bitString[15]);
            Assert.True(bitString[25]);
        }
        
        [Fact]
        public static void LengthSetToZeroTest()
        {
            var bitString = new BitString(32);
            bitString[5] = true;
            bitString[15] = true;
            
            bitString.Length = 0;
            Assert.Equal(0, bitString.Length);
        }
        
        [Fact]
        public static void LengthCrossWordBoundaryTest()
        {
            var bitString = new BitString(63); // Just under 64 (1 word)
            bitString[62] = true;
            
            // Increase to cross word boundary
            bitString.Length = 128; // 2 words
            Assert.Equal(128, bitString.Length);
            Assert.True(bitString[62]);
            
            // Set bit in second word
            bitString[100] = true;
            Assert.True(bitString[100]);
            
            // Decrease back
            bitString.Length = 63;
            Assert.Equal(63, bitString.Length);
            Assert.True(bitString[62]);
        }
        
        [Fact]
        public static void LengthPartialWordMaskingTest()
        {
            var bitString = new BitString(70); // 1 full word + 6 bits in second word
            
            // Set bits in both words
            bitString[63] = true; // Last bit of first word
            bitString[64] = true; // First bit of second word
            bitString[69] = true; // Last valid bit
            
            Assert.True(bitString[63]);
            Assert.True(bitString[64]);
            Assert.True(bitString[69]);
            
            // Decrease to middle of second word - should mask off bit 69
            bitString.Length = 67; // Should keep first 3 bits of second word
            
            Assert.Equal(67, bitString.Length);
            Assert.True(bitString[63]);
            Assert.True(bitString[64]);
            
            // Increase back and check that bit 69 is cleared (masked off)
            bitString.Length = 70;
            Assert.False(bitString[69]); // Should be false because it was masked off
        }
        
        [Fact]
        public static void LengthSameValueTest()
        {
            var bitString = new BitString(32);
            bitString[5] = true;
            bitString[15] = true;
            
            bitString.Length = 32; // Set to same value
            
            Assert.Equal(32, bitString.Length);
            Assert.True(bitString[5]);
            Assert.True(bitString[15]);
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
