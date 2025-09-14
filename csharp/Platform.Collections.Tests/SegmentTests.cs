using System;
using System.Linq;
using Xunit;
using Platform.Collections.Segments;

namespace Platform.Collections.Tests
{
    public static class SegmentTests
    {
        [Fact]
        public static void EmptySegmentTest()
        {
            var empty = Segment<int>.Empty;
            Assert.Equal(0, empty.Length);
            Assert.Equal(0, empty.Count);
            Assert.Equal(0, empty.Offset);
            Assert.NotNull(empty.Base);
        }

        [Fact]
        public static void ArrayConstructorTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            var segment = new Segment<int>(array);
            
            Assert.Equal(array, segment.Array);
            Assert.Equal(array, segment.Base);
            Assert.Equal(0, segment.Offset);
            Assert.Equal(5, segment.Length);
            Assert.Equal(5, segment.Count);
        }

        [Fact]
        public static void ArrayWithOffsetAndCountConstructorTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            var segment = new Segment<int>(array, 1, 3);
            
            Assert.Equal(array, segment.Array);
            Assert.Equal(array, segment.Base);
            Assert.Equal(1, segment.Offset);
            Assert.Equal(3, segment.Length);
            Assert.Equal(3, segment.Count);
            
            // Test indexing
            Assert.Equal(2, segment[0]);
            Assert.Equal(3, segment[1]);
            Assert.Equal(4, segment[2]);
        }

        [Fact]
        public static void ArrayConstructorValidationTest()
        {
            Assert.Throws<ArgumentNullException>(() => new Segment<int>((int[])null));
            Assert.Throws<ArgumentNullException>(() => new Segment<int>(null, 0, 0));
            
            var array = new int[] { 1, 2, 3 };
            Assert.Throws<ArgumentOutOfRangeException>(() => new Segment<int>(array, -1, 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => new Segment<int>(array, 0, -1));
            Assert.Throws<ArgumentException>(() => new Segment<int>(array, 2, 3)); // offset + count > array.Length
        }

        [Fact]
        public static void SliceTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            var segment = new Segment<int>(array, 1, 4); // [2, 3, 4, 5]
            
            // Slice from index 1 to end
            var slice1 = segment.Slice(1);
            Assert.Equal(2, slice1.Offset); // 1 + 1
            Assert.Equal(3, slice1.Length); // 4 - 1
            Assert.Equal(3, slice1[0]); // array[2]
            Assert.Equal(4, slice1[1]); // array[3]
            Assert.Equal(5, slice1[2]); // array[4]
            
            // Slice with specific count
            var slice2 = segment.Slice(1, 2);
            Assert.Equal(2, slice2.Offset); // 1 + 1
            Assert.Equal(2, slice2.Length); // specified count
            Assert.Equal(3, slice2[0]); // array[2]
            Assert.Equal(4, slice2[1]); // array[3]
        }

        [Fact]
        public static void SliceValidationTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            var segment = new Segment<int>(array, 1, 3);
            
            Assert.Throws<ArgumentOutOfRangeException>(() => segment.Slice(-1));
            Assert.Throws<ArgumentOutOfRangeException>(() => segment.Slice(4)); // index > Length
            Assert.Throws<ArgumentOutOfRangeException>(() => segment.Slice(0, -1));
            Assert.Throws<ArgumentOutOfRangeException>(() => segment.Slice(2, 2)); // index + count > Length
        }

        [Fact]
        public static void ToArrayTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            var segment = new Segment<int>(array, 1, 3); // [2, 3, 4]
            
            var result = segment.ToArray();
            
            Assert.Equal(3, result.Length);
            Assert.Equal(2, result[0]);
            Assert.Equal(3, result[1]);
            Assert.Equal(4, result[2]);
            
            // Ensure it's a copy, not the same array
            Assert.NotSame(array, result);
            
            // Modify original array and ensure copy is unchanged
            array[2] = 99;
            Assert.Equal(3, result[1]); // Should still be 3
        }

        [Fact]
        public static void ArrayPropertyTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            var segment = new Segment<int>(array);
            
            Assert.Same(array, segment.Array);
            
            // Test with IList<T> (not array)
            var list = new System.Collections.Generic.List<int> { 1, 2, 3 };
            var listSegment = new Segment<int>(list, 0, 2);
            Assert.Null(listSegment.Array); // Should return null for non-array IList<T>
        }

        [Fact]
        public static void EnumerationTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            var segment = new Segment<int>(array, 1, 3); // [2, 3, 4]
            
            var result = segment.ToArray();
            var expected = new int[] { 2, 3, 4 };
            
            Assert.True(result.SequenceEqual(expected));
        }

        [Fact]
        public static void CountPropertyCompatibilityTest()
        {
            var array = new int[] { 1, 2, 3, 4, 5 };
            var segment = new Segment<int>(array, 1, 3);
            
            // Count should be the same as Length (ArraySegment compatibility)
            Assert.Equal(segment.Length, segment.Count);
            Assert.Equal(3, segment.Count);
        }

        [Fact]
        public static void ArraySegmentSemanticCompatibilityTest()
        {
            var array = new int[] { 10, 20, 30, 40, 50 };
            
            // Test behavior similar to ArraySegment<T>
            var segment1 = new Segment<int>(array); // Entire array
            var segment2 = new Segment<int>(array, 2, 2); // [30, 40]
            
            // ArraySegment-like properties
            Assert.Same(array, segment1.Array);
            Assert.Same(array, segment2.Array);
            Assert.Equal(0, segment1.Offset);
            Assert.Equal(2, segment2.Offset);
            Assert.Equal(5, segment1.Count);
            Assert.Equal(2, segment2.Count);
            
            // ArraySegment-like methods
            var slice = segment1.Slice(1, 3); // [20, 30, 40]
            Assert.Equal(3, slice.Count);
            Assert.Equal(20, slice[0]);
            Assert.Equal(30, slice[1]);
            Assert.Equal(40, slice[2]);
            
            var copyArray = slice.ToArray();
            Assert.Equal(new int[] { 20, 30, 40 }, copyArray);
        }
    }
}