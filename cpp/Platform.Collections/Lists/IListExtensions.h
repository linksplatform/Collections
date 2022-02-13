namespace Platform::Collections::Lists
{
    template<Interfaces::CList TList, typename TItem = typename Interfaces::List<TList>::Item>
        requires std::default_initializable<TItem>
    static auto GetElementOrDefault(const TList& list, std::size_t index) noexcept
    {
        return (list.size() > index) ? list[index] : TItem{};
    }

    template<Interfaces::CList TList, typename TItem = typename Interfaces::List<TList>::Item>
        requires std::default_initializable<TItem>
    static bool TryGetElement(TList&& list, std::size_t index, TItem& element) noexcept
    {
        if (list.size() > index)
        {
            element = list[index];
            return true;
        }
        else
        {
            element = TItem{};
            return false;
        }
    }

    template<Interfaces::CList TList>
    static bool AddAndReturnTrue(TList& list, auto&& element)
    {
        list.push_back(std::forward<decltype(element)>(element));
        return true;
    }

    template<Interfaces::CList TList>
    static bool AddFirstAndReturnTrue(TList& list, Interfaces::CArray auto&& elements)
    {
        AddFirst(list, std::forward<decltype(elements)>(elements));
        return true;
    }

    template<Interfaces::CList TList>
    static void AddFirst(TList& list, Interfaces::CArray auto&& elements)
    {
        auto&& element = *std::ranges::begin(std::forward<decltype(elements)>(elements));
        list.push_back(std::forward<decltype(element)>(element));
    }

    template<Interfaces::CList TList>
    static bool AddAllAndReturnTrue(TList& list, Interfaces::CArray auto&& elements)
    {
        AddAll(list, std::forward<decltype(elements)>(elements));
        return true;
    }

    template<Interfaces::CList TList>
    static void AddAll(TList& list, Interfaces::CArray auto&& elements)
    {
        for (auto&& element : elements)
        {
            list.push_back(element);
        }
    }

    template<Interfaces::CList TList>
    static bool AddSkipFirstAndReturnTrue(TList& list, Interfaces::CArray auto&& elements)
    {
        AddSkipFirst(std::forward<decltype(elements)>(elements));
        return true;
    }

    template<Interfaces::CList TList>
    static void AddSkipFirst(TList& list, Interfaces::CArray auto&& elements)
    {
        AddSkipFirst(list, std::forward<decltype(elements)>(elements), 1);
    }

    template<Interfaces::CList TList>
    static void AddSkipFirst(TList& list, Interfaces::CArray auto&& elements, std::size_t skip)
    {
        for (auto&& element : elements | std::views::drop(skip))
        {
            list.push_back(element);
        }
    }
}
