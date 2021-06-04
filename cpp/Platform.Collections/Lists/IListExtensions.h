namespace Platform::Collections::Lists
{
    template<System::IList TList>
    requires std::default_initializable<std::ranges::range_value_t<TList>>
    static auto GetElementOrDefault(const TList& array, std::integral auto index)
    {
        using TItem = std::ranges::range_value_t<TList>;
        return std::ranges::size(array) > index ? array[index] : TItem{};
    }

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

    static bool AddAndReturnTrue(System::IList auto& list, auto element)
    {
        list.push_back(element);
        return true;
    }

    static bool AddFirstAndReturnTrue(System::IList auto& list, const System::Array auto& elements)
    {
        AddFirst(list, elements);
        return true;
    }

    static void AddFirst(System::IList auto& list, const System::Array auto& elements)
    {
        list.push_back(elements[0]);
    }

    static bool AddAllAndReturnTrue(System::IList auto& list, const System::Array auto& elements)
    {
        AddAll(list, elements);
        return true;
    }

    static void AddAll(System::IList auto& list, const System::Array auto& elements)
    {
        for (auto i = 0; i < elements.size(); i++)
        {
            list.push_back(elements[i]);
        }
    }

    static bool AddSkipFirstAndReturnTrue(System::IList auto& list, const System::Array auto& elements)
    {
        AddSkipFirst(elements);
        return true;
    }

    static void AddSkipFirst(System::IList auto& list, const System::Array auto& elements)
    {
        AddSkipFirst(list, elements, 1);
    }

    static void AddSkipFirst(System::IList auto& list, const System::Array auto& elements, std::int32_t skip)
    {
        for (auto i = skip; i < elements.size(); i++)
        {
            list.push_back(elements[i]);
        }
    }

    /*
     * Use Platform.Equality(or std::ranges::equal) instead of EqualTo and other
     */

    /* TODO А что с этим то делать :(
    static T ToArray[]<T>(IList<T> &list, Func<T, bool> predicate)
    {
        if (list == nullptr)
        {
            return {};
        }
        auto result = List<T>(list.Count());
        for (auto i = 0; i < list.Count(); i++)
        {
            if (predicate(list[i]))
            {
                result.Add(list[i]);
            }
        }
        return result.ToArray();
    }


    static T ToArray[]<T>(IList<T> &list)
    {
        auto array = T[list.Count()];
        list.CopyTo(array, 0);
        return array;
    }
    */

    /*
    static T SkipFirst[]<T>(IList<T> &list) { return list.SkipFirst(1); }

    // TODO: Fix translator
    //  static T[] Foo same as:
        // template<typename T>
        // static T* SkipFirst()
    static T SkipFirst[]<T>(IList<T> &list, std::int32_t skip)
    {
        if (list.IsNullOrEmpty() || list.Count() <= skip)
        {
            return Array.Empty<T>();
        }
        auto result = T[list.Count() - skip];
        for (std::int32_t r = skip, w = 0; r < list.Count(); r++, w++)
        {
            result[w] = list[r];
        }
        return result;
    }
    */

    static auto ShiftRight(const System::IList auto& list)
    {
        return Arrays::ShiftRight(list);
    }

    static auto ShiftRight(const System::IList auto& list, std::int32_t shift)
    {
        return Arrays::ShiftRight(list, shift);
    }
}// namespace Platform::Collections::Lists
