namespace Platform::Collections::Arrays
{
    template <typename ...> class ArrayString;
    template <typename T> class ArrayString<T> : public Segment<T>
    {
        public: ArrayString(std::int32_t length) : Segment(T[length], 0, length) { }

        public: ArrayString(T array[]) : Segment(array, 0, array.Length) { }

        public: ArrayString(T array[], std::int32_t length) : Segment(array, 0, length) { }
    };
}
