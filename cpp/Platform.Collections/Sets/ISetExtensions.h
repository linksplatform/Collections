namespace Platform::Collections::Sets
{
    // TODO
    //  хотелось бы иметь такой синтаксис
    //  void Foo(Interfaces::ISet set, Interfaces::_::ISet<decltype(set)>::Item element)
    //  или такой
    //  template<Interfaces::ISet TSet>
    //  void Foo(Interfaces::ISet set, Interfaces::_::ISet<TSet>::Item element)
    //  пока что это заменено на auto
    static void AddAndReturnVoid(Interfaces::ISet auto& set, auto element)
    {
        set.insert(element);
    }

    static void RemoveAndReturnVoid(Interfaces::ISet auto& set, auto element)
    {
        set.erase(element);
    }

    static bool AddAndReturnTrue(Interfaces::ISet auto& set, auto element)
    {
        set.insert(element);
        return true;
    }

    // TODO
    //  тут бы тоже в идеале
    //  requires std::convertible_to<ISet<TSet>::Item, IArray<TSet>::Item>
    static bool AddFirstAndReturnTrue(Interfaces::ISet auto& set, Interfaces::IArray auto& elements)
    {
        AddFirst(set, elements);
        return true;
    }

    static void AddFirst(Interfaces::ISet auto& set, Interfaces::IArray auto& elements)
    {
        set.insert(elements[0]);
    }

    static bool AddAllAndReturnTrue(Interfaces::ISet auto& set, Interfaces::IArray auto& elements)
    {
        AddAll(set, elements);
        return true;
    }

    static void AddAll(Interfaces::ISet auto& set, Interfaces::IArray auto& elements)
    {
        for (auto element : elements)
        {
            set.insert(element);
        }
    }

    static bool AddSkipFirstAndReturnTrue(Interfaces::ISet auto& set, Interfaces::IArray auto& elements)
    {
        AddSkipFirst(set, elements);
        return true;
    }

    static void AddSkipFirst(Interfaces::ISet auto& set, Interfaces::IArray auto& elements)
    {
        AddSkipFirst(set, elements, 1);
    }

    static void AddSkipFirst(Interfaces::ISet auto& set, Interfaces::IArray auto& elements, std::int32_t skip)
    {
        for (auto i = skip; i < elements.size(); i++)
        {
            set.insert(elements[i]);
        }
    }

    static bool DoNotContains(const Interfaces::ISet auto& set, auto element)
    {
        return !set.contains(element);
    }
}
