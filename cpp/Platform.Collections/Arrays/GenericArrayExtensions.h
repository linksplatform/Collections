namespace Platform::Collections::Arrays
{
    class GenericArrayExtensions
    {
        public: template <std::default_initializable T> static T GetElementOrDefault(Array<T> auto& array, std::int32_t index) { return array.size() > index ? array[index] : T{}; }
        
        public: template <std::default_initializable T> static T GetElementOrDefault(Array<T> auto& array, std::int64_t index) { return array.size() > index ? array[index] : T{}; }


        public: template <typename T> static bool TryGetElement(Array<T> auto& array, std::int32_t index, T &element)
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
        
        public: template <typename T> static bool TryGetElement(Array<T> auto& array, std::int64_t index, T &element)
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

        public: template <typename T> static auto Clone(Array<T> auto& array)
        {
            return array;
        }

        public: template <typename T> static auto ShiftRight(Array<T> auto& array) { return ShiftRight<T>(array, 1LL); }


        // TODO Тут я слегка сменил обычный стиль 'Array auto& array' на этот, чтобы был доступен конструктор 'TArray'
        public: template <typename T, Array<T> TArray>
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

        public: template<typename T> static void Add(BaseArray<T> auto& array, std::integral auto& position, T element) { array[position++] = element; }

        public: template<typename TElement, typename TReturnConstant> static TReturnConstant AddAndReturnConstant(BaseArray<TElement> auto& array, std::integral auto& position, TElement element, TReturnConstant returnConstant)
        {
            Add<TElement>(array, position, element);
            return returnConstant;
        }

        public: template <typename T> static void AddFirst(BaseArray<T> auto& array, std::integral auto& position, const BaseArray<T> auto& elements) { array[position++] = elements[0]; }

        public: template<typename TElement, typename TReturnConstant> static TReturnConstant AddFirstAndReturnConstant(BaseArray<TElement> auto& array, std::integral auto& position, const BaseArray<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddFirst<TElement>(array, position, elements);
            return returnConstant;
        }

        public: template <typename T> static void AddAll(BaseArray<T> auto& array, std::integral auto& position, const Array<T> auto& elements)
        {
            for (auto i = 0; i < elements.size(); i++)
            {
                Add<T>(array, position, elements[i]);
            }
        }

        public: template<typename TElement, typename TReturnConstant> static TReturnConstant AddAllAndReturnConstant(BaseArray<TElement> auto& array, std::integral auto& position, const Array<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddAll<TElement>(array, position, elements);
            return returnConstant;
        }


        public: template <typename T> static void AddSkipFirst(BaseArray<T> auto& array, std::integral auto& position, const Array<T> auto& elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.size(); i++)
            {
                Add<T>(array, position, elements[i]);
            }
        }

        public: template <typename T> static void AddSkipFirst(BaseArray<T> auto& array, std::integral auto& position, const Array<T> auto& elements) { AddSkipFirst<T>(array, position, elements, 1); }

        public: template<typename TElement, typename TReturnConstant> static TReturnConstant AddSkipFirstAndReturnConstant(BaseArray<TElement> auto& array, std::integral auto& position, const Array<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddSkipFirst<TElement>(array, position, elements);
            return returnConstant;
        }
    };
}
