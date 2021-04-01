namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DictionaryBasedDuplicateSegmentsWalkerBase;
    template <typename T, Platform::Collections::System::Array<T> TArray, typename TDictionary, typename TSegment>
    requires std::derived_from<TSegment, std::span<T>> && Platform::Collections::System::IDictionary<TDictionary, TSegment*, int>
    class DictionaryBasedDuplicateSegmentsWalkerBase<T, TArray, TDictionary, TSegment> : public DuplicateSegmentsWalkerBase<T, TArray, TSegment>
    {
        using base = DuplicateSegmentsWalkerBase<T, TArray, TSegment>;

        public: static constexpr bool DefaultResetDictionaryOnEachWalk = false; // change readonly to constexpr

        private: bool _resetDictionaryOnEachWalk = 0;
        protected: TDictionary* Dictionary;

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary& dictionary, std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : base(minimumStringSegmentLength), Dictionary(dictionary)
        {
            _resetDictionaryOnEachWalk = resetDictionaryOnEachWalk;
        }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary& dictionary, std::int32_t minimumStringSegmentLength)
            : DictionaryBasedDuplicateSegmentsWalkerBase(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary& dictionary)
            : DictionaryBasedDuplicateSegmentsWalkerBase(dictionary, base::DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : DictionaryBasedDuplicateSegmentsWalkerBase(resetDictionaryOnEachWalk ? nullptr : new TDictionary, minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength)
            : DictionaryBasedDuplicateSegmentsWalkerBase(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase()
            : DictionaryBasedDuplicateSegmentsWalkerBase(base::DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        public: void WalkAll(TArray& elements) override
        {
            if constexpr(decltype(*this)::_resetDictionaryOnEachWalk && requires(TDictionary dict, int capacity) {dict(capacity);})
            {
                auto capacity = std::ceil(std::pow(elements->size(), 2) / 2);
                Dictionary = new TDictionary((std::int32_t)capacity);
            }
            base::WalkAll(elements);
        }

        // TODO см. IDictionaryExtensions.h
        protected: std::int64_t GetSegmentFrequency(TSegment segment) override { return (*Dictionary)[segment]; }

        protected: void SetSegmentFrequency(TSegment segment, std::int64_t frequency) override { (*Dictionary)[segment] = frequency; }
    };
}