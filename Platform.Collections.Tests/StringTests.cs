using Xunit;

namespace Platform.Collections.Tests
{
    public static class StringTests
    {
        [Fact]
        public static void CapitalizeFirstLetterTest()
        {
            var source1 = "hello";
            var result1 = source1.CapitalizeFirstLetter();
            Assert.Equal("Hello", result1);
            var source2 = "Hello";
            var result2 = source2.CapitalizeFirstLetter();
            Assert.Equal("Hello", result2);
            var source3 = "  hello";
            var result3 = source3.CapitalizeFirstLetter();
            Assert.Equal("  Hello", result3);
        }

        [Fact]
        public static void TrimSingleTest()
        {
            var source1 = "'";
            var result1 = source1.TrimSingle('\'');
            Assert.Equal("", result1);
            var source2 = "''";
            var result2 = source2.TrimSingle('\'');
            Assert.Equal("", result2);
            var source3 = "'hello'";
            var result3 = source3.TrimSingle('\'');
            Assert.Equal("hello", result3);
            var source4 = "hello'";
            var result4 = source4.TrimSingle('\'');
            Assert.Equal("hello", result4);
            var source5 = "'hello";
            var result5 = source5.TrimSingle('\'');
            Assert.Equal("hello", result5);
        }
    }
}
