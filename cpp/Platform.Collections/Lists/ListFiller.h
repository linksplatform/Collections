namespace Platform::Collections::Lists
{
    template<typename...>
    class ListFiller;
    template<Interfaces::IList TList, typename TReturnConstant>
    class ListFiller<TList, TReturnConstant>
    {
        using TElement = std::ranges::range_value_t<TList>;

        protected: TList& _list;

        protected: TReturnConstant _returnConstant;

        public: ListFiller(TList& list, auto&& returnConstant)
            : _list(list), _returnConstant(std::forward<decltype(returnConstant)>(returnConstant))
        {
        }

        public: explicit ListFiller(TList& list)
            : ListFiller(list, {})
        {
        }

        public: void Add(TElement element)
        {
            _list.push_back(element);
        }

        public: bool AddAndReturnTrue(TElement element)
        {
            return Lists::AddAndReturnTrue(_list, element);
        }

        public: bool AddFirstAndReturnTrue(Interfaces::IArray<TElement> auto&& elements)
        {
            return Lists::AddFirstAndReturnTrue(_list, elements);
        }

        public: bool AddAllAndReturnTrue(Interfaces::IArray<TElement> auto&& elements)
        {
            return Lists::AddAllAndReturnTrue(_list, elements);
        }

        public: bool AddSkipFirstAndReturnTrue(Interfaces::IArray<TElement> auto&& elements)
        {
            return Lists::AddSkipFirstAndReturnTrue(_list, elements);
        }

        public: TReturnConstant AddAndReturnConstant(auto&& element)
        {
            _list.push_back(std::forward<decltype(element)>(element));
            return _returnConstant;
        }

        public: TReturnConstant AddFirstAndReturnConstant(Interfaces::IArray<TElement> auto&& elements)
        {
            Lists::AddFirst(_list, elements);
            return _returnConstant;
        }

        public: TReturnConstant AddAllAndReturnConstant(Interfaces::IArray<TElement> auto&& elements)
        {
            Lists::AddAll(_list, elements);
            return _returnConstant;
        }

        public: TReturnConstant AddSkipFirstAndReturnConstant(Interfaces::IArray<TElement> auto&& elements)
        {
            Lists::AddSkipFirst(_list, elements);
            return _returnConstant;
        }
    };

    template<typename TList, typename TReturnConstant>
    ListFiller(TList, TReturnConstant) -> ListFiller<TList, TReturnConstant>;
    
}// namespace Platform::Collections::Lists
