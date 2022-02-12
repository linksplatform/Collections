namespace Platform::Collections::Stacks
{
    template <typename ...>
    struct IStack;

    template<typename TElement>
    struct IStack<TElement>
    {
        virtual bool empty() const = 0;

        virtual void push(TElement item) = 0;

        virtual void pop() = 0;

        virtual const TElement& top() const = 0;

        virtual ~IStack() = default;
    };
}
