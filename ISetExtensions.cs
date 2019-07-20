using System.Collections.Generic;

namespace Platform.Collections
{
    public static class ISetExtensions
    {
        public static void AddAndReturnVoid<T>(this ISet<T> set, T element) => set.Add(element);
        public static void RemoveAndReturnVoid<T>(this ISet<T> set, T element) => set.Remove(element);
        public static bool DoNotContains<T>(this ISet<T> set, T element) => !set.Contains(element);
    }
}
