namespace Platform::Collections
{
    template<std::size_t Size>
    static void SetRandomBits(std::bitset<Size>& string)
    {
        using namespace Random;
        for (int i = 0; i < string.size(); i++)
        {
            bool value = NextBoolean(RandomHelpers::Default);
            string.set(i, value);
        }
    }
}