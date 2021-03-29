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
        public: template <typename T, Array<T> TArray> static auto ShiftRight(TArray &array, std::int64_t shift)
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
                // TODO данная реализация не гарантирует default заполнение новых элементов массива
                // то есть [1, 2, 3] >> 3 не будет равен [0, 0, 0, 1, 2, 3]
                auto restrictions = new T[array.size() + shift];
                std::copy(array.data(), array.data() + array.size(), restrictions + shift);
                return TArray(restrictions, restrictions + array.size() + shift);
            }
        }

        public: template <typename T> static void Add(Array<T> auto& array, std::int32_t &position, T element) { array[position++] = element; }

        public: template<typename T> static void Add(Array<T> auto& array, std::int64_t &position, T element) { array[position++] = element; }

        public: template<typename TElement, typename TReturnConstant> static TReturnConstant AddAndReturnConstant(Array<TElement> auto& array, std::int64_t &position, TElement element, TReturnConstant returnConstant)
        {
            Add(array, position, element);
            return returnConstant;
        }

        public: template <typename T> static void AddFirst(Array<T> auto& array, std::int64_t &position, const Array<T> auto& elements) { array[position++] = elements[0]; }

        public: template<typename TElement, typename TReturnConstant> static TReturnConstant AddFirstAndReturnConstant(Array<TElement> auto& array, std::int64_t &position, const Array<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddFirst(array, position, elements);
            return returnConstant;
        }

        public: template<typename TElement, typename TReturnConstant> static TReturnConstant AddAllAndReturnConstant(Array<TElement> auto& array, std::int64_t &position, const Array<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddAll(array, position, elements);
            return returnConstant;
        }

        public: template <typename T> static void AddAll(Array<T> auto& array, std::int64_t &position, const Array<T> auto& elements)
        {
            for (auto i = 0; i < elements.size(); i++)
            {
                Add(array, position, elements[i]);
            }
        }

        public: template<typename TElement, typename TReturnConstant> static TReturnConstant AddSkipFirstAndReturnConstant(Array<TElement> auto& array, std::int64_t &position, const Array<TElement> auto& elements, TReturnConstant returnConstant)
        {
            AddSkipFirst(array, position, elements);
            return returnConstant;
        }

        public: template <typename T> static void AddSkipFirst(Array<T> auto& array,std::int64_t position, const Array<T> auto& elements) { AddSkipFirst(array, position, elements, 1); }
        
        public: template <typename T> static void AddSkipFirst(Array<T> auto& array, std::int64_t &position, const Array<T> auto& elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.Count(); i++)
            {
                Add(array, position, elements[i]);
            }
        }
    };
}
