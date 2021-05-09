namespace Platform::Collections::Arrays
{
    template<typename...>
    class ArrayFiller;
    template<typename TElement, typename TReturnConstant>
    class ArrayFiller<TElement, TReturnConstant> : public ArrayFiller<TElement>
    {
        using base = ArrayFiller<TElement>;

    protected:
        TReturnConstant _returnConstant;

    public:
        ArrayFiller(Platform::Collections::System::Array auto& array, std::int64_t offset, TReturnConstant returnConstant)
            : ArrayFiller<TElement>(array, offset)
        {
            _returnConstant = returnConstant;
        }

    public:
        ArrayFiller(Platform::Collections::System::Array auto& array, TReturnConstant returnConstant)
            : ArrayFiller(array, 0, returnConstant)
        {
        }

    public:
        TReturnConstant AddAndReturnConstant(TElement element)
        {
            return Arrays::AddAndReturnConstant(base::_array, base::_position, element, _returnConstant);
        }

    public:
        TReturnConstant AddFirstAndReturnConstant(Platform::Collections::System::Array auto& elements)
        {
            return Arrays::AddFirstAndReturnConstant(base::_array, base::_position, elements, _returnConstant);
        }

    public:
        TReturnConstant AddAllAndReturnConstant(Platform::Collections::System::Array auto& elements)
        {
            return Arrays::AddAllAndReturnConstant(base::_array, base::_position, elements, _returnConstant);
        }

    public:
        TReturnConstant AddSkipFirstAndReturnConstant(Platform::Collections::System::Array auto& elements)
        {
            return Arrays::AddSkipFirstAndReturnConstant(base::_array, base::_position, elements, _returnConstant);
        }
    };
}// namespace Platform::Collections::Arrays
