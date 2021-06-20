#include <gtest/gtest.h>
#include <Platform.Collections.h>

namespace Platform::Collections::Tests
{
    TEST(ConceptsTest, Arrays)
    {
        using vector = std::vector<int>;
        using array = std::array<int, 0>;

        ASSERT_TRUE(Interfaces::IArray<vector>);
        ASSERT_TRUE(Interfaces::IArray<array>);

        ASSERT_TRUE((Interfaces::IArray<vector, int>));
        ASSERT_TRUE((Interfaces::IArray<array, int>));
    }

    TEST(ConceptsTest, Lists)
    {
        using vector = std::vector<int>;
        using array = std::array<int, 0>;

        ASSERT_TRUE(Interfaces::IList<vector>);
        ASSERT_FALSE(Interfaces::IList<array>);

        ASSERT_TRUE((Interfaces::IList<vector, int>));
    }
}// namespace Platform::Collections::Tests