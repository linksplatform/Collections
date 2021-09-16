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

        public: void Iteration(auto&& segment)
        {
            auto frequency = GetSegmentFrequency(segment);
            if (frequency == 1)
            {
                this->OnDuplicateFound(segment);
            }
            this->SetSegmentFrequency(segment, frequency + 1);
        }

        public: void OnDuplicateFound(auto&& segment) { this->self().OnDuplicateFound(segment); }

        public: std::size_t GetSegmentFrequency(auto&& segment) { return this->self().GetSegmentFrequency(segment); }

        public: void SetSegmentFrequency(auto&& segment, std::int64_t frequency) { this->self().SetSegmentFrequency(segment, frequency); }
    };
}
