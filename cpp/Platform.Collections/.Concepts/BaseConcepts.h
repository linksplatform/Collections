namespace Platform::Collections::System  // TODO пока что так
{
    namespace
    {
        struct __nil{};
    }

    template<typename _Type>
    concept IEquatable = requires(_Type left, _Type right)
    {
        {left == right} -> std::same_as<bool>;
    };

    template<typename _Type>
    concept IEnumerable = std::ranges::range<_Type>;

    template<typename _Type, typename _Item = __nil>
    concept Array = requires()
    {
        requires
        requires()
        {
            requires std::same_as<std::ranges::range_value_t<_Type>, _Item>;
            requires std::ranges::random_access_range<_Type>;
        }
        ||
        requires()
        {
            requires std::same_as<_Item, __nil>;
            requires std::ranges::random_access_range<_Type>;
        };
    };

    template<typename _Type, typename _Item>
    concept ISet = requires(_Type object, _Item item)
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

    template<typename _Type, typename _Key = __nil, typename _Item = __nil>
    concept IDictionary = requires(
        _Type object, _Key key, _Item item,
        decltype(std::declval<std::ranges::iterator_t<_Type>>().first) generic_key,
        decltype(std::declval<std::ranges::iterator_t<_Type>>().second) generic_item
    )
    {
        requires
            requires()
            {
                {object.clear()};
                {object.find(key)} -> std::forward_iterator;
                {object.contains(key)} -> std::same_as<bool>;
                {object.insert({key, item})};
                {object.empty()} -> std::same_as<bool>;
            }
            ||
            requires()
            {
                requires std::same_as<_Item, __nil>;
                {object.clear()};
                {object.find(key)} -> std::forward_iterator;
                {object.contains(key)} -> std::same_as<bool>;
                {object.insert({key, generic_item})};
                {object.empty()} -> std::same_as<bool>;
            }
            ||
            requires()
            {
                requires std::same_as<_Key, __nil>;
                {object.clear()};
                {object.find(generic_key)} -> std::forward_iterator;
                {object.contains(generic_key)} -> std::same_as<bool>;
                {object.insert({generic_key, generic_item})};
                {object.empty()} -> std::same_as<bool>;
            };
    };




    template<typename _Type, typename _Item = __nil>
    concept IList = Array<_Type> && requires(
        _Type object,
        _Item item, int index,
        std::ranges::range_value_t<_Type> generic_item,
        std::ranges::iterator_t<const _Type> const_iterator
    )
    {
        requires
            requires()
            {
                {object.push_back(item)};
                {object.insert(const_iterator, item)};
                {object.erase(const_iterator)};
            }
            ||
            requires()
            {
                requires std::same_as<_Item, __nil>;
                {object.push_back(generic_item)};
                {object.insert(const_iterator, generic_item)};
                {object.erase(const_iterator)};
            };
    };
}