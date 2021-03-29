template<typename T>
concept IEquatable = requires(T a, T b) {
    {a == b} -> std::same_as<bool>;
};

template<typename T>
concept IEnumerable = requires(T t) {
    {t.begin()} -> std::forward_iterator;
    {t.end()} -> std::forward_iterator;
};


template<typename C, typename T>
concept Array = IEnumerable<C> && requires(C t, T item, int index) {
    {t.operator[](index)} -> std::same_as<T&>;
    {t.size()} -> std::integral;
    {t.data()} -> std::same_as<T*>;
};

template<typename C, typename T>
concept BaseArray = requires(C t, int index) {
    {t[index]} -> std::same_as<T&>;
};


template<typename C, typename T>
concept ICollection = requires(C t, T item, T* array, int index) {
    {t.size()} -> std::integral;

    {t.isReadOnly()} -> std::same_as<bool>;

    t.add(item);

    t.clear();

    {t.contains(item)} -> std::same_as<bool>;

    t.copyTo(array, index);

    {t.remove(item)} -> std::same_as<bool>;
};


template<typename C, typename T>
concept ISet = IEnumerable<C> && requires(C t, T item) {
    {t.size()} -> std::integral;

    t.clear();

    {t.find(item)} -> std::bidirectional_iterator;

    {t.contains()} -> std::same_as<bool>;

    t.insert(item);

    {t.empty()} -> std::same_as<bool>;
};


template<typename C, typename T>
concept IList = Array<C, T> && ICollection<C, T> && requires(C t, T item, int index) {
    {t.indexOf(item)} -> std::integral;

    t.insert(index, item);

    t.removeAt(index);
};