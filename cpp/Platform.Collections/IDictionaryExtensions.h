namespace Platform::Collections::Dictionaries
{
    template<Interfaces::CDictionary TDictionary>
    void Add(TDictionary& dictionary, auto key, auto value)
    {
        Expects(!dictionary.contains(key));
        dictionary.insert({std::move(key), std::move(value)});
    }

    template<Interfaces::CDictionary TDictionary>
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