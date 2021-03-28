namespace Platform::Collections::Stacks
{
    template <typename ...> class DefaultStack;
    template <typename TElement> class DefaultStack<TElement> : public stack<TElement>
    {
        public: DefaultStack() : stack<TElement>() {}

        public: explicit DefaultStack(Enumerable auto enumerable) : stack<TElement>(deque<int>(enumerable.begin(), enumerable.end())) {}

        public: TElement pop() {
            TElement item = stack<TElement>::top();
            stack<TElement>::pop();
            return item;
        }

        public: bool operator==(DefaultStack<TElement> other) {
            return std::equal(begin(), end(), other.begin());
        }


        public: auto begin() {
            return stack<TElement>::c.begin();
        }

        public: auto end() {
            return stack<TElement>::c.end();
        }
    };
}
