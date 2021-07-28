namespace Platform::Collections::Tests
{
    TEST(StringTests, CapitalizeFirstLetterTest)
    {
        using namespace std::string_literals;

        std::locale::global(std::locale(""));
        ASSERT_EQ(u"Привет"s, CapitalizeFirstLetter(u"привет"s));
        ASSERT_EQ("Hello", CapitalizeFirstLetter("hello"s));
        ASSERT_EQ("Hello", CapitalizeFirstLetter("Hello"s));
        ASSERT_EQ("  Hello", CapitalizeFirstLetter("  hello"s));
    }

    TEST(StringTests, TrimSingleTest)
    {
        using namespace std::string_literals;

        ASSERT_EQ("", TrimSingle("'"s, '\''));
        ASSERT_EQ("", TrimSingle("''"s, '\''));
        ASSERT_EQ("hello", TrimSingle("'hello'"s, '\''));
        ASSERT_EQ("hello", TrimSingle("hello'"s, '\''));
        ASSERT_EQ("hello", TrimSingle("'hello"s, '\''));
    }
}// namespace Platform::Collections::Tests
