namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DictionaryBasedDuplicateSegmentsWalkerBase;
    template <typename T, Interfaces::IArray TArray, typename TDictionary>
    requires Interfaces::IDictionary<TDictionary, std::span<int>*, int>
    class DictionaryBasedDuplicateSegmentsWalkerBase<T, TArray, TDictionary> : public DuplicateSegmentsWalkerBase<T, TArray, std::span<T>>
    {
        using base = DuplicateSegmentsWalkerBase<T, TArray, std::span<T>>;

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary dictionary, std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : base(dictionary, minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary dictionary, std::int32_t minimumStringSegmentLength) : base(dictionary, minimumStringSegmentLength, base::DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(TDictionary dictionary) : base(dictionary, base::DefaultMinimumStringSegmentLength, base::DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : base(minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : base(minimumStringSegmentLength, base::DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase() : base(base::DefaultMinimumStringSegmentLength, base::DefaultResetDictionaryOnEachWalk) { }
    };
}
