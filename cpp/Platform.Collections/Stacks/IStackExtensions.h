namespace Platform::Collections::Stacks
{
    class IStackExtensions
    {
        public: template <typename T> static void Clear(IStack<T> &stack)
        {
            while (!stack.IsEmpty)
            {
                _ = stack.Pop();
            }
        }

        public: template <typename T> static T PopOrDefault(IStack<T> &stack) { return stack.IsEmpty ? 0 : stack.Pop(); }

        public: template <typename T> static T PeekOrDefault(IStack<T> &stack) { return stack.IsEmpty ? 0 : stack.Peek(); }
    };
}
