namespace Platform::Collections::Lists
{
    // TODO странно, что перегрузки методов объявлены выше их "оригиналов"
    class IListExtensions
    {
        public: template <std::default_initializable T> static T GetElementOrDefault(IList<T> auto& list, std::integral auto index)
        {
            return list.size() > index ? list[index] : 0;
        }

        public: template <typename T> static bool TryGetElement(IList<T> auto& list, std::int32_t index, T& element)
        {
            if (list.size() > index)
            {
                element = list[index];
                return true;
            }
            else
            {
                //element = 0; TODO точно есть смысл от этого?
                return false;
            }
        }

        public: template <typename T> static bool AddAndReturnTrue(IList<T> auto& list, T element)
        {
            list.push_back(element);
            return true;
        }

        public: template <typename T> static bool AddFirstAndReturnTrue(IList<T> auto& list, const IList<T> auto& elements)
        {
            AddFirst<T>(list, elements);
            return true;
        }

        public: template <typename T> static void AddFirst(IList<T> auto& list, const BaseArray<T> auto& elements) { list.push_back(elements[0]); }

        public: template <typename T> static bool AddAllAndReturnTrue(IList<T> auto& list, Array<T> auto& elements)
        {
            AddAll<T>(list, elements);
            return true;
        }

        public: template <typename T> static void AddAll(IList<T> auto& list, const Array<T> auto& elements)
        {
            for (auto i = 0; i < elements.size(); i++)
            {
                list.push_back(elements[i]);
            }
        }
    
        public: template <typename T> static bool AddSkipFirstAndReturnTrue(IList<T> auto& list, const Array<T> auto& elements)
        {
            AddSkipFirst<T>(elements);
            return true;
        }

        public: template <typename T> static void AddSkipFirst(IList<T> auto& list, const Array<T> auto& elements) { AddSkipFirst<T>(list, elements, 1); }

        public: template <typename T> static void AddSkipFirst(IList<T> auto& list, const Array<T> auto& elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.size(); i++)
            {
                list.push_back(elements[i]);
            }
        }

        // TODO разве 'int' может быть 'null'
        public: template <typename T> static auto GetCountOrZero(const IList<T> auto& list) { return list.size(); }

        public: template <typename T> static bool EqualTo(const IList<T> auto& left, const IList<T> auto& right) { return EqualTo<T>(left, right, ContentEqualTo); }

        public: template <typename T, IList<T> TList>
        static bool EqualTo(const TList& left, const TList& right, std::function<bool(TList, TList)> contentEqualityComparer)
        {
            auto leftCount = left.size();
            auto rightCount = right.size();
            if (leftCount == 0 && rightCount == 0)
            {
                return true;
            }
            if (leftCount == 0 || rightCount == 0 || leftCount != rightCount)
            {
                return false;
            }
            return contentEqualityComparer(left, right);
        }

        public: template <typename T, IList<T> TList> requires requires(TList list, int a, int b) {list[a] == list[b];}
        static bool ContentEqualTo(const TList& left, const TList& right)
        {
            if constexpr(requires(TList a, TList b) {a == b;})
            {
                return left == right;
            }

            for (auto i = left.Count() - 1; i >= 0; --i)
            {
                if (!(left[i] == right[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /* TODO А что с этим то делать :(
        public: static T ToArray[]<T>(IList<T> &list, Func<T, bool> predicate)
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


        public: static T ToArray[]<T>(IList<T> &list)
        {
            auto array = T[list.Count()];
            list.CopyTo(array, 0);
            return array;
        }
        */
        
        public: template <typename T> static void ForEach(const IList<T> auto& list, std::function<void(T)> action)
        {
            for (auto i = 0; i < list.size(); i++)
            {
                action(list[i]);
            }
        }


        public: template <typename T, IList<T> TList> requires requires(TList list, int a, int b) {list[a] <=> list[b];}
        static std::int32_t CompareTo(const TList& left, const TList& right)
        {
            if constexpr(requires(TList a, TList b) {a <=> b;})
            {
                return std::bit_cast<std::int8_t>(left <=> right);
            }

            auto leftCount = left.size();
            auto rightCount = right.size();
            auto intermediateResult = (leftCount <=> rightCount);
            for (auto i = 0; intermediateResult == std::weak_ordering::equivalent && i < leftCount; i++)
            {
                intermediateResult = (left[i] <=> right[i]);
            }
            return std::bit_cast<std::int8_t>(intermediateResult);
        }

        /*
        public: static T SkipFirst[]<T>(IList<T> &list) { return list.SkipFirst(1); }
    
        public: static T SkipFirst[]<T>(IList<T> &list, std::int32_t skip)
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

        public: template<typename T> static auto ShiftRight(const IList<T> auto& list)
        {
            return GenericArrayExtensions::ShiftRight<T>(list);
        }

        public: template<typename T> static auto ShiftRight(const IList<T> auto& list, std::int32_t shift)
        {
            return GenericArrayExtensions::ShiftRight<T>(list, shift);
        }

    };
}
