namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DictionaryBasedDuplicateSegmentsWalkerBase;
    template <typename T, Interfaces::IArray TArray, Interfaces::IDictionary TDictionary, typename TSegment>
    requires std::derived_from<TSegment, std::span<T>> && Interfaces::IDictionary<TDictionary, int, TSegment>
    class DictionaryBasedDuplicateSegmentsWalkerBase<T, TArray, TDictionary, TSegment> : public DuplicateSegmentsWalkerBase<T, TArray, TSegment>
    {
        using base = DuplicateSegmentsWalkerBase<T, TArray, TSegment>;

        public: static constexpr bool DefaultResetDictionaryOnEachWalk = false;

        private: bool _resetDictionaryOnEachWalk = false;
        protected: TDictionary Dictionary;

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary dictionary, std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : base(minimumStringSegmentLength), Dictionary(dictionary), _resetDictionaryOnEachWalk(resetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary dictionary, std::int32_t minimumStringSegmentLength) : DictionaryBasedDuplicateSegmentsWalkerBase(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary dictionary) : DictionaryBasedDuplicateSegmentsWalkerBase(std::move(dictionary), base::DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary{}, minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: explicit DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : DictionaryBasedDuplicateSegmentsWalkerBase(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase() : DictionaryBasedDuplicateSegmentsWalkerBase(base::DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        public: void WalkAll(const TArray& elements) override
        {
            auto capacity = std::ceil(std::pow(std::ranges::size(elements), 2) / 2);
            Dictionary = TDictionary(capacity);
            base::WalkAll(elements);
        }

        protected: std::int64_t GetSegmentFrequency(TSegment segment) override { return Dictionary[segment]; }

        protected: void SetSegmentFrequency(TSegment segment, std::int64_t frequency) override { Dictionary[segment] = frequency; }
    };
}