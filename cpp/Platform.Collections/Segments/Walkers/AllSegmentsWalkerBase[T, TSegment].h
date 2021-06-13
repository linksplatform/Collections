namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class AllSegmentsWalkerBase;
    template <typename T, System::IArray TArray, typename TSegment>
    requires std::derived_from<TSegment, std::span<T>>
    class AllSegmentsWalkerBase<T, TArray, TSegment> : public AllSegmentsWalkerBase<>
    {
        private: std::int32_t _minimumStringSegmentLength = 0;

        protected: AllSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) { _minimumStringSegmentLength = minimumStringSegmentLength; }

        protected: AllSegmentsWalkerBase() : AllSegmentsWalkerBase(DefaultMinimumStringSegmentLength) { }

        public: virtual void WalkAll(TArray elements)
        {
            for (std::int32_t offset = 0, maxOffset = elements.size() - _minimumStringSegmentLength; offset <= maxOffset; offset++)
            {
                for (std::int32_t length = _minimumStringSegmentLength, maxLength = elements.size() - offset; length <= maxLength; length++)
                {
                    Iteration(CreateSegment(elements, offset, length));
                }
            }
        }

        protected: virtual TSegment CreateSegment(TArray elements, std::int32_t offset, std::int32_t length) = 0;

        protected: virtual void Iteration(TSegment segment) = 0;

        public: virtual ~AllSegmentsWalkerBase() {}
    };
}
