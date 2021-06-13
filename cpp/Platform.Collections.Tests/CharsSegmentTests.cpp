#include <gtest/gtest.h>

namespace Platform::Collections::Tests
{
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
}
