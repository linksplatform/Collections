namespace Platform::Collections::Stacks
{
    template<typename Self, typename TElement>
    concept IStack = requires(Self self, TElement item)
    {
        { self.empty() } -> std::same_as<bool>;

        { self.push(item) };

        { self.pop() };

        { self.top() } -> std::same_as<TElement&>;
    };
}
