namespace Platform::Collections::Dictionaries
{
    template<Interfaces::IDictionary TDictionary>
    void Add(TDictionary& dictionary, auto&& key, auto&& value)
    {
        if (dictionary.contains(std::forward<decltype(key)>(key)))
        {
            throw std::logic_error("Unknown exception");
        }

        dictionary.insert({std::forward<decltype(key)>(key), std::forward<decltype(value)>(value)});
    }
}// namespace Platform::Collections::Dictionaries