namespace Platform::Collections::System  // TODO пока что так
{
    template<typename _Type>
    concept IEquatable = requires(_Type left, _Type right)
    {
        {left == right} -> std::same_as<bool>;
    };

    template<typename _Type>
    concept IEnumerable = requires(_Type object)
    {
        {object.begin()} -> std::forward_iterator;
        {object.end()} -> std::forward_iterator;
    };

    template<typename _Type>
    concept ICollection = IEnumerable<_Type> && requires(_Type object)
    {
        {object.size()} -> std::integral;
    };

    template<typename _Type, typename _Item, typename _Key = int>
    concept Array = ICollection<_Type> && requires(_Type object, _Key index)
    {
        {object[index]} -> std::same_as<_Item&>;
        //{object.data()} -> std::same_as<object*>; // TODO Убран из-за небезопасности

        {object.begin()} -> std::random_access_iterator;
        {object.end()} -> std::random_access_iterator;
    };

    template<typename _Type, typename _Item, typename _Key = int>
    concept BaseArray = requires(_Type object, int index)
    {
        {object[index]} -> std::same_as<_Item&>;
    };


    template<typename _Type, typename _Item>
    concept ISet = ICollection<_Type> && requires(_Type object, _Item item)
    {
        {object.clear()};
        {object.find(item)} -> std::bidirectional_iterator;
        {object.contains(item)} -> std::same_as<bool>;
        {object.insert(item)};
        {object.erase(item)};
        {object.empty()} -> std::same_as<bool>;

        {object.begin()} -> std::bidirectional_iterator;
        {object.end()} -> std::bidirectional_iterator;
    };

    template<typename _Type, typename _Key, typename _Item>
    concept IDictionary = ICollection<_Type> && BaseArray<_Type, _Item, _Key> && requires(_Type object, _Key key, _Item item)
    {
        {object.clear()};
        {object.find(key)} -> std::forward_iterator;
        {object.contains(key)} -> std::same_as<bool>;
        {object.insert(/*std::pair*/{key, item})};
        {object.empty()} -> std::same_as<bool>;
    };


    template<typename _Type, typename _Item>
    concept IList = Array<_Type, _Item> && requires(
            _Type object,
            const _Type const_t,
            _Item item, int index,
            decltype(const_t.begin()) const_iterator
    )
    {
        {object.push_back(item)};
        {object.insert(const_iterator, item)};
        {object.erase(const_iterator)};
        //
        // если очень хочется, то можно и pop_back
    };
}