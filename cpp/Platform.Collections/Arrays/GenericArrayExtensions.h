﻿namespace Platform::Collections::Arrays
{
    class GenericArrayExtensions
    {
        public: template <std::default_initializable T>
        static T GetElementOrDefault(Platform::Collections::System::Array<T> auto& array, std::int32_t index)
        {
            return array.size() > index ? array[index] : T{};
        }

        public: template <std::default_initializable T>
        static T GetElementOrDefault(Platform::Collections::System::Array<T> auto& array, std::int64_t index)
        {
            return array.size() > index ? array[index] : T{};
        }


        public: template <typename T>
        static bool TryGetElement(Platform::Collections::System::Array<T> auto& array, std::int32_t index, T &element)
        {
            if (array.size() > index)
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

        public: template <typename T>
        static bool TryGetElement(Platform::Collections::System::Array<T> auto& array, std::int64_t index, T &element)
        {
            if (array.size() > index)
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

        public: template <typename T>
        static auto Clone(Platform::Collections::System::Array<T> auto& array)
        {
            return array;
        }

        public: template <typename T>
        static auto ShiftRight(Platform::Collections::System::Array<T> auto& array)
        {
            return ShiftRight<T>(array, 1LL);
        }


        // TODO Тут я слегка сменил обычный стиль 'Array auto& array' на этот, чтобы был доступен конструктор 'TArray'
        public: template <typename T, Platform::Collections::System::Array<T> TArray>
        requires requires(int size) { TArray(size); } && // проверка на наличие конструктора
                 requires(T item) {T{};} // есть default конструктор
        static auto ShiftRight(TArray &array, std::int64_t shift)
        {
            if (shift < 0)
            {
                throw std::logic_error("Not implemented exception.");
            }
            if (shift == 0)
            {
                return GenericArrayExtensions::Clone<T>(array);
            }
            else
            {
                auto restrictions = TArray(array.size() + shift);
                std::ranges::copy(array, restrictions.begin() + shift);
                return restrictions;
            }
        }

        public: template<typename T>
        static void Add(Platform::Collections::System::BaseArray<T> auto& array, std::integral auto& position, T element)
        {
            array[position++] = element;
        }

        public: template<typename TElement, typename TReturnConstant>
        static TReturnConstant AddAndReturnConstant(Platform::Collections::System::BaseArray<TElement> auto& array, std::integral auto& position, TElement element, TReturnConstant returnConstant)
        {
            Add<TElement>(array, position, element);
            return returnConstant;
        }

        public: template <typename T>
        static void AddFirst(Platform::Collections::System::BaseArray<T> auto& array, std::integral auto& position, const Platform::Collections::System::BaseArray<T> auto& elements)
        {
            array[position++] = elements[0];
        }

        public: template<typename TElement, typename TReturnConstant>
        static TReturnConstant AddFirstAndReturnConstant(Platform::Collections::System::BaseArray<TElement> auto& array, std::integral auto& position, const Platform::Collections::System::BaseArray<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddFirst<TElement>(array, position, elements);
            return returnConstant;
        }

        public: template <typename T>
        static void AddAll(Platform::Collections::System::BaseArray<T> auto& array, std::integral auto& position, const Platform::Collections::System::Array<T> auto& elements)
        {
            for (auto i = 0; i < elements.size(); i++)
            {
                Add<T>(array, position, elements[i]);
            }
        }

        public: template<typename TElement, typename TReturnConstant>
        static TReturnConstant AddAllAndReturnConstant(Platform::Collections::System::BaseArray<TElement> auto& array, std::integral auto& position, const Platform::Collections::System::Array<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddAll<TElement>(array, position, elements);
            return returnConstant;
        }


        public: template <typename T>
        static void AddSkipFirst(Platform::Collections::System::BaseArray<T> auto& array, std::integral auto& position, const Platform::Collections::System::Array<T> auto& elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.size(); i++)
            {
                Add<T>(array, position, elements[i]);
            }
        }

        public: template <typename T>
        static void AddSkipFirst(Platform::Collections::System::BaseArray<T> auto& array, std::integral auto& position, const Platform::Collections::System::Array<T> auto& elements)
        {
            AddSkipFirst<T>(array, position, elements, 1);
        }

        public: template<typename TElement, typename TReturnConstant>
        static TReturnConstant AddSkipFirstAndReturnConstant(Platform::Collections::System::BaseArray<TElement> auto& array, std::integral auto& position, const Platform::Collections::System::Array<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddSkipFirst<TElement>(array, position, elements);
            return returnConstant;
        }
    };
}
