namespace Platform::Collections::Arrays
{
    template<typename...>
    class ArrayFiller;
    template<System::Array TArray, typename TReturnConstant>
    class ArrayFiller<TArray, TReturnConstant> : public ArrayFiller<TArray>
    {
        using TElement = std::ranges::range_value_t<TArray>;
        using base = ArrayFiller<TArray>;

    protected:
        TReturnConstant _returnConstant;

    public:
        ArrayFiller(TArray& array, std::int64_t offset, TReturnConstant returnConstant)
            : ArrayFiller<TArray>(array, offset)
        {
            _returnConstant = returnConstant;
        }

    public:
        ArrayFiller(TArray& array, TReturnConstant returnConstant)
            : ArrayFiller(array, 0, returnConstant)
        {
        }

    public:
        TReturnConstant AddAndReturnConstant(TElement element)
        {
            return Arrays::AddAndReturnConstant(base::_array, base::_position, element, _returnConstant);
        }

    public:
        TReturnConstant AddFirstAndReturnConstant(const System::Array<TElement> auto& elements)
        {
            return Arrays::AddFirstAndReturnConstant(base::_array, base::_position, elements, _returnConstant);
        }

    public:
        TReturnConstant AddAllAndReturnConstant(const System::Array<TElement> auto& elements)
        {
            return Arrays::AddAllAndReturnConstant(base::_array, base::_position, elements, _returnConstant);
        }

    public:
        TReturnConstant AddSkipFirstAndReturnConstant(const System::Array<TElement> auto& elements)
        {
            return Arrays::AddSkipFirstAndReturnConstant(base::_array, base::_position, elements, _returnConstant);
        }
    };

    template<System::Array TArray, typename TReturnConstant>
    requires (!std::integral<TReturnConstant>)
    ArrayFiller(TArray, TReturnConstant) -> ArrayFiller<TArray, TReturnConstant>;

    template<System::Array TArray, typename TReturnConstant>
    ArrayFiller(TArray, std::integral auto, TReturnConstant) -> ArrayFiller<TArray, TReturnConstant>;

}// namespace Platform::Collections::Arrays
