using Xunit;
using Platform.Collections.Arrays;

namespace Platform.Collections.Tests
{
    public class ArrayPoolTests
    {
        [Fact]
        public void Resize_WhenSizesAreSame_ShouldReturnSameObject()
        {
            var pool = new ArrayPool<int>();
            var originalArray = pool.AllocateDisposable(5);
            int[] arr = originalArray;
            
            // Fill with test data
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 1;
            }
            
            // Resize to the same size
            var resizedArray = pool.Resize(originalArray, 5);
            int[] newArr = resizedArray;
            
            // Should be the same object reference
            Assert.True(object.ReferenceEquals(arr, newArr));
            
            // Data should remain unchanged
            Assert.Equal(new int[] { 1, 2, 3, 4, 5 }, newArr);
            
            resizedArray.Dispose();
        }
        
        [Fact]
        public void Resize_WhenSizesDiffer_ShouldCreateNewObject()
        {
            var pool = new ArrayPool<int>();
            var originalArray = pool.AllocateDisposable(5);
            int[] arr = originalArray;
            
            // Fill with test data
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 1;
            }
            
            // Resize to different size (smaller)
            var resizedArray = pool.Resize(originalArray, 3);
            int[] newArr = resizedArray;
            
            // Should be different object reference
            Assert.False(object.ReferenceEquals(arr, newArr));
            
            // Data should be copied (first 3 elements)
            Assert.Equal(new int[] { 1, 2, 3 }, newArr);
            
            resizedArray.Dispose();
        }
        
        [Fact]
        public void Resize_WhenResizingToLarger_ShouldCopyAllOriginalData()
        {
            var pool = new ArrayPool<int>();
            var originalArray = pool.AllocateDisposable(3);
            int[] arr = originalArray;
            
            // Fill with test data
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 1;
            }
            
            // Resize to larger size
            var resizedArray = pool.Resize(originalArray, 5);
            int[] newArr = resizedArray;
            
            // Should be different object reference
            Assert.False(object.ReferenceEquals(arr, newArr));
            
            // Original data should be copied, new elements should be default (0)
            Assert.Equal(new int[] { 1, 2, 3, 0, 0 }, newArr);
            
            resizedArray.Dispose();
        }
        
        [Fact]
        public void Resize_WithEmptyArray_ShouldAllocateNewArray()
        {
            var pool = new ArrayPool<int>();
            var emptyArray = pool.AllocateDisposable(0);
            
            // Resize empty array to size 3
            var resizedArray = pool.Resize(emptyArray, 3);
            int[] newArr = resizedArray;
            
            Assert.Equal(3, newArr.Length);
            Assert.All(newArr, x => Assert.Equal(0, x)); // All elements should be default
            
            resizedArray.Dispose();
        }
        
        [Fact]
        public void Resize_ToZeroSize_ShouldReturnEmptyArray()
        {
            var pool = new ArrayPool<int>();
            var originalArray = pool.AllocateDisposable(5);
            
            // Resize to zero size
            var resizedArray = pool.Resize(originalArray, 0);
            int[] newArr = resizedArray;
            
            Assert.Equal(0, newArr.Length);
            
            resizedArray.Dispose();
        }
    }
}