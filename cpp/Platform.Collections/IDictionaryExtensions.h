namespace IDictionaryExtensions
{
    template <typename TKey, typename TValue>
    void Add(Platform::Collections::System::IDictionary<TKey, TValue> auto& dictionary, TKey key, TValue value)
    {
        if(dictionary.contains(key))
            // FIXME
            throw std::logic_error("/*Тут текст исключения, пародирующий C#*/");

        dictionary.insert({key, value});
    }

    template <typename TKey, typename TValue>
    bool TryGetValue(Platform::Collections::System::IDictionary<TKey, TValue> auto& dictionary, TKey key, TValue& value)
    {
        if (dictionary.contains(key))
        {
            value = dictionary[key];
            return true;
        }
        value = TValue{};
        return false;
    }
}