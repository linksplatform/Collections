namespace Platform::Collections::Arrays
{
    template<typename...>
    class ArrayFiller;
    template<typename TElement, System::Array TArray, typename TReturnConstant>
    class ArrayFiller<TElement, TArray, TReturnConstant> : public ArrayFiller<TElement, TArray>
    {
        //using TElement = std::ranges::range_value_t<TArray>;
        using base = ArrayFiller<TArray>;

    protected:
        TReturnConstant _returnConstant;

    public:
        ArrayFiller(TArray& array, std::int64_t offset, TReturnConstant returnConstant) :
            ArrayFiller<TArray>(array, offset)
        {
            _returnConstant = returnConstant;
        }

    public:
        ArrayFiller(TArray& array, TReturnConstant returnConstant) :
            ArrayFiller(array, 0, returnConstant)
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

    namespace Generators
    {
        template<typename TReturnConstant>
        static auto ArrayFiller(System::Array auto& array, std::int64_t offset, TReturnConstant constant)
        {
            using TArray = decltype(array);
            using TElement = typename System::Common::Array<TArray>::TItem;

            return Platform::Collections::Arrays::ArrayFiller<TElement, TArray, TReturnConstant>(array, offset, constant);
        }

        template<typename TReturnConstant>
        static auto ArrayFiller(System::Array auto& array, TReturnConstant constant)
        {
            using TArray = decltype(array);
            using TElement = typename System::Common::Array<TArray>::TItem;

            return Platform::Collections::Arrays::ArrayFiller<TElement, TArray, TReturnConstant>(array, 0, constant);
        }
    }// namespace Generators

}// namespace Platform::Collections::Arrays
