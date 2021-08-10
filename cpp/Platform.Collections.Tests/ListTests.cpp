namespace Platform::Collections::Tests
{
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
}
