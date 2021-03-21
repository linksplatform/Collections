namespace Platform::Collections::Stacks
{
    template <typename ...> class IStackFactory;
    template <typename TElement> class IStackFactory<TElement> : public IFactory<IStack<TElement>>
    {
    public:
    };
}
