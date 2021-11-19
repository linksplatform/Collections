using Xunit;
using Platform.Collections.Arrays;

namespace Platform.Collections.Tests
{
    public class ArrayTests
    {
        [Fact]
        public void GetElementTest()
        {
            var nullArray = (int[])null;
            Assert.Equal(0, nullArray.GetElementOrDefault(1));
            Assert.False(nullArray.TryGetElement(1, out int element));
            Assert.Equal(0, element);
            var array = new int[] { 1, 2, 3 };
            Assert.Equal(3, array.GetElementOrDefault(2));
            Assert.True(array.TryGetElement(2, out element));
            Assert.Equal(3, element);
            Assert.Equal(0, array.GetElementOrDefault(10));
            Assert.False(array.TryGetElement(10, out element));
            Assert.Equal(0, element);
        }
    }
}
