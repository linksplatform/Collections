namespace Platform::Collections::Lists
{
    class IListExtensions
    {
        public: template <typename T> static T GetElementOrDefault(IList<T> &list, std::int32_t index) { return list != nullptr && list.Count() > index ? list[index] : 0; }

        public: template <typename T> static bool TryGetElement(IList<T> &list, std::int32_t index, out T element)
        {
            if (list != nullptr && list.Count() > index)
            {
                element = list[index];
                return true;
            }
            else
            {
                element = 0;
                return false;
            }
        }

        public: template <typename T> static bool AddAndReturnTrue(IList<T> &list, T element)
        {
            list.Add(element);
            return true;
        }

        public: template <typename T> static bool AddFirstAndReturnTrue(IList<T> &list, IList<T> &elements)
        {
            list.AddFirst(elements);
            return true;
        }

        public: template <typename T> static void AddFirst(IList<T> &list, IList<T> &elements) { list.Add(elements[0]); }

        public: template <typename T> static bool AddAllAndReturnTrue(IList<T> &list, IList<T> &elements)
        {
            list.AddAll(elements);
            return true;
        }

        public: template <typename T> static void AddAll(IList<T> &list, IList<T> &elements)
        {
            for (auto i = 0; i < elements.Count(); i++)
            {
                list.Add(elements[i]);
            }
        }
    
        public: template <typename T> static bool AddSkipFirstAndReturnTrue(IList<T> &list, IList<T> &elements)
        {
            list.AddSkipFirst(elements);
            return true;
        }

        public: template <typename T> static void AddSkipFirst(IList<T> &list, IList<T> &elements) { list.AddSkipFirst(elements, 1); }

        public: template <typename T> static void AddSkipFirst(IList<T> &list, IList<T> &elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.Count(); i++)
            {
                list.Add(elements[i]);
            }
        }

        public: template <typename T> static std::int32_t GetCountOrZero(IList<T> &list) { return list?.Count ?? 0; }

        public: template <typename T> static bool EqualTo(IList<T> &left, IList<T> &right) { return EqualTo(left, right, ContentEqualTo); }

        public: template <typename T> static bool EqualTo(IList<T> &left, IList<T> &right, Func<IList<T>, IList<T>, bool> contentEqualityComparer)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            auto leftCount = left.GetCountOrZero();
            auto rightCount = right.GetCountOrZero();
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

        public: template <typename T> static bool ContentEqualTo(IList<T> &left, IList<T> &right)
        {
            auto equalityComparer = EqualityComparer<T>.Default;
            for (auto i = left.Count() - 1; i >= 0; --i)
            {
                if (!equalityComparer.Equals(left[i], right[i]))
                {
                    return false;
                }
            }
            return true;
        }
        
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
        
        public: template <typename T> static void ForEach(IList<T> &list, std::function<void(T)> action)
        {
            for (auto i = 0; i < list.Count(); i++)
            {
                action(list[i]);
            }
        }

        public: template <typename T> static std::int32_t GenerateHashCode(IList<T> &list)
        {
            auto hashAccumulator = 17;
            for (auto i = 0; i < list.Count(); i++)
            {
                hashAccumulator = unchecked((hashAccumulator * 23) + list[i].GetHashCode());
            }
            return hashAccumulator;
        }

        public: template <typename T> static std::int32_t CompareTo(IList<T> &left, IList<T> &right)
        {
            auto comparer = Comparer<T>.Default;
            auto leftCount = left.GetCountOrZero();
            auto rightCount = right.GetCountOrZero();
            auto intermediateResult = leftCount.CompareTo(rightCount);
            for (auto i = 0; intermediateResult == 0 && i < leftCount; i++)
            {
                intermediateResult = comparer.Compare(left[i], right[i]);
            }
            return intermediateResult;
        }

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

        public: static IList<T> ShiftRight<T>(IList<T> &list) { return list.ShiftRight(1); }

        public: static IList<T> ShiftRight<T>(IList<T> &list, std::int32_t shift)
        {
            if (shift < 0)
            {
                throw std::logic_error("Not implemented exception.");
            }
            if (shift == 0)
            {
                return list.ToArray();
            }
            else
            {
                auto result = T[list.Count() + shift];
                for (std::int32_t r = 0, w = shift; r < list.Count(); r++, w++)
                {
                    result[w] = list[r];
                }
                return result;
            }
        }
    };
}
