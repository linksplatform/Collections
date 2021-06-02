#include <gtest/gtest.h>
#include <Platform.Collections.h>

namespace Platform::Collections::Tests
{
    TEST(ConceptsTest, Arrays)
    {
        using vector = std::vector<int>;
        using array = std::array<int, 0>;

        ASSERT_TRUE(System::Array<vector>);
        ASSERT_TRUE(System::Array<array>);

        ASSERT_TRUE((System::Array<vector, int>) );
        ASSERT_TRUE((System::Array<array, int>) );
    }

    TEST(ConceptsTest, Lists)
    {
        using vector = std::vector<int>;
        using array = std::array<int, 0>;

        ASSERT_TRUE(System::IList<vector>);
        ASSERT_FALSE(System::IList<array>);

        ASSERT_TRUE((System::IList<vector, int>) );
    }
}// namespace Platform::Collections::Tests