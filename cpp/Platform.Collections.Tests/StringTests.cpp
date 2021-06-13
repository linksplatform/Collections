#include <gtest/gtest.h>

namespace Platform::Collections::Tests
{
    TEST(StringTests, CapitalizeFirstLetterTest)
    {
        ASSERT_EQ("Hello", CapitalizeFirstLetter(std::string{"hello"}));
        ASSERT_EQ("Hello", CapitalizeFirstLetter(std::string{"Hello"}));
        ASSERT_EQ("  Hello", CapitalizeFirstLetter(std::string{"  hello"}));
    }

    TEST(StringTests, TrimSingleTest)
    {
        ASSERT_EQ("", TrimSingle(std::string{"'"}, '\''));
        ASSERT_EQ("", TrimSingle(std::string{"''"}, '\''));
        ASSERT_EQ("hello", TrimSingle(std::string{"'hello'"}, '\''));
        ASSERT_EQ("hello", TrimSingle(std::string{"hello'"}, '\''));
        ASSERT_EQ("hello", TrimSingle(std::string{"'hello"}, '\''));
    }
}// namespace Platform::Collections::Tests
