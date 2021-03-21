namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DictionaryBasedDuplicateSegmentsWalkerBase;
    template <typename T> class DictionaryBasedDuplicateSegmentsWalkerBase<T> : public DictionaryBasedDuplicateSegmentsWalkerBase<T, Segment<T>>
    {
        protected: DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, std::int64_t> dictionary, std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : base(dictionary, minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, std::int64_t> dictionary, std::int32_t minimumStringSegmentLength) : base(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, std::int64_t> dictionary) : base(dictionary, DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : base(minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : base(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase() : base(DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }
    };
}
