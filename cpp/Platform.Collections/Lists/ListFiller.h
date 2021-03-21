namespace Platform::Collections::Lists
{
    template <typename ...> class ListFiller;
    template <typename TElement, typename TReturnConstant> class ListFiller<TElement, TReturnConstant>
    {
        protected: List<TElement> _list;
        protected: TReturnConstant _returnConstant = 0;

        public: ListFiller(List<TElement> list, TReturnConstant returnConstant)
        {
            _list = list;
            _returnConstant = returnConstant;
        }

        public: ListFiller(List<TElement> list) : this(list, 0) { }

        public: void Add(TElement element) { _list.Add(element); }

        public: bool AddAndReturnTrue(TElement element) { return _list.AddAndReturnTrue(element); }

        public: bool AddFirstAndReturnTrue(IList<TElement> &elements) { return _list.AddFirstAndReturnTrue(elements); }

        public: bool AddAllAndReturnTrue(IList<TElement> &elements) { return _list.AddAllAndReturnTrue(elements); }

        public: bool AddSkipFirstAndReturnTrue(IList<TElement> &elements) { return _list.AddSkipFirstAndReturnTrue(elements); }
        
        public: TReturnConstant AddAndReturnConstant(TElement element)
        {
            _list.Add(element);
            return _returnConstant;
        }

        public: TReturnConstant AddFirstAndReturnConstant(IList<TElement> &elements)
        {
            _list.AddFirst(elements);
            return _returnConstant;
        }

        public: TReturnConstant AddAllAndReturnConstant(IList<TElement> &elements)
        {
            _list.AddAll(elements);
            return _returnConstant;
        }

        public: TReturnConstant AddSkipFirstAndReturnConstant(IList<TElement> &elements)
        {
            _list.AddSkipFirst(elements);
            return _returnConstant;
        }
    };
}
