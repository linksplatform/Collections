using Xunit;

namespace Platform.Collections.Tests
{
    public static class StringTests
    {
        [Fact]
        public static void CapitalizeFirstLetterTest()
        {
            Assert.Equal("Hello", "hello".CapitalizeFirstLetter());
            Assert.Equal("Hello", "Hello".CapitalizeFirstLetter());
            Assert.Equal("  Hello", "  hello".CapitalizeFirstLetter());
        }

        [Fact]
        public static void TrimSingleTest()
        {
            Assert.Equal("", "'".TrimSingle('\''));
            Assert.Equal("", "''".TrimSingle('\''));
            Assert.Equal("hello", "'hello'".TrimSingle('\''));
            Assert.Equal("hello", "hello'".TrimSingle('\''));
            Assert.Equal("hello", "'hello".TrimSingle('\''));
        }

        [Fact]
        public static void EqualsIgnoreCaseTest()
        {
            Assert.True("Hello".EqualsIgnoreCase("hello"));
            Assert.True("HELLO".EqualsIgnoreCase("hello"));
            Assert.True("hello".EqualsIgnoreCase("HELLO"));
            Assert.True("Hello World".EqualsIgnoreCase("HELLO WORLD"));
            Assert.True("".EqualsIgnoreCase(""));
            Assert.False("Hello".EqualsIgnoreCase("World"));
            Assert.False("Hello".EqualsIgnoreCase("Hell"));
            Assert.False("Hello".EqualsIgnoreCase(null!));
            Assert.False(((string?)null).EqualsIgnoreCase("Hello"));
            Assert.True(((string?)null).EqualsIgnoreCase(null));
        }
    }
}
