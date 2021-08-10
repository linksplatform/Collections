namespace Platform::Collections::Tests
{
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
}
