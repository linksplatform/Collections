template<typename T>
concept IEquatable = requires(T a, T b) {
    {a == b} -> std::same_as<bool>;
};

template<typename T>
concept IEnumerable = requires(T t) {
    {t.begin()} -> std::forward_iterator;
    {t.end()} -> std::forward_iterator;
};


template<typename C, typename T, typename Key = int>
concept Array = IEnumerable<C> && requires(C t, Key index) {
    {t[index]} -> std::same_as<T&>;
    {t.size()} -> std::integral;
    //{t.data()} -> std::same_as<T*>; // TODO Убран из-за небезопастности

    {t.begin()} -> std::random_access_iterator;
    {t.end()} -> std::random_access_iterator;
};

template<typename C, typename T>
concept BaseArray = requires(C t, int index) {
    {t[index]} -> std::same_as<T&>;
};


template<typename C, typename T>
concept ISet = IEnumerable<C> && requires(C t, T item) {
    {t.size()} -> std::integral;

    t.clear();

    {t.find(item)} -> std::bidirectional_iterator;

    {t.contains(item)} -> std::same_as<bool>;

    t.insert(item);

    {t.empty()} -> std::same_as<bool>;

    {t.begin()} -> std::bidirectional_iterator;
    {t.end()} -> std::bidirectional_iterator;
};

template<typename C, typename Key, typename Type>
concept IDictionary = IEnumerable<C> && Array<Type, Key> && requires(C t, Key key, Type item) {
    {t.size()} -> std::integral;

    t.clear();

    {t.find(key)} -> std::bidirectional_iterator;

    {t.contains(key)} -> std::same_as<bool>;

    t.insert({key, item});

    {t.empty()} -> std::same_as<bool>;

    {t.begin()} -> std::bidirectional_iterator;
    {t.end()} -> std::bidirectional_iterator;
};


template<typename C, typename T>
concept IList = Array<C, T> && requires(
        C t,
        const C const_t,
        T item, int index,
        decltype(const_t.begin()) const_iterator
)
{
    t.push_back(item);
    t.insert(const_iterator, item);
    t.erase(const_iterator);
};


template<typename C, typename T>
concept IComparer = requires(C t, T a, T b) {
    {t.compare(a, b)} -> std::integral;
};


