using System.Collections.Generic;
using Xunit;
using Platform.Collections.Lists;


namespace Platform.Collections.Tests
{
    /// <summary>
    /// <para>
    /// Represents the list tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public class ListTests
    {
        /// <summary>
        /// <para>
        /// Tests that get element test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public void GetElementTest()
        {
            var nullList = (IList<int>)null;
            Assert.Equal(0, nullList.GetElementOrDefault(1));
            Assert.False(nullList.TryGetElement(1, out int element));
            Assert.Equal(0, element);
            var list = new List<int>() { 1, 2, 3 };
            Assert.Equal(3, list.GetElementOrDefault(2));
            Assert.True(list.TryGetElement(2, out element));
            Assert.Equal(3, element);
            Assert.Equal(0, list.GetElementOrDefault(10));
            Assert.False(list.TryGetElement(10, out element));
            Assert.Equal(0, element);
        }
    }
}
