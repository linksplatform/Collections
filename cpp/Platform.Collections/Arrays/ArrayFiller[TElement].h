namespace Platform::Collections::Arrays
{
    template <typename ...> class ArrayFiller;
    template <typename TElement> class ArrayFiller<TElement>
    {
        protected: TElement _array[N] = { {0} };
        protected: std::int64_t _position = 0;

        public: ArrayFiller(TElement array[], std::int64_t offset)
        {
            _array = array;
            _position = offset;
        }

        public: ArrayFiller(TElement array[]) : this(array, 0) { }

        public: void Add(TElement element) { _array[_position++] = element; }

        public: bool AddAndReturnTrue(TElement element) { return _array.AddAndReturnConstant(ref _position, element, true); }

        public: bool AddFirstAndReturnTrue(IList<TElement> &elements) { return _array.AddFirstAndReturnConstant(ref _position, elements, true); }
        
        public: bool AddAllAndReturnTrue(IList<TElement> &elements) { return _array.AddAllAndReturnConstant(ref _position, elements, true); }

        public: bool AddSkipFirstAndReturnTrue(IList<TElement> &elements) { return _array.AddSkipFirstAndReturnConstant(ref _position, elements, true); }
    };
}
