namespace Platform::Collections::Tests
{
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
}
