namespace Platform::Collections::Segments
{
    template <typename ...> class Segment;
    template <typename T> class Segment<T>
    {
        public: T* Base;
        public: const std::int32_t Offset;
        public: const std::int32_t Length;



        public: Segment(Array<T> auto& base, std::int32_t offset, std::int32_t length) :
            Base(base.data()),
            Offset(offset),
            Length(length) {}


        public: virtual bool operator==(Segment<T> other)
        {
            for(int i = 0; i < Length; i++) {
                if(operator[](i) != other.operator[](i)) {
                    return false;
                }
            }
            return true;
        }

        public: T& operator[](std::int32_t i) {
            return Base[Offset + i];
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
            int index = Offset;
            for(; index < Length; index++) {
                if(operator[](index) == item) {
                    return index;
                }
            }
            return -1;
        }

        public: void Insert(std::int32_t index, T item) { throw std::logic_error("Not supported exception."); }

        public: void RemoveAt(std::int32_t index) { throw std::logic_error("Not supported exception."); }

        public: void Add(T item) { throw std::logic_error("Not supported exception."); }

        public: void Clear() { throw std::logic_error("Not supported exception."); }

        public: bool Contains(T item) { return this->IndexOf(item) >= 0; }

        public: void CopyTo(Array<T> auto& array, std::int32_t arrayIndex)
        {
            std::copy(begin(), end(), array.data() + arrayIndex);
        }

        public: bool Remove(T item) { throw std::logic_error("Not supported exception."); }



        auto begin() {
            return Base + Offset;
        }

        auto end() {
            return begin() + Length;
        }
    };
}


// TODO I can't do something about it
namespace std
{
    template <typename T>
    struct hash<Platform::Collections::Segments::Segment<T>>
    {
        std::size_t operator()(Platform::Collections::Segments::Segment<int> obj) const
        {
            // Paste from C#(.NET) Standart Library
            int hashAccumulator = 17;
            for (int i = 0; i < obj.Count(); i++)
            {
                hashAccumulator = (hashAccumulator * 23) + hash<T>{}(obj[i]);
                // override hash for type T
            }
            return hashAccumulator;
        }
    };
}
