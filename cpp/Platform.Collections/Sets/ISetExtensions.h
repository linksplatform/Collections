namespace Platform::Collections::Sets
{
    static void AddAndReturnVoid(Interfaces::ISet auto& set, auto&& element)
    {
        set.insert(std::forward<decltype(element)>(element));
    }

    static void RemoveAndReturnVoid(Interfaces::ISet auto& set, auto&& element)
    {
        set.erase(std::forward<decltype(element)>(element));
    }

    static bool AddAndReturnTrue(Interfaces::ISet auto& set, auto&& element)
    {
        set.insert(std::forward<decltype(element)>(element));
        return true;
    }

    static bool AddFirstAndReturnTrue(Interfaces::ISet auto& set, Interfaces::IArray auto&& elements)
    {
        AddFirst(set, elements);
        return true;
    }

    static void AddFirst(Interfaces::ISet auto& set, Interfaces::IArray auto&& elements)
    {
        set.insert(elements[0]);
    }

    static bool AddAllAndReturnTrue(Interfaces::ISet auto& set, Interfaces::IArray auto&& elements)
    {
        AddAll(set, elements);
        return true;
    }

    static void AddAll(Interfaces::ISet auto& set, Interfaces::IArray auto&& elements)
    {
        for (auto element : elements)
        {
            set.insert(element);
        }
    }

    static bool AddSkipFirstAndReturnTrue(Interfaces::ISet auto& set, Interfaces::IArray auto&& elements)
    {
        AddSkipFirst(set, elements);
        return true;
    }

    static void AddSkipFirst(Interfaces::ISet auto& set, Interfaces::IArray auto&& elements) { AddSkipFirst(set, elements, 1); }

    static void AddSkipFirst(Interfaces::ISet auto& set, Interfaces::IArray auto&& elements, std::integral auto skip)
    {
        for (auto&& element : elements | std::views::drop(skip))
        {
            set.insert(std::forward<decltype(element)>(element));
        }
    }

    static bool DoNotContains(const Interfaces::ISet auto& set, auto&& element) { return not set.contains(element); }
}
