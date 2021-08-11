namespace Platform::Collections::Segments::Walkers
{
    template<
        typename Self,
        typename T,
        std::derived_from<std::span<T>> TSegment = std::span<T>>

    class DuplicateSegmentsWalkerBase : public AllSegmentsWalkerBase<Self, T, TSegment>
    {
        using base = AllSegmentsWalkerBase<Self, T, TSegment>;

        protected: explicit DuplicateSegmentsWalkerBase(std::size_t minimumStringSegmentLength) : base(minimumStringSegmentLength) { }

        protected: DuplicateSegmentsWalkerBase() : base() { }

        public: void Iteration(const TSegment& segment)
        {
            auto frequency = GetSegmentFrequency(segment);
            if (frequency == 1)
            {
                this->OnDuplicateFound(segment);
            }
            this->SetSegmentFrequency(segment, frequency + 1);
        }

        public: void OnDuplicateFound(const TSegment& segment) { base::self().OnDuplicateFound(segment); }

        public: std::size_t GetSegmentFrequency(const TSegment& segment) { return base::self().GetSegmentFrequency(segment); }

        public: void SetSegmentFrequency(const TSegment& segment, std::int64_t frequency) { base::self().SetSegmentFrequency(segment, frequency); }
    };
}
