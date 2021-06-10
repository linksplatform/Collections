namespace Platform::Collections::System  // TODO пока что так
{
    namespace
    {
        struct __nil{};
    }

    template<typename Self>
    concept IEquatable = requires(Self left, Self right)
    {
        {left == right} -> std::same_as<bool>;
    };

    namespace Common
    {
        template<typename Self>
        struct IEnumerable
        {
            using TItem = std::ranges::range_value_t<Self>;
        };
    }

    template<typename Self>
    concept IEnumerable = std::ranges::range<Self>;

    namespace Common
    {
        template<typename Self>
        struct Array
        {
            using TItem = typename IEnumerable<Self>::TItem;
        };
    }

    template<typename Self, typename Item = __nil>
    concept Array = IEnumerable<Self> && requires
    {
        requires
        requires
        {
            requires std::same_as<std::ranges::range_value_t<Self>, Item>;
            requires std::ranges::random_access_range<Self>;
        }
        ||
        requires
        {
            requires std::same_as<Item, __nil>;
            requires std::ranges::random_access_range<Self>;
        };
    };

    namespace Common
    {
        template<typename Self>
        struct Set
        {
            using TItem = typename IEnumerable<Self>::TItem;
        };
    }

    template<typename Self, typename Item = __nil>
    concept ISet = IEnumerable<Self> && requires(
            Self self, Item item,
            typename Common::Set<Self>::TItem generic_item
    )
    {
        requires
            requires
            {
                {self.clear()};
                {self.find(item)} -> std::same_as<std::ranges::iterator_t<Self>>;
                {self.insert(item)};
                {self.erase(item)};
                {self.contains(item)} -> std::same_as<bool>;
                {self.empty()} -> std::same_as<bool>;

                requires std::ranges::bidirectional_range<Self>;
            }
            ||
            requires
            {
                requires std::same_as<Item, __nil>;
                {self.clear()};
                {self.find(generic_item)} -> std::same_as<std::ranges::iterator_t<Self>>;
                {self.insert(generic_item)};
                {self.erase(generic_item)};
                {self.contains(generic_item)} -> std::same_as<bool>;
                {self.empty()} -> std::same_as<bool>;

                requires std::ranges::bidirectional_range<Self>;
            };
    };

    namespace Common
    {
        template<typename Self>
        struct Dictionary
        {
            using _ = typename IEnumerable<Self>::TItem;// TODO rename
            using TKey = decltype(std::declval<_>().first);
            using TItem = decltype(std::declval<_>().second);
        };
    }

    template<typename Self, typename Key = __nil, typename Item = __nil>
    concept IDictionary = IEnumerable<Self> && requires(
        Self self, Key key, Item item,
        typename Common::Dictionary<Self>::TKey generic_key,
        typename Common::Dictionary<Self>::TItem generic_item
    )
    {
        requires
            requires
            {
                {self.clear()};
                {self.find(key)} -> std::forward_iterator;
                {self.contains(key)} -> std::same_as<bool>;
                {self.insert({key, item})};
                {self.empty()} -> std::same_as<bool>;
            }
            ||
            requires
            {
                requires std::same_as<Item, __nil>;
                {self.clear()};
                {self.find(key)} -> std::forward_iterator;
                {self.contains(key)} -> std::same_as<bool>;
                {self.insert({key, generic_item})};
                {self.empty()} -> std::same_as<bool>;
            }
            ||
            requires
            {
                requires std::same_as<Key, __nil>;
                {self.clear()};
                {self.find(generic_key)} -> std::forward_iterator;
                {self.contains(generic_key)} -> std::same_as<bool>;
                {self.insert({generic_key, generic_item})};
                {self.empty()} -> std::same_as<bool>;
            };
    };




    template<typename Self, typename Item = __nil>
    concept IList = Array<Self> && requires
    (
        Self self,
        Item item, int index,
        std::ranges::range_value_t<Self> generic_item,
        std::ranges::iterator_t<const Self> const_iterator
    )
    {
        requires
            requires
            {
                {self.push_back(item)};
                {self.insert(const_iterator, item)};
                {self.erase(const_iterator)};
            }
            ||
            requires
            {
                requires std::same_as<Item, __nil>;
                {self.push_back(generic_item)};
                {self.insert(const_iterator, generic_item)};
                {self.erase(const_iterator)};
            };
    };
}