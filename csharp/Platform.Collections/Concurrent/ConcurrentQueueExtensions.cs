using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Concurrent
{
    /// <summary>
    /// <para>
    /// Represents the concurrent queue extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class ConcurrentQueueExtensions
    {
        /// <summary>
        /// <para>
        /// Dequeues the all using the specified queue.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="queue">
        /// <para>The queue.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>An enumerable of t</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DequeueAll<T>(this ConcurrentQueue<T> queue)
        {
            while (queue.TryDequeue(out T item))
            {
                yield return item;
            }
        }
    }
}
