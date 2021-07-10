namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DuplicateSegmentsWalkerBase;
    template <typename T, Interfaces::IArray TArray, typename TSegment>
    requires std::derived_from<TSegment, std::span<T>>
    class DuplicateSegmentsWalkerBase<T, TArray, TSegment> : public AllSegmentsWalkerBase<T, TArray, TSegment>
    {
        using base = AllSegmentsWalkerBase<T, TSegment, TArray>;

        protected: DuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : base(minimumStringSegmentLength) { }

        protected: DuplicateSegmentsWalkerBase() : base() { }

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
    };
}
