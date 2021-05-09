#include <Platform.Collections.h>
#include <gtest/gtest.h>

namespace Platform::Collections::Tests
{
    TEST(ConceptsTest, Arrays)
    {
        using vector = std::vector<int>;
        using array = std::array<int, 0>;

        ASSERT_TRUE(System::Array<vector>);
        ASSERT_TRUE(System::Array<array>);

        ASSERT_TRUE((System::Array<vector, int>));
        ASSERT_TRUE((System::Array<array, int>));
    }

    TEST(ConceptsTest, Lists)
    {
        using vector = std::vector<int>;
        using array = std::array<int, 0>;

        ASSERT_TRUE(System::IList<vector>);
        ASSERT_FALSE(System::IList<array>);

        ASSERT_TRUE((System::IList<vector, int>));
    }

    TEST(ArrayTests, GetElementTest)
    {
        {
            auto nullArray = std::array<int, 0>{};
            ASSERT_EQ(0, Arrays::GetElementOrDefault(nullArray, 1));
            int element;
            ASSERT_FALSE(Arrays::TryGetElement(nullArray, 1, element));
            ASSERT_EQ(0, element);
        }

        {
            auto array = std::array{1, 2, 3};
            ASSERT_EQ(3, Arrays::GetElementOrDefault(array, 2));
            int element;
            ASSERT_TRUE(Arrays::TryGetElement(array, 2, element));
            ASSERT_EQ(3, element);
            ASSERT_EQ(0, Arrays::GetElementOrDefault(array, 10));
            ASSERT_FALSE(Arrays::TryGetElement(array, 10, element));
            ASSERT_EQ(0, element);
        }
    }

    TEST(BitStringTests, BitGetSetTest)
    {
        constexpr int n = 250;
        auto bitArray = std::array<bool, n>();
        auto bitString = std::bitset<n>();
        for (int i = 0; i < n; i++)
        {
            auto value = rand() % 2;
            bitString[i] = value;
            bitArray[i] = value;
            ASSERT_EQ(value, bitArray[i]);
            ASSERT_EQ(value, bitString[i]);
        }
    }

    // Делаем тесты, как будто сами его написали и хвастаемся, что работает))
    TEST(ChangeSegmentTests, GetHashCodeEqualsTest)
    {
        const std::string testString = "test test";
        auto testArray = std::vector(testString.begin(), testString.end());
        auto firstHashCode = Platform::Hashing::Hash(std::span(testArray.begin(), 4));
        auto secondHashCode = Platform::Hashing::Hash(std::span(testArray.begin() + 5, 4));
        ASSERT_EQ(firstHashCode, secondHashCode);
    }

    TEST(ChangeSegmentTests, EqualsTest)
    {
        const std::string testString = "test test";
        auto testArray = std::vector(testString.begin(), testString.end());
        auto first = std::span(testArray.begin(), 4);
        auto second = std::span(testArray.begin() + 5, 4);
        ASSERT_TRUE(std::equal_to<std::span<char>>{}(first, second));
    }

    TEST(ListsTests, GetElementTest)
    {
        {
            auto nullList = std::vector<int>{};
            int element;
            ASSERT_EQ(0, Arrays::GetElementOrDefault(nullList, 1));
            ASSERT_FALSE(Arrays::TryGetElement(nullList, 1, element));
            ASSERT_EQ(0, element);
            // Lists and Arrays are Backward Compatible
            ASSERT_EQ(0, Lists::GetElementOrDefault(nullList, 1));
            ASSERT_FALSE(Lists::TryGetElement(nullList, 1, element));
            ASSERT_EQ(0, element);
        }

        {
            auto array = std::vector<int>{1, 2, 3};
            ASSERT_EQ(3, Lists::GetElementOrDefault(array, 2));
            int element;
            ASSERT_TRUE(Lists::TryGetElement(array, 2, element));
            ASSERT_EQ(3, element);
            ASSERT_EQ(0, Lists::GetElementOrDefault(array, 10));
            ASSERT_FALSE(Lists::TryGetElement(array, 10, element));
            ASSERT_EQ(0, element);
        }
    }

    TEST(StringTests, CapitalizeFirstLetterTest)
    {
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
