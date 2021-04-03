namespace Platform::Collections::System  // TODO пока что так
{
    template<typename _Type>
    concept IEquatable = requires(_Type a, _Type b) {
        {a == b} -> std::same_as<bool>;
    };

    template<typename _Type>
    concept IEnumerable = requires(_Type t) {
        {t.begin()} -> std::forward_iterator;
        {t.end()} -> std::forward_iterator;
    };


    template<typename _Type, typename _Item, typename _Key = int>
    concept Array = IEnumerable<_Type> && requires(_Type t, _Key index) {
        {t[index]} -> std::same_as<_Item&>;
        {t.size()} -> std::integral;
        //{t.data()} -> std::same_as<T*>; // TODO Убран из-за небезопасности

        {t.begin()} -> std::random_access_iterator;
        {t.end()} -> std::random_access_iterator;
    };

    template<typename _Type, typename _Item>
    concept BaseArray = requires(_Type t, int index) {
        {t[index]} -> std::same_as<_Item&>;
    };


    template<typename _Type, typename _Item>
    concept ISet = IEnumerable<_Type> && requires(_Type t, _Item item) {
        {t.size()} -> std::integral;

        t.clear();

        {t.find(item)} -> std::bidirectional_iterator;

        {t.contains(item)} -> std::same_as<bool>;

        t.insert(item);

        {t.empty()} -> std::same_as<bool>;

        {t.begin()} -> std::bidirectional_iterator;
        {t.end()} -> std::bidirectional_iterator;
    };

    template<typename _Type, typename _Key, typename _Item>
    concept IDictionary = IEnumerable<_Type> && Array<_Item, _Key> && requires(_Type t, _Key key, _Item item) {
        {t.size()} -> std::integral;

        t.clear();

        {t.find(key)} -> std::bidirectional_iterator;

        {t.contains(key)} -> std::same_as<bool>;

        t.insert({key, item});

        {t.empty()} -> std::same_as<bool>;

        {t.begin()} -> std::bidirectional_iterator;
        {t.end()} -> std::bidirectional_iterator;
    };


    template<typename _Type, typename _Item>
    concept IList = Array<_Type, _Item> && requires(
            _Type t,
            const _Type const_t,
            _Item item, int index,
            decltype(const_t.begin()) const_iterator
    )
    {
        t.push_back(item);
        t.insert(const_iterator, item);
        t.erase(const_iterator);
    };
}