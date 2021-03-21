namespace Platform::Collections::Concurrent
{
    class ConcurrentQueueExtensions
    {
        public: static IEnumerable<T> DequeueAll<T>(ConcurrentQueue<T> queue)
        {
            while (queue.TryDequeue(out T item))
            {
                yield return item;
            }
        }
    };
}
