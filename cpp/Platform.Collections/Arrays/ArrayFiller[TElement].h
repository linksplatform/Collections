﻿namespace Platform::Collections::Arrays
{
    template<typename...> class ArrayFiller;
    template<Interfaces::CArray TArray> class ArrayFiller<TArray>
    {
        protected: TArray& _array;
        protected: std::int64_t _position = 0;

        public: ArrayFiller(TArray& array, std::int64_t offset) : _array(array), _position(offset) { }

        public: explicit ArrayFiller(TArray& array) : ArrayFiller(array, 0) { }

        public: void Add(auto&& element) { _array[_position++] = std::forward<decltype(element)>(element); }

        public: bool AddAndReturnTrue(auto&& element) { return Arrays::AddAndReturnConstant(_array, _position, element, true); }

        public: bool AddFirstAndReturnTrue(Interfaces::CArray auto&& elements) { return Arrays::AddFirstAndReturnConstant(_array, _position, elements, true); }

        public: bool AddAllAndReturnTrue(Interfaces::CArray auto&& elements) { return Arrays::AddAllAndReturnConstant(_array, _position, elements, true); }

        public: bool AddSkipFirstAndReturnTrue(Interfaces::CArray auto&& elements) { return Arrays::AddSkipFirstAndReturnConstant(_array, _position, elements, true); }
    };
}
