namespace Platform::Collections::Stacks
{
    template <typename ...> class IStack;
    template <typename TElement> class IStack<TElement>
    {
    public:
        const bool IsEmpty;

        virtual void Push(TElement element) = 0;

        virtual TElement Pop() = 0;

        virtual TElement Peek() = 0;
    };
}
