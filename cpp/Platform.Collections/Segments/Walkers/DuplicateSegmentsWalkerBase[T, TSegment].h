namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DuplicateSegmentsWalkerBase;
    template <typename T, typename TSegment> class DuplicateSegmentsWalkerBase<T, TSegment> : public AllSegmentsWalkerBase<T, TSegment>
        where TSegment : Segment<T>
    {
        protected: DuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : base(minimumStringSegmentLength) { }

        protected: DuplicateSegmentsWalkerBase() : base(DefaultMinimumStringSegmentLength) { }

        protected: void Iteration(TSegment segment) override
        {
            auto frequency = this->GetSegmentFrequency(segment);
            if (frequency == 1)
            {
                this->OnDublicateFound(segment);
            }
            this->SetSegmentFrequency(segment, frequency + 1);
        }

        protected: virtual void OnDublicateFound(TSegment segment) = 0;

        protected: virtual std::int64_t GetSegmentFrequency(TSegment segment) = 0;

        protected: virtual void SetSegmentFrequency(TSegment segment, std::int64_t frequency) = 0;
    }
}
