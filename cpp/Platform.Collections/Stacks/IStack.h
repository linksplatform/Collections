namespace Platform::Collections::Stacks
{
    template <typename T, typename TElement> concept IStack = requires(T t, TElement item)
    {
        {t.empty()} -> std::same_as<bool>;

        t.push(item);

        //{t.pop()} -> std::same_as<TElement>;
        t.pop();

        {t.top()} -> std::same_as<TElement&>;
    };
}
