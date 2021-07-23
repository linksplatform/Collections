namespace Platform::Collections::Segments::Walkers
{

    template<
        typename Self,
        typename T,
        std::derived_from<std::span<T>> TSegment = std::span<T>,
        template<typename, typename, typename...> typename TDictionary = std::unordered_map,
        typename Dictionary = TDictionary<TSegment, std::size_t>>

    requires
        requires { std::hash<TSegment>{}; }
         and
        requires { std::equal_to<TSegment>{}; }
        // and
        //Interfaces::IDictionary<Dictionary, TSegment, std::size_t>

    class DictionaryBasedDuplicateSegmentsWalkerBase : public DuplicateSegmentsWalkerBase<Self, T, TSegment>
    {
        using base = DuplicateSegmentsWalkerBase<Self, T, TSegment>;
       // public: using Dictionary = TDictionary<TSegment, std::size_t>;

        public: static constexpr bool DefaultResetDictionaryOnEachWalk = false;

        private: bool _resetDictionaryOnEachWalk = false;
        protected: Dictionary dictionary{};

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(Dictionary dictionary, std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : base(minimumStringSegmentLength), dictionary(std::move(dictionary)), _resetDictionaryOnEachWalk(resetDictionaryOnEachWalk) { }

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(Dictionary dictionary, std::int32_t minimumStringSegmentLength) : DictionaryBasedDuplicateSegmentsWalkerBase(std::move(dictionary), minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(Dictionary dictionary) : DictionaryBasedDuplicateSegmentsWalkerBase(std::move(dictionary), base::DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : DictionaryBasedDuplicateSegmentsWalkerBase(Dictionary{}, minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : DictionaryBasedDuplicateSegmentsWalkerBase(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase() : DictionaryBasedDuplicateSegmentsWalkerBase(base::DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        public: void WalkAll(Interfaces::IArray<T> auto&& elements)
        {
            // Not use capacity-style if want use std::map
            auto capacity = std::ceil(std::pow(std::ranges::size(elements), 2) / 2);
            dictionary = Dictionary(capacity);
            base::WalkAll(elements);
        }

        public: std::size_t GetSegmentFrequency(TSegment segment) { return dictionary[segment]; }

        public: void SetSegmentFrequency(TSegment segment, std::int64_t frequency) { dictionary[segment] = frequency; }
    };
}