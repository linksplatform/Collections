namespace Platform::Collections::Sets
{
    // TODO
    //  хотелось бы иметь такой синтаксис
    //  void Foo(System::ISet set, System::_::ISet<decltype(set)>::Item element)
    //  или такой
    //  template<System::ISet TSet>
    //  void Foo(System::ISet set, System::_::ISet<TSet>::Item element)
    //  пока что это заменено на auto
    static void AddAndReturnVoid(System::ISet auto& set, auto element)
    {
        set.insert(element);
    }

    static void RemoveAndReturnVoid(System::ISet auto& set, auto element)
    {
        set.erase(element);
    }

    static bool AddAndReturnTrue(System::ISet auto& set, auto element)
    {
        set.insert(element);
        return true;
    }

    // TODO
    //  тут бы тоже в идеале
    //  requires std::convertible_to<ISet<TSet>::Item, IArray<TSet>::Item>
    static bool AddFirstAndReturnTrue(System::ISet auto& set, System::IArray auto& elements)
    {
        AddFirst(set, elements);
        return true;
    }

    static void AddFirst(System::ISet auto& set, System::IArray auto& elements)
    {
        set.insert(elements[0]);
    }

    static bool AddAllAndReturnTrue(System::ISet auto& set, System::IArray auto& elements)
    {
        AddAll(set, elements);
        return true;
    }

    static void AddAll(System::ISet auto& set, System::IArray auto& elements)
    {
        for (auto element : elements)
        {
            set.insert(element);
        }
    }

    static bool AddSkipFirstAndReturnTrue(System::ISet auto& set, System::IArray auto& elements)
    {
        AddSkipFirst(set, elements);
        return true;
    }

    static void AddSkipFirst(System::ISet auto& set, System::IArray auto& elements)
    {
        AddSkipFirst(set, elements, 1);
    }

    static void AddSkipFirst(System::ISet auto& set, System::IArray auto& elements, std::int32_t skip)
    {
        for (auto i = skip; i < elements.size(); i++)
        {
            set.insert(elements[i]);
        }
    }

    static bool DoNotContains(const System::ISet auto& set, auto element)
    {
        return !set.contains(element);
    }
}
