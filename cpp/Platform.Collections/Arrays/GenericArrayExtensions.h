namespace Platform::Collections::Arrays
{
    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    requires std::default_initializable<TItem>
    static auto GetElementOrDefault(const TArray& array, std::integral auto index)
    {
        return std::ranges::size(array) > index ? array[index] : TItem{};
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    requires std::default_initializable<TItem>
    static bool TryGetElement(const TItem& array, std::integral auto index, TItem& element)
    {
        if (std::ranges::size(array) > index)
        {
            element = array[index];
            return true;
        }
        else
        {
            element = TItem{};
            return false;
        }
    }

    static auto ShiftRight(const System::IArray auto& array, std::integral auto shift)
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
            // TODO: Снова вернулся к этому
            //  глянул на реализацию шарпа. Подумал, что можно было бы возвращать System::IArray auto замест auto
            //  типо интерфейсный стиль. Хотя тогда по-хорошему можно будет делать только std::ranges:: штуки по типу size, begin и тд
            //  ну и типа вектор возвращать. По тому как нынешняя реализация требует конструктор, который бы выделил памяти кусок
            auto restrictions = TArray(std::ranges::size(array) + shift);
            std::ranges::copy(array, std::ranges::begin(array) + shift);
            return restrictions;
        }
    }

    static auto ShiftRight(const System::IArray auto& array)
    {
        return ShiftRight(array, 1);
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    static void Add(TArray& array, std::integral auto& position, const TItem& element)
    {
        array[position++] = element;
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    static decltype(auto) AddAndReturnConstant(TArray& array, std::integral auto& position, const TItem& element, const auto& returnConstant)
    {
        Add(array, position, element);
        return returnConstant;
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    static void AddFirst(TArray& array, std::integral auto& position, const System::IArray<TItem> auto& elements)
    {
        array[position++] = elements[0];
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    static decltype(auto) AddFirstAndReturnConstant(TArray& array, std::integral auto& position, const System::IArray<TItem> auto& elements, const auto& returnConstant)
    {
        AddFirst(array, position, elements);
        return returnConstant;
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    static void AddAll(TArray& array, std::integral auto& position, const System::IArray<TItem> auto& elements)
    {
        for (const auto& element : elements)
        {
            Add(array, position, element);
        }
    }

    static decltype(auto) AddAllAndReturnConstant(System::IArray auto& array, std::integral auto& position, System::IArray auto elements, auto returnConstant)
    {
        AddAll(array, position, elements);
        return returnConstant;
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    static void AddSkipFirst(TArray& array, std::integral auto& position, System::IArray<TItem> auto elements, std::integral auto skip)
    {
        for (const auto& element : elements | std::views::drop(skip))
        {
            Add(array, position, element);
        }
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    static void AddSkipFirst(TArray& array, std::integral auto& position, const System::IArray<TItem> auto& elements)
    {
        AddSkipFirst(array, position, elements, 1);
    }

    template<System::IArray TArray, typename TItem = typename System::Array<TArray>::Item>
    static decltype(auto) AddSkipFirstAndReturnConstant(TArray& array, std::integral auto& position, const System::IArray<TItem> auto& elements, const auto& constant)
    {
        AddSkipFirst(array, position, elements, 1);
        return constant;
    }
}// namespace Platform::Collections::Arrays
