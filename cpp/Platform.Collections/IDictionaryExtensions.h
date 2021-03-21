namespace Platform::Collections
{
    class IDictionaryExtensions
    {
        public: static TValue GetOrDefault<TKey, TValue>(IDictionary<TKey, TValue> &dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out TValue value);
            return value;
        }

        public: static TValue GetOrAdd<TKey, TValue>(IDictionary<TKey, TValue> &dictionary, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!dictionary.TryGetValue(key, out TValue value))
            {
                value = valueFactory(key);
                dictionary.Add(key, value);
                return value;
            }
            return value;
        }
    };
}
