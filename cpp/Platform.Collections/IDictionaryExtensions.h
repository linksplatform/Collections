namespace Platform::Collections::Dictionaries
{
    //template<Interfaces::IDictionary TDictionary>
    void Add(/*TDictionary&*/auto& dictionary, auto&& key, auto&& value)
    {
        if (dictionary.contains(key))
        {
            throw std::logic_error("Unknown exception");
        }
        //using Item = typename Interfaces::Dictionary<TDictionary>::Item;
        dictionary.insert({std::forward<decltype(key)>(key), std::forward<decltype(value)>(value)});
    }

    template<Interfaces::IDictionary TDictionary>
    auto& GetOrAdd(TDictionary& dictionary, auto&& key, auto&& valueFactory)
    {
        auto contains = dictionary.contains(key);
        auto& value = dictionary[key];
        if (!contains)
        {
            value = valueFactory(key);
        }
        return value;
    }
}