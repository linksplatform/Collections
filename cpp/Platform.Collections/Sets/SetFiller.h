namespace Platform::Collections::Sets
{
    template<typename...>
    class SetFiller;
    template<Interfaces::ISet TSet, typename TReturnConstant>
    class SetFiller<TSet, TReturnConstant>
    {
        using TElement = std::ranges::range_value_t<TSet>;

        protected: TSet& _set;
        protected: TReturnConstant _returnConstant{};

        public: SetFiller(TSet& set, TReturnConstant returnConstant)
            : _set(set), _returnConstant(returnConstant)
        {
        }

        public: explicit SetFiller(TSet& set)
            : SetFiller(set, {})
        {
        }

        public:  void Add(TElement element)
        {
            _set.insert(element);
        }

        public:  bool AddAndReturnTrue(TElement element)
        {
            return Sets::AddAndReturnTrue(_set, element);
        }

        public:  bool AddFirstAndReturnTrue(Interfaces::IArray<TElement> auto&& elements)
        {
            return Sets::AddFirstAndReturnTrue(_set, elements);
        }

        public:  bool AddAllAndReturnTrue(Interfaces::IArray<TElement> auto&& elements)
        {
            return Sets::AddAllAndReturnTrue(_set, elements);
        }

        public: bool AddSkipFirstAndReturnTrue(Interfaces::IArray<TElement> auto&& elements)
        {
            return Sets::AddSkipFirstAndReturnTrue(_set, elements);
        }

        public: TReturnConstant AddAndReturnConstant(TElement element)
        {
            _set.insert(element);
            return _returnConstant;
        }

        public: TReturnConstant AddFirstAndReturnConstant(Interfaces::IArray<TElement> auto&& elements)
        {
            Sets::AddFirst(_set, elements);
            return _returnConstant;
        }

        public: TReturnConstant AddAllAndReturnConstant(Interfaces::IArray<TElement> auto&& elements)
        {
            Sets::AddAll(_set, elements);
            return _returnConstant;
        }

        public: TReturnConstant AddSkipFirstAndReturnConstant(Interfaces::IArray<TElement> auto&& elements)
        {
            Sets::AddSkipFirst(_set, elements);
            return _returnConstant;
        }
    };

    template<typename TSet, typename TReturnConstant>
    SetFiller(TSet, TReturnConstant) -> SetFiller<TSet, TReturnConstant>;

}// namespace Platform::Collections::Sets
