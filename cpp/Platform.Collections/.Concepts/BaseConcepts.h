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

    namespace Common
    {
        template<typename _Type>
        struct IEnumerable
        {
            using TItem = std::ranges::range_value_t<_Type>;
        };
    }

    template<typename _Type>
    concept IEnumerable = std::ranges::range<_Type>;

    namespace Common
    {
        template<typename _Type>
        struct Array
        {
            using TItem = typename IEnumerable<_Type>::TItem;
        };
    }

    template<typename _Type, typename _Item = __nil>
    concept Array = IEnumerable<_Type> && requires()
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

    namespace Common
    {
        template<typename _Type>
        struct Set
        {
            using TItem = typename IEnumerable<_Type>::TItem;
        };
    }

    template<typename _Type, typename _Item = __nil>
    concept ISet = IEnumerable<_Type> && requires(
            _Type object, _Item item,
            typename Common::Set<_Type>::TItem generic_item
    )
    {
        requires
            requires()
            {
                {object.clear()};
                {object.find(item)} -> std::same_as<std::ranges::iterator_t<_Type>>;
                {object.insert(item)};
                {object.erase(item)};
                {object.contains(item)} -> std::same_as<bool>;
                {object.empty()} -> std::same_as<bool>;

                requires std::ranges::bidirectional_range<_Type>;
            }
            ||
            requires()
            {
                requires std::same_as<_Item, __nil>;
                {object.clear()};
                {object.find(generic_item)} -> std::same_as<std::ranges::iterator_t<_Type>>;
                {object.insert(generic_item)};
                {object.erase(generic_item)};
                {object.contains(generic_item)} -> std::same_as<bool>;
                {object.empty()} -> std::same_as<bool>;

                requires std::ranges::bidirectional_range<_Type>;
            };
    };

    namespace Common
    {
        template<typename _Type>
        struct Dictionary
        {
            using _ = typename IEnumerable<_Type>::TItem;// TODO rename
            using TKey = decltype(std::declval<_>().first);
            using TItem = decltype(std::declval<_>().second);
        };
    }

    template<typename _Type, typename _Key = __nil, typename _Item = __nil>
    concept IDictionary = IEnumerable<_Type> && requires(
        _Type object, _Key key, _Item item,
        typename Common::Dictionary<_Type>::TKey generic_key,
        typename Common::Dictionary<_Type>::TItem generic_item
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