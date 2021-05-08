namespace Platform::Collections::Arrays
{
    template<typename...>
    class ArrayFiller;
    template<typename TElement>
    class ArrayFiller<TElement>
    {
    protected:
        std::span<TElement> _array;
        std::int64_t _position = 0;

    public:
        ArrayFiller(Platform::Collections::System::Array auto& array, std::int64_t offset)
        {
            _array = std::span<TElement>(std::ranges::begin(array), std::ranges::size(array));
            _position = offset;
        }

        ArrayFiller(Platform::Collections::System::Array auto& array)
            : ArrayFiller(array, 0)
        {
        }

        void Add(TElement element)
        {
            _array[_position++] = element;
        }

        bool AddAndReturnTrue(TElement element)
        {
            return GenericArrayExtensions::AddAndReturnConstant<TElement>(_array, _position, element, true);
        }

        bool AddFirstAndReturnTrue(const Platform::Collections::System::Array auto& elements)
        {
            return GenericArrayExtensions::AddFirstAndReturnConstant<TElement>(_array, _position, elements, true);
        }

        bool AddAllAndReturnTrue(const Platform::Collections::System::Array auto& elements)
        {
            return GenericArrayExtensions::AddAllAndReturnConstant<TElement>(_array, _position, elements, true);
        }

        bool AddSkipFirstAndReturnTrue(const Platform::Collections::System::Array auto& elements)
        {
            return GenericArrayExtensions::AddSkipFirstAndReturnConstant<TElement>(_array, _position, elements, true);
        }
    };
}// namespace Platform::Collections::Arrays
