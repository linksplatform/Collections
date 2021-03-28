template<typename T>
concept Equatable = requires(T a, T b) {
    {a == b} -> same_as<bool>;
};

template<typename T>
concept Enumerable = requires(T t) {
    {t.begin()} -> forward_iterator;
    {t.end()} -> forward_iterator;
};


template<typename C, typename T>
concept Array = Enumerable<C> && requires(C t, T item, int number) {
    {t.operator[](number)} -> same_as<T&>;
    {t.size()} -> integral;
    {t.data()} -> same_as<T*>;
};


template<typename C, typename T>
concept Collection = requires(C t, T item, T* array, int index) {
    {t.size()} -> integral;

    {t.isReadOnly()} -> same_as<bool>;

    t.add(item);

    t.clear();

    {t.contains(item)} -> same_as<bool>;

    t.copyTo(array, index);

    {t.remove(item)} -> same_as<bool>;
};


template<typename C, typename T>
concept List = Array<C, T> && Collection<C, T> && requires(C t, T item, int index) {
    {t.indexOf(item)} -> integral;

    t.insert(index, item);

    t.removeAt(index);
};