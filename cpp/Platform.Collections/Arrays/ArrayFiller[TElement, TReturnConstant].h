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
        ArrayFiller(Platform::Collections::System::Array<TElement> auto& array, std::int64_t offset, TReturnConstant returnConstant)
            : ArrayFiller<TElement>(array, offset)
        {
            _returnConstant = returnConstant;
        }

        ArrayFiller(Platform::Collections::System::Array<TElement> auto& array, TReturnConstant returnConstant)
            : ArrayFiller(array, 0, returnConstant)
        {
        }

        TReturnConstant AddAndReturnConstant(TElement element)
        {
            return GenericArrayExtensions::AddAndReturnConstant<TElement>(base::_array, base::_position, element, _returnConstant);
        }

        TReturnConstant AddFirstAndReturnConstant(Platform::Collections::System::Array<TElement> auto& elements)
        {
            return GenericArrayExtensions::AddFirstAndReturnConstant<TElement>(base::_array, base::_position, elements, _returnConstant);
        }

        TReturnConstant AddAllAndReturnConstant(Platform::Collections::System::Array<TElement> auto& elements)
        {
            return GenericArrayExtensions::AddAllAndReturnConstant<TElement>(base::_array, base::_position, elements, _returnConstant);
        }

        TReturnConstant AddSkipFirstAndReturnConstant(Platform::Collections::System::Array<TElement> auto& elements)
        {
            return GenericArrayExtensions::AddSkipFirstAndReturnConstant<TElement>(base::_array, base::_position, elements, _returnConstant);
        }
    };
}// namespace Platform::Collections::Arrays
