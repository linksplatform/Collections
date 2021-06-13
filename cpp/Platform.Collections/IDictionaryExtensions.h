namespace Platform::Collections::Dictionaries
{
    template<System::IDictionary TDictionary,
             typename TKey = typename System::Dictionary<TDictionary>::Key,
             typename TValue = typename System::Dictionary<TDictionary>::Value>
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