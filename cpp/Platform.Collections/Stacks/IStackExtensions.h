namespace Platform::Collections::Stacks
{
    class IStackExtensions
    {
        public: template <typename T> static void Clear(Stack<T> auto& stack)
        {
            while (!stack.empty())
            {
                stack.pop();
            }
        }

        public: template <default_initializable T> static T PopOrDefault(Stack<T> auto& stack) { return stack.empty() ? T{} : stack.pop(); }

        public: template <default_initializable T> static T PeekOrDefault(Stack<T> auto& stack) { return stack.empty() ? T{} : stack.top(); }
    };
}
