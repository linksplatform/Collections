namespace Platform::Collections::Lists
{
    template <typename ...> class ListFiller;
    template <typename TElement, typename TReturnConstant, Platform::Collections::System::IList<TElement> TList> requires std::default_initializable<TReturnConstant>
    class ListFiller<TElement, TReturnConstant, TList>
    {
        // TODO ничего, кроме ссылки на сам список, не позволят адекватно делать 'push_back' (возможно ради сходства придётся стиль 'ArrayFiller' заменить)
        protected: TList& _list;
        protected: TReturnConstant _returnConstant;

        public: ListFiller(TList& list, TReturnConstant returnConstant) : _list(list) // такой вот конструктор может инициализировать константы и ссылки
        {
            _returnConstant = returnConstant;
        }

        public: ListFiller(TList& list) : ListFiller(list, TElement{}) {}

        public: void Add(TElement element)
        {
            _list.push_back(element);
        }

        public: bool AddAndReturnTrue(TElement element)
        {
            return IListExtensions::AddAndReturnTrue<TElement>(_list, element);
        }

        public: bool AddFirstAndReturnTrue(const Platform::Collections::System::IList<TElement> auto& elements)
        {
            return IListExtensions::AddFirstAndReturnTrue<TElement>(_list, elements);
        }

        public: bool AddAllAndReturnTrue(const Platform::Collections::System::IList<TElement> auto& elements)
        {
            return IListExtensions::AddAllAndReturnTrue<TElement>(_list, elements);
        }

        public: bool AddSkipFirstAndReturnTrue(const Platform::Collections::System::IList<TElement> auto& elements)
        {
            return IListExtensions::AddSkipFirstAndReturnTrue<TElement>(_list, elements);
        }
        
        public: TReturnConstant AddAndReturnConstant(TElement element)
        {
            _list.push_back(element);
            return _returnConstant;
        }

        public: TReturnConstant AddFirstAndReturnConstant(const Platform::Collections::System::IList<TElement> auto& elements)
        {
           IListExtensions::AddFirst<TElement>(_list, elements);
            return _returnConstant;
        }

        public: TReturnConstant AddAllAndReturnConstant(const Platform::Collections::System::IList<TElement> auto& elements)
        {
            IListExtensions::AddAll<TElement>(_list, elements);
            return _returnConstant;
        }

        public: TReturnConstant AddSkipFirstAndReturnConstant(const Platform::Collections::System::IList<TElement> auto& elements)
        {
            IListExtensions::AddSkipFirst<TElement>(_list, elements);
            return _returnConstant;
        }
    };
}
