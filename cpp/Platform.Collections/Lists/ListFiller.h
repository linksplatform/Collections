namespace Platform::Collections::Lists
{
    template<typename...>
    class ListFiller;
    template<System::IList TList, typename TReturnConstant>
    class ListFiller<TList, TReturnConstant>
    {
        using TElement = std::ranges::range_value_t<TList>;

    protected:
        TList& _list;

    protected:
        TReturnConstant _returnConstant;

    public:
        ListFiller(TList& list, TReturnConstant returnConstant)
            : _list(list)
        {
            _returnConstant = returnConstant;
        }

    public:
        ListFiller(TList& list)
            : ListFiller(list, TReturnConstant{})
        {
        }

    public:
        void Add(TElement element)
        {
            _list.push_back(element);
        }

    public:
        bool AddAndReturnTrue(TElement element)
        {
            return Lists::AddAndReturnTrue(_list, element);
        }

    public:
        bool AddFirstAndReturnTrue(const System::IArray<TElement> auto& elements)
        {
            return Lists::AddFirstAndReturnTrue(_list, elements);
        }

    public:
        bool AddAllAndReturnTrue(const System::IArray<TElement> auto& elements)
        {
            return Lists::AddAllAndReturnTrue(_list, elements);
        }

    public:
        bool AddSkipFirstAndReturnTrue(const System::IArray<TElement> auto& elements)
        {
            return Lists::AddSkipFirstAndReturnTrue(_list, elements);
        }

    public:
        TReturnConstant AddAndReturnConstant(TElement element)
        {
            _list.push_back(element);
            return _returnConstant;
        }

    public:
        TReturnConstant AddFirstAndReturnConstant(const System::IArray<TElement> auto& elements)
        {
            Lists::AddFirst(_list, elements);
            return _returnConstant;
        }

    public:
        TReturnConstant AddAllAndReturnConstant(const System::IArray<TElement> auto& elements)
        {
            Lists::AddAll(_list, elements);
            return _returnConstant;
        }

    public:
        TReturnConstant AddSkipFirstAndReturnConstant(const System::IArray<TElement> auto& elements)
        {
            Lists::AddSkipFirst(_list, elements);
            return _returnConstant;
        }
    };

    template<typename TList, typename TReturnConstant>
    ListFiller(TList, TReturnConstant) -> ListFiller<TList, TReturnConstant>;
    
}// namespace Platform::Collections::Lists
