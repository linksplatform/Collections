namespace Platform::Collections::Segments
{
    class CharSegment : public Segment<char>
    {
        public: CharSegment(IList<char> base, std::int32_t offset, std::int32_t length) : base(base, offset, length) { }

        public: override std::int32_t GetHashCode()
        {
            if (Base is char baseArray[])
            {
                return baseArray.GenerateHashCode(Offset, Length);
            }
            else
            {
                return this.GenerateHashCode();
            }
        }

        public: bool Equals(Segment<char> other) override
        {
            auto contentEqualityComparer = [&]() -> bool {
                if (Base is char baseArray[] && other.Base is char otherArray[])
                {
                    return baseArray.ContentEqualTo(Offset, Length, otherArray, other.Offset);
                };
                else
                {
                    return left.ContentEqualTo(right);
                }
            }
            return this.EqualTo(other, contentEqualityComparer);
        }

        public: operator std::string() const
        {
            if (!(this->Base is char array[]))
            {
                array = segment.Base.ToArray();
            }
            return std::string(array, this->Offset, this->Length);
        }

        public: override std::string ToString() { return this; }
    };
}
