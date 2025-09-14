using System;
using Xunit;
using Platform.Collections;

namespace Platform.Collections.Tests
{
    public class LengthPropertyTests
    {
        [Fact]
        public void Length_IncreaseFromZero_ShouldWork()
        {
            var bitString = new BitString(0);
            Assert.Equal(0, bitString.Length);
            
            // Should be able to increase length
            bitString.Length = 64;
            Assert.Equal(64, bitString.Length);
            
            // All bits should be false initially
            for (int i = 0; i < 64; i++)
            {
                Assert.False(bitString[i]);
            }
        }
        
        [Fact]
        public void Length_IncreaseFromNonZero_ShouldPreserveExistingBits()
        {
            var bitString = new BitString(32);
            
            // Set some bits
            bitString[5] = true;
            bitString[15] = true;
            bitString[25] = true;
            
            // Increase length
            bitString.Length = 64;
            Assert.Equal(64, bitString.Length);
            
            // Original bits should be preserved
            Assert.True(bitString[5]);
            Assert.True(bitString[15]);
            Assert.True(bitString[25]);
            
            // New bits should be false
            for (int i = 32; i < 64; i++)
            {
                Assert.False(bitString[i]);
            }
        }
        
        [Fact]
        public void Length_DecreaseLength_ShouldWork()
        {
            var bitString = new BitString(64);
            
            // Set some bits
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
            
            // Should not be able to access bits beyond new length
            Assert.Throws<ArgumentOutOfRangeException>(() => bitString[45]);
            Assert.Throws<ArgumentOutOfRangeException>(() => bitString[55]);
        }
        
        [Fact]
        public void Length_SetToSameValue_ShouldNotChangeAnything()
        {
            var bitString = new BitString(32);
            bitString[5] = true;
            bitString[15] = true;
            
            bitString.Length = 32; // Set to same value
            
            Assert.Equal(32, bitString.Length);
            Assert.True(bitString[5]);
            Assert.True(bitString[15]);
        }
        
        [Fact]
        public void Length_CrossWordBoundaries_ShouldWork()
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
        public void Length_SetToZero_ShouldWork()
        {
            var bitString = new BitString(64);
            bitString[5] = true;
            bitString[15] = true;
            
            bitString.Length = 0;
            Assert.Equal(0, bitString.Length);
            
            // Should not be able to access any bits
            Assert.Throws<ArgumentOutOfRangeException>(() => bitString[0]);
        }
        
        [Fact]
        public void Length_HandlePartialWordMasking_ShouldWork()
        {
            var bitString = new BitString(70); // 1 full word + 6 bits in second word
            
            // Set bits in both words
            bitString[63] = true; // Last bit of first word
            bitString[64] = true; // First bit of second word
            bitString[69] = true; // Last valid bit
            
            // Decrease to middle of second word
            bitString.Length = 67; // Should keep first 3 bits of second word
            
            Assert.Equal(67, bitString.Length);
            Assert.True(bitString[63]);
            Assert.True(bitString[64]);
            Assert.Throws<ArgumentOutOfRangeException>(() => bitString[69]);
            
            // The bit at index 69 should be cleared from internal storage
            // even though we can't access it anymore
        }
    }
}