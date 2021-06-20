namespace Platform::Collections::Arrays
{
    template<typename...>
    class ArrayFiller;
    template<Interfaces::IArray TArray>
    class ArrayFiller<TArray>
    {
        using TElement = typename Interfaces::Array<TArray>::Item;

    protected:
        TArray& _array;
        std::int64_t _position = 0;

    public:
        ArrayFiller(TArray& array, std::int64_t offset) :
            _array(array), _position(offset)
        {
        }

    public:
        explicit ArrayFiller(TArray& array) :
            ArrayFiller(array, 0)
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
        bool AddFirstAndReturnTrue(const Interfaces::IArray<TElement> auto& elements)
        {
            return Arrays::AddFirstAndReturnConstant(_array, _position, elements, true);
        }

    public:
        bool AddAllAndReturnTrue(const Interfaces::IArray<TElement> auto& elements)
        {
            return Arrays::AddAllAndReturnConstant(_array, _position, elements, true);
        }

    public:
        bool AddSkipFirstAndReturnTrue(const Interfaces::IArray<TElement> auto& elements)
        {
            return Arrays::AddSkipFirstAndReturnConstant(_array, _position, elements, true);
        }
    };
}// namespace Platform::Collections::Arrays
