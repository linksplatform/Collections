namespace Platform::Collections::Sets
{
    template<typename...>
    class SetFiller;
    template<System::ISet TSet, typename TReturnConstant>
    class SetFiller<TSet, TReturnConstant>
    {
        using TElement = std::ranges::range_value_t<TSet>;

    protected:
        TSet& _set;
        TReturnConstant _returnConstant = 0;

    public:
        SetFiller(TSet& set, TReturnConstant returnConstant)
            : _set(set)
        {
            _returnConstant = returnConstant;
        }

        SetFiller(TSet& set)
            : SetFiller(set, TReturnConstant{})
        {
        }

        void Add(TElement element)
        {
            _set.insert(element);
        }

        bool AddAndReturnTrue(TElement element)
        {
            return Sets::AddAndReturnTrue(_set, element);
        }

        bool AddFirstAndReturnTrue(const System::Array<TElement> auto& elements)
        {
            return Sets::AddFirstAndReturnTrue(_set, elements);
        }

        bool AddAllAndReturnTrue(const System::Array<TElement> auto& elements)
        {
            return Sets::AddAllAndReturnTrue(_set, elements);
        }

        bool AddSkipFirstAndReturnTrue(const System::Array<TElement> auto& elements)
        {
            return Sets::AddSkipFirstAndReturnTrue(_set, elements);
        }

        TReturnConstant AddAndReturnConstant(TElement element)
        {
            _set.insert(element);
            return _returnConstant;
        }

        TReturnConstant AddFirstAndReturnConstant(const System::Array<TElement> auto& elements)
        {
            Sets::AddFirst(_set, elements);
            return _returnConstant;
        }

        TReturnConstant AddAllAndReturnConstant(const System::Array<TElement> auto& elements)
        {
            Sets::AddAll(_set, elements);
            return _returnConstant;
        }

        TReturnConstant AddSkipFirstAndReturnConstant(const System::Array<TElement> auto& elements)
        {
            Sets::AddSkipFirst(_set, elements);
            return _returnConstant;
        }
    };

    template<typename TSet, typename TReturnConstant>
    SetFiller(TSet, TReturnConstant) -> SetFiller<TSet, TReturnConstant>;

}// namespace Platform::Collections::Sets
