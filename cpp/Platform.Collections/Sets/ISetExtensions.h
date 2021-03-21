namespace Platform::Collections::Sets
{
    class ISetExtensions
    {
        public: template <typename T> static void AddAndReturnVoid(ISet<T> &set, T element) { set.Add(element); }

        public: template <typename T> static void RemoveAndReturnVoid(ISet<T> &set, T element) { set.Remove(element); }

        public: template <typename T> static bool AddAndReturnTrue(ISet<T> &set, T element)
        {
            set.Add(element);
            return true;
        }

        public: template <typename T> static bool AddFirstAndReturnTrue(ISet<T> &set, IList<T> &elements)
        {
            AddFirst(set, elements);
            return true;
        }

        public: template <typename T> static void AddFirst(ISet<T> &set, IList<T> &elements) { set.Add(elements[0]); }

        public: template <typename T> static bool AddAllAndReturnTrue(ISet<T> &set, IList<T> &elements)
        {
            set.AddAll(elements);
            return true;
        }

        public: template <typename T> static void AddAll(ISet<T> &set, IList<T> &elements)
        {
            for (auto i = 0; i < elements.Count(); i++)
            {
                set.Add(elements[i]);
            }
        }

        public: template <typename T> static bool AddSkipFirstAndReturnTrue(ISet<T> &set, IList<T> &elements)
        {
            set.AddSkipFirst(elements);
            return true;
        }

        public: template <typename T> static void AddSkipFirst(ISet<T> &set, IList<T> &elements) { set.AddSkipFirst(elements, 1); }

        public: template <typename T> static void AddSkipFirst(ISet<T> &set, IList<T> &elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.Count(); i++)
            {
                set.Add(elements[i]);
            }
        }

        public: template <typename T> static bool DoNotContains(ISet<T> &set, T element) { return !set.Contains(element); }
    };
}
