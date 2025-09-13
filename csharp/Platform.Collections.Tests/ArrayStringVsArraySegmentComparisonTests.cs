using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Platform.Collections.Arrays;
using Platform.Collections.Segments;

namespace Platform.Collections.Tests
{
    /// <summary>
    /// <para>Tests comparing ArrayString&lt;T&gt; with .NET's ArraySegment&lt;T&gt; to highlight similarities and differences.</para>
    /// <para>Тесты, сравнивающие ArrayString&lt;T&gt; с ArraySegment&lt;T&gt; из .NET для выявления сходств и различий.</para>
    /// </summary>
    public class ArrayStringVsArraySegmentComparisonTests
    {
        [Fact]
        public void ConstructorsComparison_ShouldShowDifferences()
        {
            // ArrayString<T> constructors
            var arrayString1 = new ArrayString<int>(5); // Creates new array with length 5
            var arrayString2 = new ArrayString<int>(new int[] { 1, 2, 3, 4, 5 }); // Uses entire existing array
            var arrayString3 = new ArrayString<int>(new int[] { 1, 2, 3, 4, 5 }, 3); // Uses first 3 elements
            
            // ArraySegment<T> constructors
            var arraySegment1 = new ArraySegment<int>(new int[] { 1, 2, 3, 4, 5 }); // Uses entire array
            var arraySegment2 = new ArraySegment<int>(new int[] { 1, 2, 3, 4, 5 }, 1, 3); // Uses offset + count
            
            // ArrayString always starts from index 0, ArraySegment can start from any offset
            Assert.Equal(0, arrayString2.Offset);
            Assert.Equal(5, arrayString2.Count);
            
            Assert.Equal(0, arraySegment1.Offset);
            Assert.Equal(5, arraySegment1.Count);
            
            Assert.Equal(1, arraySegment2.Offset);
            Assert.Equal(3, arraySegment2.Count);
        }
        
        [Fact]
        public void TypeComparison_ShouldShowClassVsStruct()
        {
            var arrayString = new ArrayString<int>(new int[] { 1, 2, 3 });
            var arraySegment = new ArraySegment<int>(new int[] { 1, 2, 3 });
            
            // ArrayString<T> is a class (reference type), ArraySegment<T> is a struct (value type)
            Assert.True(arrayString.GetType().IsClass);
            Assert.False(arraySegment.GetType().IsClass);
            Assert.True(arraySegment.GetType().IsValueType);
        }
        
        [Fact]
        public void InterfacesComparison_ShouldShowDifferences()
        {
            var arrayString = new ArrayString<int>(new int[] { 1, 2, 3 });
            var arraySegment = new ArraySegment<int>(new int[] { 1, 2, 3 });
            
            // Both implement IList<T>, ICollection<T>, IEnumerable<T>
            Assert.IsAssignableFrom<IList<int>>(arrayString);
            Assert.IsAssignableFrom<ICollection<int>>(arrayString);
            Assert.IsAssignableFrom<IEnumerable<int>>(arrayString);
            
            Assert.IsAssignableFrom<IList<int>>(arraySegment);
            Assert.IsAssignableFrom<ICollection<int>>(arraySegment);
            Assert.IsAssignableFrom<IEnumerable<int>>(arraySegment);
            
            // ArraySegment also implements IReadOnlyList<T> and IReadOnlyCollection<T>
            Assert.IsAssignableFrom<IReadOnlyList<int>>(arraySegment);
            Assert.IsAssignableFrom<IReadOnlyCollection<int>>(arraySegment);
        }
        
        [Fact]
        public void ReadOnlyBehaviorComparison_ShouldShowSimilarities()
        {
            var source = new int[] { 1, 2, 3, 4, 5 };
            var arrayString = new ArrayString<int>(source);
            var arraySegment = new ArraySegment<int>(source);
            
            // Both are read-only (cannot insert, add, remove, clear)
            Assert.True(arrayString.IsReadOnly);
            // Cast to ICollection<T> to access IsReadOnly property
            Assert.True(((ICollection<int>)arraySegment).IsReadOnly);
            
            Assert.Throws<NotSupportedException>(() => arrayString.Add(6));
            Assert.Throws<NotSupportedException>(() => ((ICollection<int>)arraySegment).Add(6));
            
            Assert.Throws<NotSupportedException>(() => arrayString.Insert(0, 0));
            Assert.Throws<NotSupportedException>(() => ((IList<int>)arraySegment).Insert(0, 0));
            
            Assert.Throws<NotSupportedException>(() => arrayString.Remove(1));
            Assert.Throws<NotSupportedException>(() => ((ICollection<int>)arraySegment).Remove(1));
            
            Assert.Throws<NotSupportedException>(() => arrayString.Clear());
            Assert.Throws<NotSupportedException>(() => ((ICollection<int>)arraySegment).Clear());
            
            Assert.Throws<NotSupportedException>(() => arrayString.RemoveAt(0));
            Assert.Throws<NotSupportedException>(() => ((IList<int>)arraySegment).RemoveAt(0));
        }
        
        [Fact]
        public void IndexerBehaviorComparison_ShouldShowSimilarities()
        {
            var source = new int[] { 1, 2, 3, 4, 5 };
            var arrayString = new ArrayString<int>(source);
            var arraySegment = new ArraySegment<int>(source);
            
            // Both allow reading and writing through indexer
            Assert.Equal(1, arrayString[0]);
            Assert.Equal(1, arraySegment[0]);
            
            // Both allow modification through indexer (affects original array)
            arrayString[0] = 10;
            Assert.Equal(10, source[0]);
            
            arraySegment[1] = 20;
            Assert.Equal(20, source[1]);
            
            // Verify both see the changes
            Assert.Equal(10, arrayString[0]);
            Assert.Equal(20, arraySegment[1]);
        }
        
        [Fact]
        public void SegmentBehaviorComparison_ShouldShowDifferences()
        {
            var source = new int[] { 1, 2, 3, 4, 5 };
            
            // ArrayString always starts from 0 and can only specify length
            var arrayString = new ArrayString<int>(source, 3); // First 3 elements
            
            // ArraySegment can start from any offset with any count
            var arraySegment = new ArraySegment<int>(source, 2, 2); // Elements at indices 2 and 3
            
            Assert.Equal(3, arrayString.Count);
            Assert.Equal(0, arrayString.Offset);
            Assert.Equal(1, arrayString[0]); // source[0]
            Assert.Equal(2, arrayString[1]); // source[1]
            Assert.Equal(3, arrayString[2]); // source[2]
            
            Assert.Equal(2, arraySegment.Count);
            Assert.Equal(2, arraySegment.Offset);
            Assert.Equal(3, arraySegment[0]); // source[2]
            Assert.Equal(4, arraySegment[1]); // source[3]
        }
        
        [Fact]
        public void EnumerationComparison_ShouldShowSimilarities()
        {
            var source = new int[] { 1, 2, 3, 4, 5 };
            var arrayString = new ArrayString<int>(source, 3);
            var arraySegment = new ArraySegment<int>(source, 0, 3);
            
            // Both support enumeration
            var arrayStringList = arrayString.ToList();
            var arraySegmentList = arraySegment.ToList();
            
            Assert.Equal(new[] { 1, 2, 3 }, arrayStringList);
            Assert.Equal(new[] { 1, 2, 3 }, arraySegmentList);
            
            // Both support LINQ operations
            Assert.Equal(3, arrayString.Count(x => x <= 3));
            Assert.Equal(3, arraySegment.Count(x => x <= 3));
        }
        
        [Fact]
        public void EqualityComparison_ShouldShowDifferences()
        {
            var source1 = new int[] { 1, 2, 3 };
            var source2 = new int[] { 1, 2, 3 };
            
            var arrayString1 = new ArrayString<int>(source1);
            var arrayString2 = new ArrayString<int>(source2);
            var arrayString3 = new ArrayString<int>(source1); // Same reference
            
            var arraySegment1 = new ArraySegment<int>(source1);
            var arraySegment2 = new ArraySegment<int>(source2);
            var arraySegment3 = new ArraySegment<int>(source1); // Same reference
            
            // ArrayString uses custom equality implementation (content-based via EqualTo extension)
            Assert.True(arrayString1.Equals(arrayString2)); // Different arrays, same content
            Assert.True(arrayString1.Equals(arrayString3)); // Same array reference
            
            // ArraySegment uses default struct equality (checks array reference and offset/count)
            Assert.False(arraySegment1.Equals(arraySegment2)); // Different array references
            Assert.True(arraySegment1.Equals(arraySegment3)); // Same array reference
        }
        
        [Fact]
        public void HashCodeComparison_ShouldShowDifferences()
        {
            var source1 = new int[] { 1, 2, 3 };
            var source2 = new int[] { 1, 2, 3 };
            
            var arrayString1 = new ArrayString<int>(source1);
            var arrayString2 = new ArrayString<int>(source2);
            
            var arraySegment1 = new ArraySegment<int>(source1);
            var arraySegment2 = new ArraySegment<int>(source2);
            
            // ArrayString generates hash code based on content
            Assert.Equal(arrayString1.GetHashCode(), arrayString2.GetHashCode()); // Same content, same hash
            
            // ArraySegment generates hash code based on array reference and segment info
            Assert.NotEqual(arraySegment1.GetHashCode(), arraySegment2.GetHashCode()); // Different arrays, different hash
        }
        
        [Fact]
        public void MemoryAllocationComparison_ShouldShowDifferences()
        {
            var source = new int[] { 1, 2, 3, 4, 5 };
            
            // ArrayString can create new arrays (allocation) or wrap existing ones
            var arrayStringNew = new ArrayString<int>(5); // Allocates new array
            var arrayStringWrapped = new ArrayString<int>(source); // Wraps existing array
            
            // ArraySegment always wraps existing arrays (no allocation)
            var arraySegment = new ArraySegment<int>(source);
            
            Assert.NotNull(arrayStringNew.Base);
            Assert.Same(source, arrayStringWrapped.Base);
            Assert.Same(source, arraySegment.Array);
        }
        
        [Fact]
        public void DefaultValueComparison_ShouldShowDifferences()
        {
            // Default ArrayString<T> is null (reference type)
            ArrayString<int>? defaultArrayString = default;
            Assert.Null(defaultArrayString);
            
            // Default ArraySegment<T> represents an empty segment (value type)
            ArraySegment<int> defaultArraySegment = default;
            // Array property returns null for default ArraySegment, but struct itself is not null
            Assert.Null(defaultArraySegment.Array);
            Assert.Equal(0, defaultArraySegment.Count);
            Assert.Equal(0, defaultArraySegment.Offset);
        }
        
        [Fact]
        public void PerformanceCharacteristics_ComparisonNotes()
        {
            // This test documents performance characteristics rather than testing them
            
            var source = new int[] { 1, 2, 3, 4, 5 };
            var arrayString = new ArrayString<int>(source);
            var arraySegment = new ArraySegment<int>(source);
            
            // ArrayString<T> (class):
            // - Heap allocation overhead
            // - Reference equality fast, content equality slower
            // - Garbage collection overhead
            
            // ArraySegment<T> (struct):
            // - No heap allocation for the segment itself
            // - Value type copying overhead when passed by value
            // - No garbage collection overhead for the segment
            
            // Both:
            // - O(1) indexer access
            // - Same memory access patterns for the underlying array
            // - Similar enumeration performance
            
            Assert.True(true); // This test always passes, it's for documentation
        }
    }
}