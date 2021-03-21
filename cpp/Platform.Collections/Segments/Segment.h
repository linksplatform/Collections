namespace Platform::Collections::Segments
{
    template <typename ...> class Segment;
    template <typename T> class Segment<T> : public IEquatable<Segment<T>>, IList<T>
    {
        public: const IList<T> *Base;
        public: const std::int32_t Offset;
        public: const std::int32_t Length;

        public: Segment(IList<T> base, std::int32_t offset, std::int32_t length)
        {
            Base = base;
            Offset = offset;
            Length = length;
        }

        public: virtual bool operator ==(const Segment<T> &other) const { return this.EqualTo(other); }

        public: T this[std::int32_t i]
        {
            get => Base[Offset + i];
            set => Base[Offset + i] = value;
        }

        public: std::int32_t Count()
        {
            return Length;
        }

        public: bool IsReadOnly()
        {
            return true;
        }

        public: std::int32_t IndexOf(T item)
        {
            auto index = Base.IndexOf(item);
            if (index >= Offset)
            {
                auto actualIndex = index - Offset;
                if (actualIndex < Length)
                {
                    return actualIndex;
                }
            }
            return -1;
        }

        public: void Insert(std::int32_t index, T item) { throw std::logic_error("Not supported exception."); }

        public: void RemoveAt(std::int32_t index) { throw std::logic_error("Not supported exception."); }

        public: void Add(T item) { throw std::logic_error("Not supported exception."); }

        public: void Clear() { throw std::logic_error("Not supported exception."); }

        public: bool Contains(T item) { return this->IndexOf(item) >= 0; }

        public: void CopyTo(T array[], std::int32_t arrayIndex)
        {
            for (auto i = 0; i < Length; i++)
            {
                array.Add(arrayIndex, this[i]);
            }
        }

        public: bool Remove(T item) { throw std::logic_error("Not supported exception."); }

        public: IEnumerator<T> GetEnumerator()
        {
            for (auto i = 0; i < Length; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
    };
}

namespace std
{
    template <typename T>
    struct hash<Platform::Collections::Segments::Segment<T>>
    {
        std::size_t operator()(const Platform::Collections::Segments::Segment<T> &obj) const
        {
            return this.GenerateHashCode();
        }
    };
}
