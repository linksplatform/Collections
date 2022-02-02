namespace Platform::Collections::Stacks
{
    template<typename TElement>
    class IStack
    {
    public:
        virtual bool empty() = 0;

        virtual void push(TElement item) = 0;

        virtual void pop() = 0;

        virtual TElement top() = 0;
    };
}
