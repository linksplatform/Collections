namespace Platform::Collections::Lists
{
    class CharIListExtensions
    {
        public: static std::int32_t GenerateHashCode(IList<char> &list)
        {
            auto hashSeed = 5381;
            auto hashAccumulator = hashSeed;
            for (auto i = 0; i < list.Count(); i++)
            {
                hashAccumulator = (hashAccumulator << 5) + hashAccumulator ^ list[i];
            }
            return hashAccumulator + (hashSeed * 1566083941);
        }

        public: static bool EqualTo(IList<char> &left, IList<char> &right) { return left.EqualTo(right, ContentEqualTo); }

        public: static bool ContentEqualTo(IList<char> &left, IList<char> &right)
        {
            for (auto i = left.Count() - 1; i >= 0; --i)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }
            return true;
        }
    };
}
