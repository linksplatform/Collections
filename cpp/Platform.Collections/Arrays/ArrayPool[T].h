namespace Platform::Collections::Arrays
{
    template <typename ...> class ArrayPool;
    template <typename T> class ArrayPool<T>
    {
        private: static ArrayPool<T> _threadInstance;
        static ArrayPool<T> ThreadInstance() { return _threadInstance ?? (_threadInstance = ArrayPool<T>()); }

        private: std::int32_t _maxArraysPerSize = 0;
        private: readonly Dictionary<std::int64_t, Stack<T[]>> _pool = Dictionary<std::int64_t, Stack<T[]>>(ArrayPool.DefaultSizesAmount);

        public: ArrayPool(std::int32_t maxArraysPerSize) { _maxArraysPerSize = maxArraysPerSize; }

        public: ArrayPool() : this(ArrayPool.DefaultMaxArraysPerSize) { }

        public: Disposable<T[]> AllocateDisposable(std::int64_t size) { return {Allocate(size}, Free); }

        public: Disposable<T[]> Resize(Disposable<T[]> source, std::int64_t size)
        {
            auto destination = AllocateDisposable(size);
            T sourceArray[] = source;
            if (!sourceArray.IsNullOrEmpty())
            {
                T destinationArray[] = destination;
                Array.Copy(sourceArray, destinationArray, size < sourceArray.LongLength ? size : sourceArray.LongLength);
                source.Dispose();
            }
            return destination;
        }

        public: virtual void Clear() { return _pool.Clear(); }

        public: virtual T Allocate[](std::int64_t size) { return size <= 0L ? Array.Empty<T>() : _pool.GetOrDefault(size)?.PopOrDefault() ?? T[size]; }

        public: virtual void Free(T array[])
        {
            if (array.IsNullOrEmpty())
            {
                return;
            }
            auto stack = _pool.GetOrAdd(array.LongLength, size => Stack<T[]>(_maxArraysPerSize));
            if (stack.Count() == _maxArraysPerSize)
            {
                return;
            }
            stack.Push(array);
        }
    };
}