namespace Platform::Collections::Arrays
{
    template <typename ...> class ArrayFiller;
    template <typename TElement, typename TReturnConstant> class ArrayFiller<TElement, TReturnConstant> : public ArrayFiller<TElement>
    {
        protected: TReturnConstant _returnConstant;

        public: ArrayFiller(Array<TElement> auto& array, std::int64_t offset, TReturnConstant returnConstant) : ArrayFiller<TElement>(array, offset) { return _returnConstant = returnConstant; }

        public: ArrayFiller(Array<TElement> auto& array, TReturnConstant returnConstant) : ArrayFiller(array, 0, returnConstant) { }

        // TODO 'ArrayFiller<TElement>::' пишется, потому что те поля 'protected'
        public: TReturnConstant AddAndReturnConstant(TElement element) { return GenericArrayExtensions::AddAndReturnConstant<TElement>(ArrayFiller<TElement>::_array, ArrayFiller<TElement>::_position, element, _returnConstant); }

        public: TReturnConstant AddFirstAndReturnConstant(IList<TElement> &elements) { return GenericArrayExtensions::AddFirstAndReturnConstant<TElement>(ArrayFiller<TElement>::_array, ArrayFiller<TElement>::_position, elements, _returnConstant); }

        public: TReturnConstant AddAllAndReturnConstant(IList<TElement> &elements) { return _array.AddAllAndReturnConstant(ref _position, elements, _returnConstant); }

        public: TReturnConstant AddSkipFirstAndReturnConstant(IList<TElement> &elements) { return _array.AddSkipFirstAndReturnConstant(ref _position, elements, _returnConstant); }
    };
}
