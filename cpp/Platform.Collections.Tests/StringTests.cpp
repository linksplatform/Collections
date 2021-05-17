#include <gtest/gtest.h>

namespace Platform::Collections::Tests
{
    TEST(StringTests, CapitalizeFirstLetterTest)
    {
        ASSERT_EQ("Hello", StringExtensions::CapitalizeFirstLetter(std::string{"hello"}));
        ASSERT_EQ("Hello", StringExtensions::CapitalizeFirstLetter(std::string{"Hello"}));
        ASSERT_EQ("  Hello", StringExtensions::CapitalizeFirstLetter(std::string{"  hello"}));
    }

    TEST(StringTests, TrimSingleTest)
    {
        ASSERT_EQ("", StringExtensions::TrimSingle(std::string{"'"}, '\''));
        ASSERT_EQ("", StringExtensions::TrimSingle(std::string{"''"}, '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle(std::string{"'hello'"}, '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle(std::string{"hello'"}, '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle(std::string{"'hello"}, '\''));
    }
}// namespace Platform::Collections::Tests
