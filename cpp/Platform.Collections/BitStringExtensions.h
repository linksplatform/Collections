namespace Platform::Collections
{
    class BitStringExtensions
    {
        public: static void SetRandomBits(BitString std::string)
        {
            for (auto i = 0; i < std::string.Length; i++)
            {
                auto value = RandomHelpers.Default.NextBoolean();
                std::string.Set(i, value);
            }
        }
    };
}
