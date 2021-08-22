using Xunit;

namespace Platform.Collections.Tests
{
    /// <summary>
    /// <para>
    /// Represents the string tests.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class StringTests
    {
        /// <summary>
        /// <para>
        /// Tests that capitalize first letter test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void CapitalizeFirstLetterTest()
        {
            Assert.Equal("Hello", "hello".CapitalizeFirstLetter());
            Assert.Equal("Hello", "Hello".CapitalizeFirstLetter());
            Assert.Equal("  Hello", "  hello".CapitalizeFirstLetter());
        }

        /// <summary>
        /// <para>
        /// Tests that trim single test.
        /// </para>
        /// <para></para>
        /// </summary>
        [Fact]
        public static void TrimSingleTest()
        {
            Assert.Equal("", "'".TrimSingle('\''));
            Assert.Equal("", "''".TrimSingle('\''));
            Assert.Equal("hello", "'hello'".TrimSingle('\''));
            Assert.Equal("hello", "hello'".TrimSingle('\''));
            Assert.Equal("hello", "'hello".TrimSingle('\''));
        }
    }
}
