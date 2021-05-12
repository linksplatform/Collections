namespace Platform::Collections::Dictionaries
{
    template<System::IDictionary TDictionary,
             typename TKey = typename System::Common::Dictionary<TDictionary>::TKey,
             typename TValue = typename System::Common::Dictionary<TDictionary>::TValue>
    void Add(TDictionary& dictionary, TKey key, TValue value)
    {
        if (dictionary.contains(key))
        {
            // FIXME
            throw std::logic_error("/*Тут текст исключения, пародирующий C#*/");
        }

        dictionary.insert({key, value});
    }

    template<System::IDictionary TDictionary,
             typename TKey = typename System::Common::Dictionary<TDictionary>::TKey,
             typename TValue = typename System::Common::Dictionary<TDictionary>::TValue>
    bool TryGetValue(const TDictionary& dictionary, TKey key, TValue value)
    {
        if (dictionary.contains(key))
        {
            value = dictionary[key];
            return true;
        }
        value = TValue{};
        return false;
    }
}// namespace Platform::Collections::Dictionaries