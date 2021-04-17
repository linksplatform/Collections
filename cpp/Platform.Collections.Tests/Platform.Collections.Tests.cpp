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
            auto nullArray = std::array<int, 1>{};
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
    };


    class ChangeSegmentTests : public ::testing::Test
    {
    protected: void SetUp() override
        {
        }
    };

    // Делаем тесты, как будто сами его написали и хвастаемся, что работает
    TEST_F(ChangeSegmentTests, GetHashCodeEqualsTest)
    {
        static std::string testString = "test test";
        auto testArray = std::vector(testString.begin(), testString.end());
        auto firstHashCode = Platform::Hashing::Hash(std::span(testArray.begin(), 4));
        auto secondHashCode = Platform::Hashing::Hash(std::span(testArray.begin() + 5, 4));
        ASSERT_EQ(firstHashCode, secondHashCode);
    }

    TEST_F(ChangeSegmentTests, EqualsTest)
    {
        static std::string testString = "test test";
        auto testArray = std::vector(testString.begin(), testString.end());
        auto first = std::span(testArray.begin(), 4);
        auto second = std::span(testArray.begin() + 5, 4);
        ASSERT_TRUE(std::equal_to<std::span<char>>{}(first, second));
    }
}
