namespace Platform::Collections::Sets
{
    template <typename ...> class SetFiller;
    template <typename TElement, typename TReturnConstant> class SetFiller<TElement, TReturnConstant>
    {
        protected: ISet<TElement> *_set;
        protected: TReturnConstant _returnConstant = 0;

        public: SetFiller(ISet<TElement> &set, TReturnConstant returnConstant)
        {
            _set = set;
            _returnConstant = returnConstant;
        }

        public: SetFiller(ISet<TElement> &set) : this(set, 0) { }

        public: void Add(TElement element) { _set.Add(element); }

        public: bool AddAndReturnTrue(TElement element) { return _set.AddAndReturnTrue(element); }

        public: bool AddFirstAndReturnTrue(IList<TElement> &elements) { return _set.AddFirstAndReturnTrue(elements); }

        public: bool AddAllAndReturnTrue(IList<TElement> &elements) { return _set.AddAllAndReturnTrue(elements); }

        public: bool AddSkipFirstAndReturnTrue(IList<TElement> &elements) { return _set.AddSkipFirstAndReturnTrue(elements); }

        public: TReturnConstant AddAndReturnConstant(TElement element)
        {
            _set.Add(element);
            return _returnConstant;
        }

        public: TReturnConstant AddFirstAndReturnConstant(IList<TElement> &elements)
        {
            _set.AddFirst(elements);
            return _returnConstant;
        }

        public: TReturnConstant AddAllAndReturnConstant(IList<TElement> &elements)
        {
            _set.AddAll(elements);
            return _returnConstant;
        }

        public: TReturnConstant AddSkipFirstAndReturnConstant(IList<TElement> &elements)
        {
            _set.AddSkipFirst(elements);
            return _returnConstant;
        }
    };
}
