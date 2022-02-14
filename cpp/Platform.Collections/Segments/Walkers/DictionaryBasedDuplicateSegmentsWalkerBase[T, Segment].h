namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DictionaryBasedDuplicateSegmentsWalkerBase;
    template <typename T, typename TSegment> class DictionaryBasedDuplicateSegmentsWalkerBase<T, TSegment> : public DuplicateSegmentsWalkerBase<T, TSegment>
        where TSegment : Segment<T>
    {
        public: static bool DefaultResetDictionaryOnEachWalk;
        private: bool _resetDictionaryOnEachWalk = 0;
        protected: IDictionary<TSegment, std::int64_t> *Dictionary;

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, std::int64_t> &dictionary, std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : base(minimumStringSegmentLength)
        {
            Dictionary = dictionary;
            _resetDictionaryOnEachWalk = resetDictionaryOnEachWalk;
        }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, std::int64_t> &dictionary, std::int32_t minimumStringSegmentLength) : this(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, std::int64_t> &dictionary) : this(dictionary, DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : this(resetDictionaryOnEachWalk ? {} : Dictionary<TSegment, std::int64_t>(), minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : this(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        protected: DictionaryBasedDuplicateSegmentsWalkerBase() : this(DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        public: void WalkAll(IList<T> &elements) override
        {
            if (_resetDictionaryOnEachWalk)
            {
                auto capacity = Math.Ceiling(Math.Pow(elements.Count(), 2) / 2);
                Dictionary = Dictionary<TSegment, std::int64_t>((std::int32_t)capacity);
            }
            base.WalkAll(elements);
        }

        protected: std::int64_t GetSegmentFrequency(TSegment segment) override { return Dictionary.GetOrDefault(segment); }

        protected: void SetSegmentFrequency(TSegment segment, std::int64_t frequency) override { Dictionary[segment] = frequency; }
    }
}