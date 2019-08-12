using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Concurrent
{
    public static class ConcurrentStackExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T PopOrDefault<T>(this ConcurrentStack<T> stack) => stack.TryPop(out T value) ? value : default;

        public static T PeekOrDefault<T>(this ConcurrentStack<T> stack) => stack.TryPeek(out T value) ? value : default;
    }
}
