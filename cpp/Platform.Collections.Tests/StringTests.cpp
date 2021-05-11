#include <gtest/gtest.h>

namespace Platform::Collections::Tests
{
    TEST(StringTests, CapitalizeFirstLetterTest)
    {
        auto type = System::Common::IEnumerable<decltype("3")>::TItem{};
        ASSERT_EQ("Hello", StringExtensions::CapitalizeFirstLetter("hello"));
        ASSERT_EQ("Hello", StringExtensions::CapitalizeFirstLetter("Hello"));
        ASSERT_EQ("  Hello", StringExtensions::CapitalizeFirstLetter("  hello"));
    }

    TEST(StringTests, TrimSingleTest)
    {
        ASSERT_EQ("", StringExtensions::TrimSingle("'", '\''));
        ASSERT_EQ("", StringExtensions::TrimSingle("''", '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("'hello'", '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("hello'", '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("'hello", '\''));
    }
}// namespace Platform::Collections::Tests
