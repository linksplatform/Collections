namespace Platform::Collections::Arrays
{
    template<typename...>
    class ArrayFiller;
    template<System::Array TArray>
    class ArrayFiller<TArray>
    {
        using TElement = typename System::Common::Array<TArray>::TItem;

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
        bool AddFirstAndReturnTrue(const System::Array<TElement> auto& elements)
        {
            return Arrays::AddFirstAndReturnConstant(_array, _position, elements, true);
        }

    public:
        bool AddAllAndReturnTrue(const System::Array<TElement> auto& elements)
        {
            return Arrays::AddAllAndReturnConstant(_array, _position, elements, true);
        }

    public:
        bool AddSkipFirstAndReturnTrue(const System::Array<TElement> auto& elements)
        {
            return Arrays::AddSkipFirstAndReturnConstant(_array, _position, elements, true);
        }
    };
}// namespace Platform::Collections::Arrays
