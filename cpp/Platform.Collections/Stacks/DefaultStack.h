namespace Platform::Collections::Stacks
{
    template <typename ...> class DefaultStack;
    template <typename TElement> class DefaultStack<TElement> : public Stack<TElement>, IStack<TElement>
    {
        public: bool IsEmpty()
        {
            return Count <= 0;
        }
    };
}
