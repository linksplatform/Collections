namespace Platform::Collections::Stacks
{
    class StackExtensions
    {
        public: template <typename T> static T PopOrDefault(Stack<T> stack) { return stack.Count() > 0 ? stack.Pop() : 0; }

        public: template <typename T> static T PeekOrDefault(Stack<T> stack) { return stack.Count() > 0 ? stack.Peek() : 0; }
    };
}
