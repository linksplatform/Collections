namespace Platform::Collections::Arrays
{
    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    requires std::default_initializable<TItem>
    static auto&& GetElementOrDefault(TArray&& array, std::integral auto index) noexcept
    {
        return (std::ranges::size(array) > index && index >= 0) ? array[index] : TItem{};
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    requires std::default_initializable<TItem>
    static bool TryGetElement(TArray&& array, std::integral auto index, TItem& element) noexcept
    {
        if (index >= 0 && std::ranges::size(array) > index)
        {
            element = array[index];
            return true;
        }
        else
        {
            element = {};
            return false;
        }
    }

    static Interfaces::IArray auto ShiftRight(const Interfaces::IArray auto& array, std::integral auto shift)
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
    static auto&& AddAndReturnConstant(TArray& array, std::integral auto& position, const TItem& element, const auto& returnConstant)
    {
        Add(array, position, element);
        return returnConstant;
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void AddFirst(TArray& array, std::integral auto& position, Interfaces::IArray<TItem> auto&& elements)
    {
        array[position++] = elements[0];
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static auto&& AddFirstAndReturnConstant(TArray& array, std::integral auto& position, Interfaces::IArray<TItem> auto&& elements, const auto& returnConstant)
    {
        AddFirst(array, position, elements);
        return returnConstant;
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void AddAll(TArray& array, std::integral auto& position, Interfaces::IArray<TItem> auto&& elements)
    {
        for (const auto& element : elements)
        {
            Add(array, position, element);
        }
    }

    static auto&& AddAllAndReturnConstant(Interfaces::IArray auto& array, std::integral auto& position, Interfaces::IArray auto elements, auto returnConstant)
    {
        AddAll(array, position, elements);
        return returnConstant;
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void AddSkipFirst(TArray& array, std::integral auto& position, Interfaces::IArray<TItem> auto elements, std::integral auto skip)
    {
        for (auto&& element : elements | std::views::drop(skip))
        {
            Add(array, position, std::forward<decltype(element)>(element));
        }
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static void AddSkipFirst(TArray& array, std::integral auto& position, Interfaces::IArray<TItem> auto&& elements)
    {
        AddSkipFirst(array, position, elements, 1);
    }

    template<Interfaces::IArray TArray, typename TItem = typename Interfaces::Array<TArray>::Item>
    static auto&& AddSkipFirstAndReturnConstant(TArray& array, std::integral auto& position, Interfaces::IArray<TItem> auto&& elements, auto&& constant)
    {
        AddSkipFirst(array, position, elements, 1);
        return std::forward<decltype(constant)>(constant);
    }
}// namespace Platform::Collections::Arrays
