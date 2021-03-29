template<typename T>
concept Equatable = requires(T a, T b) {
    {a == b} -> std::same_as<bool>;
};

template<typename T>
concept Enumerable = requires(T t) {
    {t.begin()} -> std::forward_iterator;
    {t.end()} -> std::forward_iterator;
};


template<typename C, typename T>
concept Array = Enumerable<C> && requires(C t, T item, int number) {
    {t.operator[](number)} -> std::same_as<T&>;
    {t.size()} -> std::integral;
    {t.data()} -> std::same_as<T*>;
};


template<typename C, typename T>
concept Collection = requires(C t, T item, T* array, int index) {
    {t.size()} -> std::integral;

    {t.isReadOnly()} -> std::same_as<bool>;

    t.add(item);

    t.clear();

    {t.contains(item)} -> std::same_as<bool>;

    t.copyTo(array, index);

    {t.remove(item)} -> std::same_as<bool>;
};


template<typename C, typename T>
concept List = Array<C, T> && Collection<C, T> && requires(C t, T item, int index) {
    {t.indexOf(item)} -> std::integral;

    t.insert(index, item);

    t.removeAt(index);
};