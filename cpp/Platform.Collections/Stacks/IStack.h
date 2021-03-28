namespace Platform::Collections::Stacks
{
    template <typename T, typename TElement> concept IStack = requires(T t, TElement item)
    {
        {t.empty()} -> same_as<bool>;

        t.push(item);

        {t.pop()} -> same_as<TElement>;

        {t.top()} -> same_as<TElement&>;
    };
}
