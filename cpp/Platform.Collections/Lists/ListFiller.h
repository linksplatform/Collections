namespace Platform::Collections::Lists
{
    template<typename...> class ListFiller;
    template<Interfaces::CList TList, typename TReturnConstant> class ListFiller<TList, TReturnConstant>
    {
        protected: TList& _list;

        protected: TReturnConstant _returnConstant;

        public: ListFiller(TList& list, TReturnConstant returnConstant) : _list(list), _returnConstant(returnConstant) { }

        public: explicit ListFiller(TList& list) : ListFiller(list, {}) { }

        public: void Add(auto&& element) { _list.push_back(std::forward<decltype(element)>(element)); }

        public: bool AddAndReturnTrue(auto&& element) { return Lists::AddAndReturnTrue(_list, std::forward<decltype(element)>(element)); }

        public: bool AddFirstAndReturnTrue(Interfaces::CArray auto&& elements) { return Lists::AddFirstAndReturnTrue(_list, elements); }

        public: bool AddAllAndReturnTrue(Interfaces::CArray auto&& elements){ return Lists::AddAllAndReturnTrue(_list, elements); }

        public: bool AddSkipFirstAndReturnTrue(Interfaces::CArray auto&& elements) { return Lists::AddSkipFirstAndReturnTrue(_list, elements); }

        public: TReturnConstant AddAndReturnConstant(auto&& element)
        {
            Add(element);
            return _returnConstant;
        }

        public: TReturnConstant AddFirstAndReturnConstant(Interfaces::CArray auto&& elements)
        {
            Lists::AddFirst(_list, elements);
            return _returnConstant;
        }

        public: TReturnConstant AddAllAndReturnConstant(Interfaces::CArray auto&& elements)
        {
            Lists::AddAll(_list, elements);
            return _returnConstant;
        }

        public: TReturnConstant AddSkipFirstAndReturnConstant(Interfaces::CArray auto&& elements)
        {
            Lists::AddSkipFirst(_list, elements);
            return _returnConstant;
        }
    };

    template<typename TList, typename TReturnConstant>
    ListFiller(TList, TReturnConstant) -> ListFiller<TList, TReturnConstant>;
    
}
