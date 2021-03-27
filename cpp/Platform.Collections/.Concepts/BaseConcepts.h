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
    {t.Count()} -> integral;

    {t.IsReadOnly()} -> same_as<bool>;

    t.Add(item);

    t.Clear();

    {t.Contains(item)} -> same_as<bool>;

    //TODO Возникла проблемка. Нужны специалисты С++
    //t.CopyTo(Array<C, T>, index);

    {t.Remove(item)} -> same_as<bool>;
};


template<typename C, typename T>
concept List = Array<C, T> && Collection<C, T> && requires(C t, T item, int index) {
    {t.IndexOf(item)} -> integral;

    t.Insert(index, item);

    t.RemoveAt(index);
};