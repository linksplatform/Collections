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
         and
        Interfaces::CDictionary<Dictionary, TSegment, std::size_t>
    class DictionaryBasedDuplicateSegmentsWalkerBase : public DuplicateSegmentsWalkerBase<Self, T, TSegment>
    {
        using base = DuplicateSegmentsWalkerBase<Self, T, TSegment>;

        public: static constexpr bool DefaultResetDictionaryOnEachWalk = false;

        private: bool _resetDictionaryOnEachWalk = false;
        public: Dictionary dictionary{};

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(Dictionary dictionary, std::size_t minimumStringSegmentLength = base::DefaultMinimumStringSegmentLength, bool resetDictionaryOnEachWalk = DefaultResetDictionaryOnEachWalk) : base(minimumStringSegmentLength), dictionary(std::move(dictionary)), _resetDictionaryOnEachWalk(resetDictionaryOnEachWalk) { }

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(std::size_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk = DefaultResetDictionaryOnEachWalk) : DictionaryBasedDuplicateSegmentsWalkerBase(Dictionary{}, minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase() : DictionaryBasedDuplicateSegmentsWalkerBase(base::DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        public: void WalkAll(Interfaces::CArray<T> auto&& elements)
        {
            // Not use capacity-style if want use std::map
            if (_resetDictionaryOnEachWalk)
            {
                if constexpr (requires { Dictionary(std::size_t{}); })
                {
                    std::unordered_map<int, int>(1);
                    auto capacity = std::ceil(std::pow(std::ranges::size(elements), 2) / 2);
                    dictionary = Dictionary(capacity);
                }
                else
                {
                   dictionary.clear();
                }
            }

            base::WalkAll(elements);
        }

        public: std::size_t GetSegmentFrequency(auto&& segment) { return dictionary[segment]; }

        public: void SetSegmentFrequency(auto&& segment, std::int64_t frequency) { dictionary[segment] = frequency; }
    };
}