using Xunit;
using Platform.Collections.Segments;

namespace Platform.Collections.Tests
{
    /// <summary>
    /// <para>
    /// Represents the chars segment tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class CharsSegmentTests
    {
        /// <summary>
        /// <para>
        /// Tests that get hash code equals test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void GetHashCodeEqualsTest()
        {
            const string testString = "test test";
            var testArray = testString.ToCharArray();
            var firstHashCode = new CharSegment(testArray, 0, 4).GetHashCode();
            var secondHashCode = new CharSegment(testArray, 5, 4).GetHashCode();
            Assert.Equal(firstHashCode, secondHashCode);
        }

        /// <summary>
        /// <para>
        /// Tests that equals test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void EqualsTest()
        {
            const string testString = "test test";
            var testArray = testString.ToCharArray();
            var first = new CharSegment(testArray, 0, 4);
            var second = new CharSegment(testArray, 5, 4);
            Assert.True(first.Equals(second));
        }
    }
}
