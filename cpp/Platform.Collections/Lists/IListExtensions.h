namespace Platform::Collections::Lists
{
    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    requires std::default_initializable<TItem>
    static auto GetElementOrDefault(const TList& list, std::integral auto index) noexcept
    {
        return (list.size() > index && index >= 0)  ? list[index] : TItem{};
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    requires std::default_initializable<TItem>
    static bool TryGetElement(const TList& list, std::integral auto index, TItem& element) noexcept
    {
        if (index >= 0 && list.size() > index)
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

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    static bool AddAndReturnTrue(TList& list, const TItem& element)
    {
        list.push_back(element);
        return true;
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    static bool AddFirstAndReturnTrue(TList& list, Interfaces::IArray<TItem> auto&& elements)
    {
        AddFirst(list, elements);
        return true;
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    static void AddFirst(TList& list, Interfaces::IArray<TItem> auto&& elements)
    {
        list.push_back(*std::ranges::begin(elements));
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    static bool AddAllAndReturnTrue(TList& list, Interfaces::IArray<TItem> auto&& elements)
    {
        AddAll(list, elements);
        return true;
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    static void AddAll(TList& list, Interfaces::IArray<TItem> auto&& elements)
    {
        for (auto&& element : elements)
        {
            list.push_back(element);
        }
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    static bool AddSkipFirstAndReturnTrue(TList& list, Interfaces::IArray<TItem> auto&& elements)
    {
        AddSkipFirst(elements);
        return true;
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    static void AddSkipFirst(TList& list, Interfaces::IArray<TItem> auto&& elements)
    {
        AddSkipFirst(list, elements, 1);
    }

    template<Interfaces::IList TList, typename TItem = typename Interfaces::List<TList>::Item>
    static void AddSkipFirst(TList& list, Interfaces::IArray<TItem> auto&& elements, std::integral auto skip)
    {
        for (auto&& element : elements | std::views::drop(skip))
        {
            list.push_back(element);
        }
    }

    static auto ShiftRight(Interfaces::IList auto&& list) { return Arrays::ShiftRight(list); }

    static auto ShiftRight(Interfaces::IList auto&& list, std::integral auto shift) { return Arrays::ShiftRight(list, shift); }
}
