namespace Platform::Collections::Sets
{
    static void AddAndReturnVoid(Interfaces::CSet auto& set, auto&& element)
    {
        set.insert(std::forward<decltype(element)>(element));
    }

    static void RemoveAndReturnVoid(Interfaces::CSet auto& set, auto&& element)
    {
        set.erase(std::forward<decltype(element)>(element));
    }

    static bool AddAndReturnTrue(Interfaces::CSet auto& set, auto&& element)
    {
        set.insert(std::forward<decltype(element)>(element));
        return true;
    }

    static bool AddFirstAndReturnTrue(Interfaces::CSet auto& set, Interfaces::CArray auto&& elements)
    {
        AddFirst(set, elements);
        return true;
    }

    static void AddFirst(Interfaces::CSet auto& set, Interfaces::CArray auto&& elements)
    {
        set.insert(elements[0]);
    }

    static bool AddAllAndReturnTrue(Interfaces::CSet auto& set, Interfaces::CArray auto&& elements)
    {
        AddAll(set, elements);
        return true;
    }

    static void AddAll(Interfaces::CSet auto& set, Interfaces::CArray auto&& elements)
    {
        for (auto element : elements)
        {
            set.insert(element);
        }
    }

    static bool AddSkipFirstAndReturnTrue(Interfaces::CSet auto& set, Interfaces::CArray auto&& elements)
    {
        AddSkipFirst(set, elements);
        return true;
    }

    static void AddSkipFirst(Interfaces::CSet auto& set, Interfaces::CArray auto&& elements) { AddSkipFirst(set, elements, 1); }

    static void AddSkipFirst(Interfaces::CSet auto& set, Interfaces::CArray auto&& elements, std::size_t skip)
    {
        for (auto&& element : elements | std::views::drop(skip))
        {
            set.insert(std::forward<decltype(element)>(element));
        }
    }

    static bool DoNotContains(const Interfaces::CSet auto& set, auto&& element) { return not set.contains(element); }
}
