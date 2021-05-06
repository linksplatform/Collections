namespace Platform::Collections::Sets
{
    template<typename...>
    class SetFiller;
    template<typename TElement, typename TReturnConstant, Platform::Collections::System::ISet<TElement> TSet>
    class SetFiller<TElement, TReturnConstant, TSet>
    {
    protected:
        TSet& _set;
        TReturnConstant _returnConstant = 0;

    public:
        SetFiller(TSet& set, TReturnConstant returnConstant)
            : _set(set)
        {
            _returnConstant = returnConstant;
        }

        SetFiller(TSet& set) requires std::default_initializable<TReturnConstant> : SetFiller(set, TReturnConstant{})
        {
        }

        void Add(TElement element)
        {
            _set.insert(element);
        }

        bool AddAndReturnTrue(TElement element)
        {
            return ISetExtensions::AddAndReturnTrue(_set, element);
        }

        bool AddFirstAndReturnTrue(Platform::Collections::System::IList<TElement> auto& elements)
        {
            return ISetExtensions::AddFirstAndReturnTrue(_set, elements);
        }

        bool AddAllAndReturnTrue(Platform::Collections::System::IList<TElement> auto& elements)
        {
            return ISetExtensions::AddAllAndReturnTrue(_set, elements);
        }

        bool AddSkipFirstAndReturnTrue(Platform::Collections::System::IList<TElement> auto& elements)
        {
            return ISetExtensions::AddSkipFirstAndReturnTrue(_set, elements);
        }

        TReturnConstant AddAndReturnConstant(TElement element)
        {
            _set.Add(element);
            return _returnConstant;
        }

        TReturnConstant AddFirstAndReturnConstant(Platform::Collections::System::IList<TElement> auto& elements)
        {
            _set.AddFirst(elements);
            return _returnConstant;
        }

        TReturnConstant AddAllAndReturnConstant(Platform::Collections::System::IList<TElement> auto& elements)
        {
            _set.AddAll(elements);
            return _returnConstant;
        }

        TReturnConstant AddSkipFirstAndReturnConstant(Platform::Collections::System::IList<TElement> auto& elements)
        {
            _set.AddSkipFirst(elements);
            return _returnConstant;
        }
    };
}// namespace Platform::Collections::Sets
