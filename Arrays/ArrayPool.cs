using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    /// <remarks>
    /// TODO: Check actual performance
    /// TODO: Check for memory leaks
    /// </remarks>
    public static class ArrayPool
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Allocate<T>(long size) => ArrayPool<T>.GetOrCreateThreadInstance().Allocate(size);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Free<T>(T[] array) => ArrayPool<T>.GetOrCreateThreadInstance().Free(array);
    }
}
