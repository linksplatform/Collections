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
            _array = std::span<TElement>(array);
            _position = offset;
        }

    public:
        ArrayFiller(Platform::Collections::System::Array auto& array)
            : ArrayFiller(array, 0)
        {
        }

    public:
        void Add(TElement element)
        {
            _array[_position++] = element;
        }

    public:
        bool AddAndReturnTrue(TElement element)
        {
            return Arrays::AddAndReturnConstant(_array, _position, element, true);
        }

    public:
        bool AddFirstAndReturnTrue(const Platform::Collections::System::Array auto& elements)
        {
            return Arrays::AddFirstAndReturnConstant(_array, _position, elements, true);
        }

    public:
        bool AddAllAndReturnTrue(const Platform::Collections::System::Array auto& elements)
        {
            return Arrays::AddAllAndReturnConstant(_array, _position, elements, true);
        }

    public:
        bool AddSkipFirstAndReturnTrue(const Platform::Collections::System::Array auto& elements)
        {
            return Arrays::AddSkipFirstAndReturnConstant(_array, _position, elements, true);
        }
    };
}// namespace Platform::Collections::Arrays
