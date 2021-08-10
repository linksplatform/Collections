namespace Platform::Collections::Dictionaries
{
    template<Interfaces::IDictionary TDictionary>
    void Add(TDictionary& dictionary, auto&& key, auto&& value)
    {
        if (dictionary.contains(key))
        {
            throw std::logic_error("Unknown exception");
        }
        using Item = typename Interfaces::Dictionary<TDictionary>::Item;
        dictionary.insert(Item{std::forward<decltype(key)>(key), std::forward<decltype(value)>(value)});
    }

    template<Interfaces::IDictionary TDictionary>
    auto& GetOrAdd(TDictionary& dictionary, auto&& key, auto&& valueFactory)
    {
        if (!dictionary.contains(key))
        {
            auto& value = dictionary[key];
            value = valueFactory(key);
            return value;
        }
        return dictionary[key];
    }
}