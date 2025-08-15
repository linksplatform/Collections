using Xunit;
using Platform.Collections.Segments;

namespace Platform.Collections.Tests
{
    public static class CharsSegmentTests
    {
        [Fact]
        public static void GetHashCodeEqualsTest()
        {
            const string testString = "test test";
            var testArray = testString.ToCharArray();
            var firstHashCode = new CharSegment(testArray, 0, 4).GetHashCode();
            var secondHashCode = new CharSegment(testArray, 5, 4).GetHashCode();
            Assert.Equal(firstHashCode, secondHashCode);
        }

        [Fact]
        public static void EqualsTest()
        {
            const string testString = "test test";
            var testArray = testString.ToCharArray();
            var first = new CharSegment(testArray, 0, 4);
            var second = new CharSegment(testArray, 5, 4);
            Assert.True(first.Equals(second));
        }

        [Fact]
        public static void Issue136()
        {
            const string str = "aaaaaaaaaa";
            var test = str.ToCharArray();
            var a = new CharSegment(test, 0, 3);
            var b = new CharSegment(test, 7, 3);
            Assert.True(a.Equals(b));
            Assert.True(b.Equals(a));
            Assert.Equal(a, b);
        }
    }
}
