namespace Platform::Collections::Arrays
{
    namespace GenericArrayExtensions
    {
        template<System::Array TArray>
        requires std::default_initializable<std::ranges::range_value_t<TArray>>
        static auto GetElementOrDefault(const TArray& array, std::integral auto index)
        {
            using TItem = std::ranges::range_value_t<TArray>;
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

        static auto inline Clone(System::Array auto& list)
        {
            using TArray = decltype(list);
            using TItem = std::ranges::range_value_t<TArray>;

            constexpr auto not_copyable =
                std::same_as<TArray, std::span<TItem>> ||
                std::is_array_v<TArray>;

            if constexpr (not_copyable)
            {
                auto copy = list;
                return std::ranges::copy(list, std::ranges::begin(copy));
            }

            return list;
        }

        template<typename T, System::Array TArray>
        requires
            requires(int size){TArray(size);}
            &&
            std::default_initializable<T>
        static auto ShiftRight(const TArray& array, std::integral auto shift)
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
                auto restrictions = TArray(std::ranges::size(array) + shift);
                std::ranges::copy(array, std::ranges::begin(array) + shift);
                return restrictions;
            }
        }

        static auto ShiftRight(const auto& array)
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
    };// namespace GenericArrayExtensions
}// namespace Platform::Collections::Arrays
