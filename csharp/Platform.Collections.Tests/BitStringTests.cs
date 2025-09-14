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
        public static void AutomaticBorderRefreshingPropertyTest()
        {
            var bitString = new BitString(100);
            Assert.True(bitString.AutomaticBorderRefreshing);
            
            bitString.AutomaticBorderRefreshing = false;
            Assert.False(bitString.AutomaticBorderRefreshing);
            
            bitString.AutomaticBorderRefreshing = true;
            Assert.True(bitString.AutomaticBorderRefreshing);
        }

        [Fact]
        public static void CreateWithAutomaticBorderRefreshingTest()
        {
            var automaticBitString = BitString.Create(100, true);
            Assert.True(automaticBitString.AutomaticBorderRefreshing);
            
            var manualBitString = BitString.Create(100, false);
            Assert.False(manualBitString.AutomaticBorderRefreshing);
        }

        [Fact]
        public static void CreateWithDefaultValueAndAutomaticBorderRefreshingTest()
        {
            var automaticBitString = BitString.Create(100, false, true);
            Assert.True(automaticBitString.AutomaticBorderRefreshing);
            
            var manualBitString = BitString.Create(100, true, false);
            Assert.False(manualBitString.AutomaticBorderRefreshing);
        }

        [Fact]
        public static void ManualBorderRefreshTest()
        {
            var bitString = BitString.Create(1000, false);
            
            // Set some bits
            bitString.Set(100, true);
            bitString.Set(500, true);
            bitString.Set(900, true);
            
            // Manually refresh borders
            bool updated = bitString.RefreshBorders();
            Assert.True(updated);
            
            // Verify correct borders
            Assert.Equal(100, bitString.GetFirstSetBitIndex());
            Assert.Equal(900, bitString.GetLastSetBitIndex());
        }

        [Fact]
        public static void AutomaticVsManualBorderBehaviorTest()
        {
            // Test with automatic border refreshing
            var automaticBitString = new BitString(100);
            automaticBitString.Set(50, true);
            var automaticFirst = automaticBitString.GetFirstSetBitIndex();
            var automaticLast = automaticBitString.GetLastSetBitIndex();
            
            // Test with manual border refreshing
            var manualBitString = BitString.Create(100, false);
            manualBitString.Set(50, true);
            manualBitString.RefreshBorders();
            var manualFirst = manualBitString.GetFirstSetBitIndex();
            var manualLast = manualBitString.GetLastSetBitIndex();
            
            // Results should be the same
            Assert.Equal(automaticFirst, manualFirst);
            Assert.Equal(automaticLast, manualLast);
            Assert.Equal(50, automaticFirst);
            Assert.Equal(50, manualFirst);
        }

        [Fact]
        public static void CopyConstructorPreservesAutomaticBorderRefreshingTest()
        {
            var originalAutomatic = new BitString(100);
            var copyAutomatic = new BitString(originalAutomatic);
            Assert.True(copyAutomatic.AutomaticBorderRefreshing);
            
            var originalManual = BitString.Create(100, false);
            var copyManual = new BitString(originalManual);
            Assert.False(copyManual.AutomaticBorderRefreshing);
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
