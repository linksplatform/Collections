namespace Platform::Collections::Dictionaries
{
    template<Interfaces::IDictionary TDictionary>
    void Add(TDictionary& dictionary, auto&& key, auto&& value)
    {
        if (dictionary.contains(key))
        {
            throw std::logic_error("Unknown exception");
        }

        dictionary.insert({std::forward<decltype(key)>(key), std::forward<decltype(value)>(value)});
    }

    template<Interfaces::IDictionary TDictionary>
    auto& GetOrAdd(TDictionary& dictionary, auto&& key, auto&& valueFactory)
    {
        if (!dictionary.contains(key))
        {
            auto& value = dictionary[key];
            value = std::forward<decltype(valueFactory(key))>(valueFactory(key));
            return value;
        }
        return dictionary[key];
    }
}// namespace Platform::Collections::Dictionaries