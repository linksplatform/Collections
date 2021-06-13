namespace Platform::Collections::System  // TODO пока что так
{
    template<typename Self>
    concept IEnumerable = std::ranges::range<Self>;

    template<IEnumerable Self>
    struct Enumerable
    {
        using Item = std::ranges::range_value_t<Self>;
    };

    template<typename Self, typename... Item>
    concept IArray =
        IEnumerable<Self> &&
        sizeof...(Item) <= 1 &&
    requires(std::tuple<Item...> items)
    {
        requires
            requires
            {
                requires sizeof...(Item) == 1;

                requires std::same_as<std::ranges::range_value_t<Self>, decltype(std::get<0>(items))>;
                requires std::ranges::random_access_range<Self>;
            }
            ||
            requires
            {
                requires sizeof...(Item) == 0;

                requires std::ranges::random_access_range<Self>;
            };
    };

    template<IArray Self>
    struct Array : Enumerable<Self> {};

    template<typename Self, typename... Item>
    concept ISet =
        IEnumerable<Self> &&
        sizeof...(Item) <= 1 &&
    requires
    (
        Self self,
        std::tuple<Item...> items,

        decltype(std::get<0>(items)) item,

        typename Enumerable<Self>::Item generic_item
    )
    {
        requires
            requires
            {
                requires sizeof...(Item) == 1;

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
                requires sizeof...(Item) == 0;

                {self.clear()};
                {self.find(generic_item)} -> std::same_as<std::ranges::iterator_t<Self>>;
                {self.insert(generic_item)};
                {self.erase(generic_item)};
                {self.contains(generic_item)} -> std::same_as<bool>;
                {self.empty()} -> std::same_as<bool>;

                requires std::ranges::bidirectional_range<Self>;
            };
    };

    template<ISet Self>
    struct Set : Enumerable<Self> {};


    template<typename Self, typename... Args>
    concept IDictionary =
        IEnumerable<Self> &&
        sizeof...(Args) <= 1 &&
    requires
    (
        Self self,
        std::tuple<Args...> args,

        decltype(std::get<0>(args)) key,
        decltype(std::get<1>(args)) value,

        decltype(std::declval<Enumerable<Self>::Item>().first) generic_key,
        decltype(std::declval<Enumerable<Self>::Item>().second) generic_value
    )
    {
        requires
            requires
            {
                requires sizeof...(Args) == 2;

                {self.clear()};
                {self.find(key)} -> std::forward_iterator;
                {self.contains(key)} -> std::same_as<bool>;
                {self.insert({key, value})};
                {self.empty()} -> std::same_as<bool>;
            }
            ||
            requires
            {
                requires sizeof...(Args) == 1;

                {self.clear()};
                {self.find(key)} -> std::forward_iterator;
                {self.contains(key)} -> std::same_as<bool>;
                {self.insert({key, generic_value})};
                {self.empty()} -> std::same_as<bool>;
            }
            ||
            requires
            {
                requires sizeof...(Args) == 0;

                {self.clear()};
                {self.find(generic_key)} -> std::forward_iterator;
                {self.contains(generic_key)} -> std::same_as<bool>;
                {self.insert({generic_key, generic_value})};
                {self.empty()} -> std::same_as<bool>;
            };
    };

    template<IDictionary Self>
    struct Dictionary : Enumerable<Self>
    {
    private:
        using base = Enumerable<Self>;

    public:
        using Key = decltype(std::declval<base::Item>().first);
        using Value = decltype(std::declval<base::Item>().second);
    };


    template<typename Self, typename... Item>
    concept IList =
        IArray<Self> &&
        sizeof...(Item) <= 1 &&
    requires
    (
        Self self,
        std::tuple<Item...> items,

        std::size_t index,
        decltype(std::get<0>(items)) item,

        std::ranges::range_value_t<Self> generic_item,

        std::ranges::iterator_t<const Self> const_iterator
    )
    {
        requires
            requires
            {
                requires sizeof...(Item) == 1;

                {self.push_back(item)};
                {self.insert(const_iterator, item)};
                {self.erase(const_iterator)};
            }
            ||
            requires
            {
                requires sizeof...(Item) == 0;

                {self.push_back(generic_item)};
                {self.insert(const_iterator, generic_item)};
                {self.erase(const_iterator)};
            };
    };

    template<IList Self>
    struct List : Enumerable<Self> {};
}