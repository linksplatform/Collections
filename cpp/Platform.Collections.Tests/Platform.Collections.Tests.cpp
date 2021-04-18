#include <Platform.Collections.h>
#include <gtest/gtest.h>

namespace Platform::Collections::Tests
{
    class ArrayTests : public ::testing::Test
    {
    protected:
        void SetUp() override
        {
        }
    };

    TEST_F(ArrayTests, GetElementTest)
    {
        {
            // If someone doesn't know 0 == int{} (or default(int))
            auto nullArray = std::array<int, 0>{};
            ASSERT_EQ(0, Arrays::GenericArrayExtensions::GetElementOrDefault<int>(nullArray, 1));
            int element;
            ASSERT_FALSE(Arrays::GenericArrayExtensions::TryGetElement<int>(nullArray, 1, element));
            ASSERT_EQ(0, element);
        }

        {
            auto array = std::array{1, 2, 3};
            ASSERT_EQ(3, Arrays::GenericArrayExtensions::GetElementOrDefault<int>(array, 2));
            int element;
            ASSERT_TRUE(Arrays::GenericArrayExtensions::TryGetElement<int>(array, 2, element));
            ASSERT_EQ(3, element);
            ASSERT_EQ(0, Arrays::GenericArrayExtensions::GetElementOrDefault<int>(array, 10));
            ASSERT_FALSE(Arrays::GenericArrayExtensions::TryGetElement<int>(array, 10, element));
            ASSERT_EQ(0, element);
        }
    }

    class BitStringTests : public ::testing::Test
    {
    protected:
        void SetUp() override
        {
        }


    private:
        template<std::size_t Size1, std::size_t Size2, std::size_t Size3, std::size_t Size4>
        static void TestToOperationsWithSameMeaning(std::function<void(
                                                            std::bitset<Size1>&,
                                                            std::bitset<Size2>&,
                                                            std::bitset<Size3>&,
                                                            std::bitset<Size4>&)>
                                                            test)
        {
            constexpr std::int32_t n = 5654;
            auto x = std::bitset<n>();
            auto y = std::bitset<n>();
            while (std::equal_to<std::bitset<n>>{}(x, y))
            {
                BitStringExtensions::SetRandomBits(x);
                BitStringExtensions::SetRandomBits(y);
            }
            auto w = std::bitset(x);
            auto v = std::bitset(y);
            ASSERT_FALSE(std::equal_to<std::bitset<n>>{}(x, w));
            ASSERT_FALSE(std::equal_to<std::bitset<n>>{}(v, w));
            ASSERT_TRUE(std::equal_to<std::bitset<n>>{}(x, w));
            ASSERT_TRUE(std::equal_to<std::bitset<n>>{}(y, v));
            test(x, y, w, v);
            ASSERT_TRUE(std::equal_to<std::bitset<n>>{}(x, w));
        }
    };

    TEST_F(BitStringTests, BitGetSetTest)
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


    class ChangeSegmentTests : public ::testing::Test
    {
    protected:
        void SetUp() override
        {
        }
    };

    // Делаем тесты, как будто сами его написали и хвастаемся, что работает
    TEST_F(ChangeSegmentTests, GetHashCodeEqualsTest)
    {
        const std::string testString = "test test";
        auto testArray = std::vector(testString.begin(), testString.end());
        auto firstHashCode = Platform::Hashing::Hash(std::span(testArray.begin(), 4));
        auto secondHashCode = Platform::Hashing::Hash(std::span(testArray.begin() + 5, 4));
        ASSERT_EQ(firstHashCode, secondHashCode);
    }

    TEST_F(ChangeSegmentTests, EqualsTest)
    {
        const std::string testString = "test test";
        auto testArray = std::vector(testString.begin(), testString.end());
        auto first = std::span(testArray.begin(), 4);
        auto second = std::span(testArray.begin() + 5, 4);
        ASSERT_TRUE(std::equal_to<std::span<char>>{}(first, second));
    }

    class ListsTests : public ::testing::Test
    {
    protected:
        void SetUp() override
        {
        }
    };

    TEST_F(ListsTests, GetElementTest)
    {
        {
            auto nullList = std::vector<int>{};
            int element;
            ASSERT_EQ(0, Arrays::GenericArrayExtensions::GetElementOrDefault<int>(nullList, 1));
            ASSERT_FALSE(Arrays::GenericArrayExtensions::TryGetElement<int>(nullList, 1, element));
            ASSERT_EQ(0, element);
            // Lists and Arrays are Backward Compatible
            ASSERT_EQ(0, Lists::IListExtensions::GetElementOrDefault<int>(nullList, 1));
            ASSERT_FALSE(Lists::IListExtensions::TryGetElement<int>(nullList, 1, element));
            ASSERT_EQ(0, element);
        }

        {
            auto array = std::vector{1, 2, 3};
            ASSERT_EQ(3, Lists::IListExtensions::GetElementOrDefault<int>(array, 2));
            int element;
            ASSERT_TRUE(Lists::IListExtensions::TryGetElement<int>(array, 2, element));
            ASSERT_EQ(3, element);
            ASSERT_EQ(0, Lists::IListExtensions::GetElementOrDefault<int>(array, 10));
            ASSERT_FALSE(Lists::IListExtensions::TryGetElement<int>(array, 10, element));
            ASSERT_EQ(0, element);
        }
    }

    class StringTests : public ::testing::Test
    {
        protected:
            void SetUp() override
            {
            }
    };

    TEST_F(StringTests, CapitalizeFirstLetterTest)
    {
        ASSERT_EQ("Hello", StringExtensions::CapitalizeFirstLetter("hello"));
        ASSERT_EQ("Hello", StringExtensions::CapitalizeFirstLetter("Hello"));
        ASSERT_EQ("  Hello", StringExtensions::CapitalizeFirstLetter("  hello"));
    }

    TEST_F(StringTests, TrimSingleTest)
    {
        ASSERT_EQ("", StringExtensions::TrimSingle("'", '\''));
        ASSERT_EQ("", StringExtensions::TrimSingle("''", '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("'hello'", '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("hello'", '\''));
        ASSERT_EQ("hello", StringExtensions::TrimSingle("'hello", '\''));
    }
}// namespace Platform::Collections::Tests
