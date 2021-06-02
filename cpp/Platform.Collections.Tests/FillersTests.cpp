#include <gtest/gtest.h>

namespace Platform::Collections::Tests
{
    TEST(FillersTests, Arrays)
    {
        {
            std::array array{0, 0, 0, 0, 0, 0};
            auto filler1 = Arrays::ArrayFiller<decltype(array)>(array);

            filler1.Add(1);
            filler1.Add(2);
            filler1.Add(3);

            auto filler2 = Arrays::ArrayFiller<decltype(array)>(array, 3);

            filler2.Add(3);
            filler2.Add(2);
            filler2.Add(1);

            ASSERT_EQ(array, (std::array{1, 2, 3, 3, 2, 1}));
        }

        {
            std::array array{0, 0, 0, 0, 0, 0};

            auto filler1 = Arrays::ArrayFiller<decltype(array), std::string>(array, "accepted");

            ASSERT_EQ(filler1.AddAndReturnConstant(1), "accepted");
            ASSERT_EQ(filler1.AddAndReturnConstant(2), "accepted");
            ASSERT_EQ(filler1.AddAndReturnConstant(3), "accepted");

            auto filler2 = Arrays::ArrayFiller<decltype(array), std::string>(array, 3, "lol");

            ASSERT_EQ(filler2.AddAndReturnConstant(3), "lol");
            ASSERT_EQ(filler2.AddAndReturnConstant(2), "lol");
            ASSERT_EQ(filler2.AddAndReturnConstant(1), "lol");

            ASSERT_EQ(array, (std::array{1, 2, 3, 3, 2, 1}));
        }
    }

    TEST(FillersTests, Lists)
    {
        {
            std::vector<int> list{};
            auto filler = Lists::ListFiller(list, "const");

            ASSERT_EQ(filler.AddAndReturnConstant(1), "const");
            ASSERT_EQ(filler.AddAllAndReturnConstant(std::array{2, 3, 4}), "const");

            ASSERT_EQ(list, (std::vector{1, 2, 3, 4}));
        }
    }

    TEST(FillersTests, Sets)
    {
        {
            std::set<int> set{};
            auto filler = Sets::SetFiller(set, "const");

            ASSERT_EQ(filler.AddAndReturnConstant(1), "const");
            ASSERT_EQ((filler.AddAllAndReturnConstant(std::array{2, 3, 4})), "const");

            ASSERT_EQ(set, (std::set{1, 2, 3, 4}));
        }
    }
}// namespace Platform::Collections::Tests
