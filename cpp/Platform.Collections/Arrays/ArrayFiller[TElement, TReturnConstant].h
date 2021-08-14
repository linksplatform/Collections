namespace Platform::Collections::Arrays
{
    template<typename...> class ArrayFiller;
    template<Interfaces::IArray TArray, typename TReturnConstant> class ArrayFiller<TArray, TReturnConstant> : public ArrayFiller<TArray>
    {
        using base = ArrayFiller<TArray>;

        protected: TReturnConstant _returnConstant;

        public: ArrayFiller(TArray& array, std::int64_t offset, TReturnConstant returnConstant) : ArrayFiller<TArray>(array, offset), _returnConstant(returnConstant) { }

        public: ArrayFiller(TArray& array, TReturnConstant returnConstant) : ArrayFiller(array, 0, returnConstant) { }

        public: TReturnConstant AddAndReturnConstant(auto&& element){ return Arrays::AddAndReturnConstant(base::_array, base::_position, std::forward<decltype(element)>(element), _returnConstant); }

        public: TReturnConstant AddFirstAndReturnConstant(Interfaces::IArray<TElement> auto&& elements) { return Arrays::AddFirstAndReturnConstant(base::_array, base::_position, elements, _returnConstant); }

        public: TReturnConstant AddAllAndReturnConstant(Interfaces::IArray<TElement> auto&& elements) { return Arrays::AddAllAndReturnConstant(base::_array, base::_position, elements, _returnConstant); }

        public: TReturnConstant AddSkipFirstAndReturnConstant(Interfaces::IArray<TElement> auto&& elements) { return Arrays::AddSkipFirstAndReturnConstant(base::_array, base::_position, elements, _returnConstant); }
    };
}
