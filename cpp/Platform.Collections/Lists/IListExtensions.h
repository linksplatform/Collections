namespace Platform::Collections::Lists
{
    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
        requires std::default_initializable<TItem>
    static auto GetElementOrDefault(const TList& list, std::integral auto index) noexcept
    {
        return (index >= 0 && list.size() > index) ? list[index] : TItem{};
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
        requires std::default_initializable<TItem>
    static bool TryGetElement(TList&& list, std::integral auto index, TItem& element) noexcept
    {
        if (index >= 0 && list.size() > index)
        {
            element = list[index]; // TODO: move if list is moved
            return true;
        }
        else
        {
            element = TItem{};
            return false;
        }
    }

    template<Interfaces::IList TList>
    static bool AddAndReturnTrue(TList& list, auto&& element)
    {
        list.push_back(std::forward<decltype(element)>(element));
        return true;
    }

    template<Interfaces::IList TList>
    static bool AddFirstAndReturnTrue(TList& list, Interfaces::IArray auto&& elements)
    {
        AddFirst(list, std::forward<decltype(elements)>(elements));
        return true;
    }

    template<Interfaces::IList TList>
    static void AddFirst(TList& list, Interfaces::IArray auto&& elements)
    {
        auto&& element = *std::ranges::begin(std::forward<decltype(elements)>(elements));
        list.push_back(std::forward<decltype(element)>(element));
    }

    template<Interfaces::IList TList>
    static bool AddAllAndReturnTrue(TList& list, Interfaces::IArray auto&& elements)
    {
        AddAll(list, std::forward<decltype(elements)>(elements));
        return true;
    }

    template<Interfaces::IList TList>
    static void AddAll(TList& list, Interfaces::IArray auto&& elements)
    {
        for (auto&& element : elements)
        {
            list.push_back(element);
        }
    }

    template<Interfaces::IList TList>
    static bool AddSkipFirstAndReturnTrue(TList& list, Interfaces::IArray auto&& elements)
    {
        AddSkipFirst(std::forward<decltype(elements)>(elements));
        return true;
    }

    template<Interfaces::IList TList>
    static void AddSkipFirst(TList& list, Interfaces::IArray auto&& elements)
    {
        AddSkipFirst(list, std::forward<decltype(elements)>(elements), 1);
    }

    template<Interfaces::IList TList>
    static void AddSkipFirst(TList& list, Interfaces::IArray auto&& elements, std::integral auto skip)
    {
        for (auto&& element : elements | std::views::drop(skip))
        {
            list.push_back(element);
        }
    }
}
