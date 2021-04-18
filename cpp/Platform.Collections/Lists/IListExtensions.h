namespace Platform::Collections::Lists
{
    // TODO странно, что перегрузки методов объявлены выше их "оригиналов"
    namespace IListExtensions
    {
        template <std::default_initializable T>
        static T GetElementOrDefault(Platform::Collections::System::IList<T> auto& list, std::integral auto index)
        {
            return list.size() > index ? list[index] : 0;
        }

        template <typename T>
        static bool TryGetElement(Platform::Collections::System::IList<T> auto& list, std::int32_t index, T& element)
        {
            if (list.size() > index)
            {
                element = list[index];
                return true;
            }
            else
            {
                element = T{};
                return false;
            }
        }

        template <typename T>
        static bool AddAndReturnTrue(Platform::Collections::System::IList<T> auto& list, T element)
        {
            list.push_back(element);
            return true;
        }

        template <typename T>
        static bool AddFirstAndReturnTrue(Platform::Collections::System::IList<T> auto& list, Platform::Collections::System::IList<T> auto elements)
        {
            AddFirst<T>(list, elements);
            return true;
        }

        template <typename T>
        static void AddFirst(Platform::Collections::System::IList<T> auto& list, Platform::Collections::System::BaseArray<T> auto elements)
        {
            list.push_back(elements[0]);
        }

        template <typename T>
        static bool AddAllAndReturnTrue(Platform::Collections::System::IList<T> auto& list, Platform::Collections::System::Array<T> auto elements)
        {
            AddAll<T>(list, elements);
            return true;
        }

        template <typename T>
        static void AddAll(Platform::Collections::System::IList<T> auto& list, Platform::Collections::System::Array<T> auto elements)
        {
            for (auto i = 0; i < elements.size(); i++)
            {
                list.push_back(elements[i]);
            }
        }
    
        template <typename T>
        static bool AddSkipFirstAndReturnTrue(Platform::Collections::System::IList<T> auto& list, Platform::Collections::System::Array<T> auto elements)
        {
            AddSkipFirst<T>(elements);
            return true;
        }

        template <typename T>
        static void AddSkipFirst(Platform::Collections::System::IList<T> auto& list, Platform::Collections::System::Array<T> auto elements)
        {
            AddSkipFirst<T>(list, elements, 1);
        }

        template <typename T>
        static void AddSkipFirst(Platform::Collections::System::IList<T> auto& list, Platform::Collections::System::Array<T> auto elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.size(); i++)
            {
                list.push_back(elements[i]);
            }
        }

        // TODO разве в C# 'int' может быть 'null' (всё-таки может)
        template <typename T>
        static auto GetCountOrZero(Platform::Collections::System::IList<T> auto list)
        {
            return list.size();
        }

        template <typename T, Platform::Collections::System::IList<T> TList>
        requires Platform::Collections::System::IEquatable<T>
        static bool ContentEqualTo(TList left, TList right)
        {
            for (auto i = left.size() - 1; i >= 0; --i)
            {
                if (!(left[i] == right[i]))
                {
                    return false;
                }
            }
            return true;
        }

        template <typename T, Platform::Collections::System::IList<T> TList>
        static bool EqualTo(TList left, TList right)
        {
            if constexpr(Platform::Collections::System::IEquatable<TList>)
            {
                return left == right;
            }
            return EqualTo<T>(left, right, ContentEqualTo);
        }

        template <typename T, Platform::Collections::System::IList<T> TList>
        static bool EqualTo(TList left, TList right, std::function<bool(TList, TList)> contentEqualityComparer)
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
        



        // TODO метод 'ForEach' удалён за ненадобностью
        // TODO метод 'CompareTo' удалён за ненадобностью

        /*
        static T SkipFirst[]<T>(IList<T> &list) { return list.SkipFirst(1); }
    
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

        template<typename T>
        static auto ShiftRight(Platform::Collections::System::IList<T> auto list)
        {
            return Platform::Collections::Arrays::GenericArrayExtensions::ShiftRight<T>(list);
        }

        template<typename T>
        static auto ShiftRight(Platform::Collections::System::IList<T> auto list, std::int32_t shift)
        {
            return Platform::Collections::Arrays::GenericArrayExtensions::ShiftRight<T>(list, shift);
        }
    };
}
