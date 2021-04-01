namespace Platform::Collections::Arrays
{

    template <typename ...> class ArrayFiller;
    template <typename TElement> class ArrayFiller<TElement>
    {
        // Можно также использовать TElement*
        protected: std::span<TElement> _array;
        protected: std::int64_t _position = 0;

        public: ArrayFiller(Platform::Collections::System::Array<TElement> auto& array, std::int64_t offset)
        {
            _array = std::span<TElement>(array.begin(), array.size());
            _position = offset;
        }

        public: ArrayFiller(Platform::Collections::System::Array<TElement> auto& array) : ArrayFiller(array, 0) { }


        public: void Add(TElement element)
        {
            _array[_position++] = element;
        }

        public: bool AddAndReturnTrue(TElement element)
        {
            return GenericArrayExtensions::AddAndReturnConstant<TElement>(_array, _position, element, true);
        }

        public: bool AddFirstAndReturnTrue(const Platform::Collections::System::BaseArray<TElement> auto& elements)
        {
            return GenericArrayExtensions::AddFirstAndReturnConstant<TElement>(_array, _position, elements, true);
        }
        
        public: bool AddAllAndReturnTrue(const Platform::Collections::System::Array<TElement> auto& elements)
        {
            return GenericArrayExtensions::AddAllAndReturnConstant<TElement>(_array, _position, elements, true);
        }

        public: bool AddSkipFirstAndReturnTrue(const Platform::Collections::System::Array<TElement> auto& elements)
        {
            return GenericArrayExtensions::AddSkipFirstAndReturnConstant<TElement>(_array, _position, elements, true);
        }
    };
}
