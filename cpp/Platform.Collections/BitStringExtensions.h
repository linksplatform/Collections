namespace BitStringExtensions
{
    template<std::size_t Size>
    static void SetRandomBits(std::bitset<Size>& string)
    {
        for (int i = 0; i < string.size(); i++)
        {
            // FIXME use NextBool() from Platform.Random [wait]
            bool value = rand() % 2;
            string.set(i, value);
        }
    }
}// namespace BitStringExtensions