namespace Platform::Collections::Dictionaries
{
    template<Interfaces::IDictionary TDictionary,
             typename TKey = typename Interfaces::Dictionary<TDictionary>::Key,
             typename TValue = typename Interfaces::Dictionary<TDictionary>::Value>
    void Add(TDictionary& dictionary, TKey key, TValue value)
    {
        if (dictionary.contains(key))
        {
            // FIXME
            throw std::logic_error("/*Тут текст исключения, пародирующий C#*/");
        }

        dictionary.insert({key, value});
    }
}// namespace Platform::Collections::Dictionaries