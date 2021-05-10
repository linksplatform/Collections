#include <gtest/gtest.h>

namespace Platform::Collections::Tests
{
    TEST(StringTests, CapitalizeFirstLetterTest)
    {
        using namespace std::string_literals;
        ASSERT_EQ("Hello", StringExtensions::CapitalizeFirstLetter("hello"s));
        ASSERT_EQ("Hello", StringExtensions::CapitalizeFirstLetter("Hello"s));
        ASSERT_EQ("  Hello", StringExtensions::CapitalizeFirstLetter("  hello"s));
    }

    TEST(StringTests, TrimSingleTest)
    {
        using namespace std::string_literals;
        ASSERT_EQ("", StringExtensions::TrimSingle("'"s, '\''));
        ASSERT_EQ("", StringExtensions::TrimSingle("''"s, '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("'hello'"s, '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("hello'"s, '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("'hello"s, '\''));
    }
}// namespace Platform::Collections::Tests
