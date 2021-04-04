namespace Platform::Collections::Sets
{
    template <typename ...> class SetFiller;
    template <typename TElement, typename TReturnConstant, Platform::Collections::System::ISet<TElement> TSet>
    class SetFiller<TElement, TReturnConstant, TSet>
    {
        protected: TSet& _set;
        protected: TReturnConstant _returnConstant = 0;

        public: SetFiller(TSet& set, TReturnConstant returnConstant) : _set(set)
        {
            _returnConstant = returnConstant;
        }


        public: SetFiller(TSet& set) requires std::default_initializable<TReturnConstant> : SetFiller(set, TReturnConstant{}) { }

        public: void Add(TElement element) { _set.insert(element); }

        public: bool AddAndReturnTrue(TElement element) { return ISetExtensions::AddAndReturnTrue(_set, element); }

        public: bool AddFirstAndReturnTrue(Platform::Collections::System::IList<TElement> auto& elements)
        {
            return ISetExtensions::AddFirstAndReturnTrue(_set, elements);
        }

        public: bool AddAllAndReturnTrue(Platform::Collections::System::IList<TElement> auto& elements)
        {
            return ISetExtensions::AddAllAndReturnTrue(_set, elements);
        }

        public: bool AddSkipFirstAndReturnTrue(Platform::Collections::System::IList<TElement> auto& elements)
        {
            return ISetExtensions::AddSkipFirstAndReturnTrue(_set, elements);
        }

        public: TReturnConstant AddAndReturnConstant(TElement element)
        {
            _set.Add(element);
            return _returnConstant;
        }

        public: TReturnConstant AddFirstAndReturnConstant(Platform::Collections::System::IList<TElement> auto& elements)
        {
            _set.AddFirst(elements);
            return _returnConstant;
        }

        public: TReturnConstant AddAllAndReturnConstant(Platform::Collections::System::IList<TElement> auto& elements)
        {
            _set.AddAll(elements);
            return _returnConstant;
        }

        public: TReturnConstant AddSkipFirstAndReturnConstant(Platform::Collections::System::IList<TElement> auto& elements)
        {
            _set.AddSkipFirst(elements);
            return _returnConstant;
        }
    };
}
