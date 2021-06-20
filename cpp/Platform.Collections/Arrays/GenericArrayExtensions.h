namespace Platform::Collections::Arrays
{
    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    requires std::default_initializable<TItem>
    static auto GetElementOrDefault(const TArray& array, std::integral auto index) noexcept
    {
        return (std::ranges::size(array) > index && index >= 0) ? std::ranges::begin(array)[index] : TItem{};
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    requires std::default_initializable<TItem>
    static bool TryGetElement(const TArray& array, std::integral auto index, TItem& element) noexcept
    {
        if (std::ranges::size(array) > index && index >= 0)
        {
            element = std::ranges::begin(array)[index];
            return true;
        }
        else
        {
            element = TItem{};
            return false;
        }
    }

    static auto ShiftRight(const Interfaces::IArray auto& array, std::integral auto shift)
    {
        if (shift < 0)
        {
            throw std::logic_error("Not implemented exception.");
        }
        if (shift == 0)
        {
            return array;
        }
        else
        {
            using Item = typename Interfaces::Enumerable<decltype(array)>::Item;
            // TODO в оригинале возвращает IList, значит и мы так поступим
            auto restrictions = std::vector<Item>(std::ranges::size(array) + shift);
            std::ranges::copy(array, std::ranges::begin(restrictions) + shift);
            return restrictions;
        }
    }

    static auto ShiftRight(const Interfaces::IArray auto& array)
    {
        return ShiftRight(array, 1);
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void Add(TArray& array, std::integral auto& position, const TItem& element)
    {
        std::ranges::begin(array)[position++] = element;
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static decltype(auto) AddAndReturnConstant(TArray& array, std::integral auto& position, const TItem& element, const auto& returnConstant)
    {
        Add(array, position, element);
        return returnConstant;
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void AddFirst(TArray& array, std::integral auto& position, const Interfaces::IArray<TItem> auto& elements)
    {
        std::ranges::begin(array)[position++] = std::ranges::begin(elements)[0];
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static decltype(auto) AddFirstAndReturnConstant(TArray& array, std::integral auto& position, const Interfaces::IArray<TItem> auto& elements, const auto& returnConstant)
    {
        AddFirst(array, position, elements);
        return returnConstant;
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void AddAll(TArray& array, std::integral auto& position, const Interfaces::IArray<TItem> auto& elements)
    {
        for (const auto& element : elements)
        {
            Add(array, position, element);
        }
    }

    static decltype(auto) AddAllAndReturnConstant(Interfaces::IArray auto& array, std::integral auto& position, Interfaces::IArray auto elements, auto returnConstant)
    {
        AddAll(array, position, elements);
        return returnConstant;
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void AddSkipFirst(TArray& array, std::integral auto& position, Interfaces::IArray<TItem> auto elements, std::integral auto skip)
    {
        for (const auto& element : elements | std::views::drop(skip))
        {
            Add(array, position, element);
        }
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void AddSkipFirst(TArray& array, std::integral auto& position, const Interfaces::IArray<TItem> auto& elements)
    {
        AddSkipFirst(array, position, elements, 1);
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static decltype(auto) AddSkipFirstAndReturnConstant(TArray& array, std::integral auto& position, const Interfaces::IArray<TItem> auto& elements, const auto& constant)
    {
        AddSkipFirst(array, position, elements, 1);
        return constant;
    }
}// namespace Platform::Collections::Arrays
