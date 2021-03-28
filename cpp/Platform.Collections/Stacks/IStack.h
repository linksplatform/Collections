namespace Platform::Collections::Stacks
{
    template <typename T, typename TElement> concept Stack = requires(T t, TElement item) {
        {t.empty()} -> same_as<bool>;

        {t.top()} -> same_as<TElement&>;

        {t.pop()} -> same_as<TElement>;

        t.push(item);
    };

}
