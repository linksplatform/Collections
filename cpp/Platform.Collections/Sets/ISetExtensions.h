namespace Platform::Collections::Sets
{
    namespace ISetExtensions
    {
        template <typename T> static void AddAndReturnVoid(Platform::Collections::System::ISet<T> auto& set, T element)
        {
            set.insert(element);
        }

        template <typename T> static void RemoveAndReturnVoid(Platform::Collections::System::ISet<T> auto& set, T element)
        {
            set.erase(element);
        }

        template <typename T> static bool AddAndReturnTrue(Platform::Collections::System::ISet<T> auto& set, T element)
        {
            set.insert(element);
            return true;
        }

        template <typename T> static bool AddFirstAndReturnTrue(Platform::Collections::System::ISet<T> auto& set, Platform::Collections::System::IList<T> auto& elements)
        {
            AddFirst(set, elements);
            return true;
        }

        template <typename T> static void AddFirst(Platform::Collections::System::ISet<T> auto& set, Platform::Collections::System::IList<T> auto& elements)
        {
            set.insert(elements[0]);
        }

        template <typename T> static bool AddAllAndReturnTrue(Platform::Collections::System::ISet<T> auto& set, Platform::Collections::System::IList<T> auto& elements)
        {
            AddAll<T>(set, elements);
            return true;
        }

        template <typename T> static void AddAll(Platform::Collections::System::ISet<T> auto& set, Platform::Collections::System::IList<T> auto& elements)
        {
            for (auto i = 0; i < elements.Count(); i++)
            {
                set.insert(elements[i]);
            }
        }

        template <typename T> static bool AddSkipFirstAndReturnTrue(Platform::Collections::System::ISet<T> auto& set, Platform::Collections::System::IList<T> auto& elements)
        {
            AddSkipFirst<T>(set, elements);
            return true;
        }

        template <typename T> static void AddSkipFirst(Platform::Collections::System::ISet<T> auto& set, Platform::Collections::System::IList<T> auto& elements)
        {
            AddSkipFirst<T>(set, elements, 1);
        }

        template <typename T> static void AddSkipFirst(Platform::Collections::System::ISet<T> auto& set, Platform::Collections::System::IList<T> auto& elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.size(); i++)
            {
                set.insert(elements[i]);
            }
        }

        template <typename T> static bool DoNotContains(Platform::Collections::System::ISet<T> auto& set, T element)
        {
            return !set.contains(element);
        }
    };
}
