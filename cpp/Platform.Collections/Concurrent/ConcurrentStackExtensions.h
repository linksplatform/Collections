namespace Platform::Collections::Concurrent
{
    class ConcurrentStackExtensions
    {
        public: template <typename T> static T PopOrDefault(ConcurrentStack<T> stack) { return stack.TryPop(out T value) ? value : 0; }

        public: template <typename T> static T PeekOrDefault(ConcurrentStack<T> stack) { return stack.TryPeek(out T value) ? value : 0; }
    };
}
