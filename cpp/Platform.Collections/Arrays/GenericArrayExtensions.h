namespace Platform::Collections::Arrays
{
    template<System::Array TArray>
    requires std::default_initializable<std::ranges::range_value_t<TArray>>
    static auto GetElementOrDefault(const TArray& array, std::integral auto index)
    {
        using TItem = std::ranges::range_value_t<TArray>;
        return std::ranges::size(array) > index ? array[index] : TItem{};
    }

    // TODO: Might use `typename System::Common::Array<decltype(array)>::TItem&` instead auto&
    static bool TryGetElement(const System::Array auto& array, std::integral auto index, auto& element)
    {
        if (std::ranges::size(array) > index)
        {
            element = array[index];
            return true;
        }
        else
        {
            element = 0;
            return false;
        }
    }

    static auto ShiftRight(const System::Array auto& array, std::integral auto shift)
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
            //  глянул на реализацию шарпа. Подумал, что можно было бы возвращать System::Array auto замест auto
            //  типо интерфейсный стиль. Хотя тогда по-хорошему можно будет делать только std::ranges:: штуки по типу size, begin и тд
            //  ну и типа вектор возвращать. По тому как нынешняя реализация требует конструктор, который бы выделил памяти кусок
            auto restrictions = TArray(std::ranges::size(array) + shift);
            std::ranges::copy(array, std::ranges::begin(array) + shift);
            return restrictions;
        }
    }

    static auto ShiftRight(const System::Array auto& array)
    {
        return ShiftRight(array, 1);
    }

    static void Add(System::Array auto& array, std::integral auto& position, auto element)
    {
        array[position++] = element;
    }

    static auto AddAndReturnConstant(System::Array auto& array, std::integral auto& position, auto element, auto returnConstant)
    {
        Add(array, position, element);
        return returnConstant;
    }

    static void AddFirst(System::Array auto& array, std::integral auto& position, System::Array auto elements)
    {
        array[position++] = elements[0];
    }

    static auto AddFirstAndReturnConstant(System::Array auto& array, std::integral auto& position, System::Array auto elements, auto returnConstant)
    {
        AddFirst(array, position, elements);
        return returnConstant;
    }

    static void AddAll(System::Array auto& array, std::integral auto& position, System::Array auto elements)
    {
        for (auto i = 0; i < elements.size(); i++)
        {
            Add(array, position, elements[i]);
        }
    }

    static auto AddAllAndReturnConstant(System::Array auto& array, std::integral auto& position, System::Array auto elements, auto returnConstant)
    {
        AddAll(array, position, elements);
        return returnConstant;
    }

    static void AddSkipFirst(System::Array auto& array, std::integral auto& position, System::Array auto elements, std::integral auto skip)
    {
        for (auto i = skip; i < elements.size(); i++)
        {
            Add(array, position, elements[i]);
        }
    }

    static void AddSkipFirst(System::Array auto& array, std::integral auto& position, const System::Array auto& elements)
    {
        AddSkipFirst(array, position, elements, 1);
    }

    static auto AddSkipFirstAndReturnConstant(System::Array auto& array, std::integral auto& position, const System::Array auto& elements, auto returnConstant)
    {
        AddSkipFirst(array, position, elements);
        return returnConstant;
    }
}// namespace Platform::Collections::Arrays
