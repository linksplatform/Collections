namespace Platform::Collections::Arrays
{
    template <typename ...> class ArrayFiller;
    template <typename TElement, typename TReturnConstant> class ArrayFiller<TElement, TReturnConstant> : public ArrayFiller<TElement>
    {
        protected: TReturnConstant _returnConstant = 0;

        public: ArrayFiller(TElement array[], std::int64_t offset, TReturnConstant returnConstant) : ArrayFiller<TElement>(array, offset) { return _returnConstant = returnConstant; }

        public: ArrayFiller(TElement array[], TReturnConstant returnConstant) : this(array, 0, returnConstant) { }

        public: TReturnConstant AddAndReturnConstant(TElement element) { return _array.AddAndReturnConstant(ref _position, element, _returnConstant); }

        public: TReturnConstant AddFirstAndReturnConstant(IList<TElement> &elements) { return _array.AddFirstAndReturnConstant(ref _position, elements, _returnConstant); }

        public: TReturnConstant AddAllAndReturnConstant(IList<TElement> &elements) { return _array.AddAllAndReturnConstant(ref _position, elements, _returnConstant); }

        public: TReturnConstant AddSkipFirstAndReturnConstant(IList<TElement> &elements) { return _array.AddSkipFirstAndReturnConstant(ref _position, elements, _returnConstant); }
    };
}
