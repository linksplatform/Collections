namespace Platform::Collections::Arrays
{
    class ArrayPool
    {
        public: inline static const std::int32_t DefaultSizesAmount = 512;
        public: inline static const std::int32_t DefaultMaxArraysPerSize = 32;

        public: static T Allocate[]<T>(std::int64_t size) { return ArrayPool<T>.ThreadInstance.Allocate(size); }
        
        public: template <typename T> static void Free(T array[]) { ArrayPool<T>.ThreadInstance.Free(array); }
    };
}