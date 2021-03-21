namespace Platform::Collections::Arrays
{
    class GenericArrayExtensions
    {
        public: template <typename T> static T GetElementOrDefault(T array[], std::int32_t index) { return array != nullptr && array.Length > index ? array[index] : 0; }
        
        public: template <typename T> static T GetElementOrDefault(T array[], std::int64_t index) { return array != nullptr && array.LongLength > index ? array[index] : 0; }

        public: template <typename T> static bool TryGetElement(T array[], std::int32_t index, out T element)
        {
            if (array != nullptr && array.Length > index)
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
        
        public: template <typename T> static bool TryGetElement(T array[], std::int64_t index, out T element)
        {
            if (array != nullptr && array.LongLength > index)
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

        public: static T Clone[]<T>(T array[])
        {
            auto copy = T[array.LongLength];
            Array.Copy(array, 0L, copy, 0L, array.LongLength);
            return copy;
        }

        public: static IList<T> ShiftRight<T>(T array[]) { return array.ShiftRight(1L); }
        
        public: static IList<T> ShiftRight<T>(T array[], std::int64_t shift)
        {
            if (shift < 0)
            {
                throw std::logic_error("Not implemented exception.");
            }
            if (shift == 0)
            {
                return array.Clone<T>();
            }
            else
            {
                auto restrictions = T[array.LongLength + shift];
                Array.Copy(array, 0L, restrictions, shift, array.LongLength);
                return restrictions;
            }
        }

        public: template <typename T> static void Add(T array[], ref std::int32_t position, T element) { array[position++] = element; }

        public: template <typename T> static void Add(T array[], ref std::int64_t position, T element) { array[position++] = element; }

        public: static TReturnConstant AddAndReturnConstant<TElement, TReturnConstant>(TElement array[], ref std::int64_t position, TElement element, TReturnConstant returnConstant)
        {
            array.Add(position, element);
            return returnConstant;
        }

        public: template <typename T> static void AddFirst(T array[], ref std::int64_t position, IList<T> &elements) { array[position++] = elements[0]; }

        public: static TReturnConstant AddFirstAndReturnConstant<TElement, TReturnConstant>(TElement array[], ref std::int64_t position, IList<TElement> &elements, TReturnConstant returnConstant)
        {
            array.AddFirst(position, elements);
            return returnConstant;
        }

        public: static TReturnConstant AddAllAndReturnConstant<TElement, TReturnConstant>(TElement array[], ref std::int64_t position, IList<TElement> &elements, TReturnConstant returnConstant)
        {
            array.AddAll(position, elements);
            return returnConstant;
        }

        public: template <typename T> static void AddAll(T array[], ref std::int64_t position, IList<T> &elements)
        {
            for (auto i = 0; i < elements.Count(); i++)
            {
                array.Add(position, elements[i]);
            }
        }

        public: static TReturnConstant AddSkipFirstAndReturnConstant<TElement, TReturnConstant>(TElement array[], ref std::int64_t position, IList<TElement> &elements, TReturnConstant returnConstant)
        {
            array.AddSkipFirst(position, elements);
            return returnConstant;
        }
        
        public: template <typename T> static void AddSkipFirst(T array[], ref std::int64_t position, IList<T> &elements) { array.AddSkipFirst(position, elements, 1); }
        
        public: template <typename T> static void AddSkipFirst(T array[], ref std::int64_t position, IList<T> &elements, std::int32_t skip)
        {
            for (auto i = skip; i < elements.Count(); i++)
            {
                array.Add(position, elements[i]);
            }
        }
    };
}
