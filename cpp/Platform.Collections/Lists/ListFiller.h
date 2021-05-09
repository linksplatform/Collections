namespace Platform::Collections::Lists
{
    template<typename...>
    class ListFiller;
    template<typename TElement, Platform::Collections::System::IList<TElement> TList, typename TReturnConstant>
    requires std::default_initializable<TReturnConstant>
    class ListFiller<TElement, TReturnConstant, TList>
    {
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
            : ListFiller(list, TElement{})
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
        bool AddFirstAndReturnTrue(Platform::Collections::System::IList<TElement> auto elements)
        {
            return Lists::AddFirstAndReturnTrue(_list, elements);
        }

    public:
        bool AddAllAndReturnTrue(Platform::Collections::System::IList<TElement> auto elements)
        {
            return Lists::AddAllAndReturnTrue(_list, elements);
        }

    public:
        bool AddSkipFirstAndReturnTrue(Platform::Collections::System::IList<TElement> auto elements)
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
        TReturnConstant AddFirstAndReturnConstant(Platform::Collections::System::IList<TElement> auto elements)
        {
            Lists::AddFirst(_list, elements);
            return _returnConstant;
        }

    public:
        TReturnConstant AddAllAndReturnConstant(Platform::Collections::System::IList<TElement> auto elements)
        {
            Lists::AddAll(_list, elements);
            return _returnConstant;
        }

    public:
        TReturnConstant AddSkipFirstAndReturnConstant(Platform::Collections::System::IList<TElement> auto elements)
        {
            Lists::AddSkipFirst(_list, elements);
            return _returnConstant;
        }
    };
}// namespace Platform::Collections::Lists
