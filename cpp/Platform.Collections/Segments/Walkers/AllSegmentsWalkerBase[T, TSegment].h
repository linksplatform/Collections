namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class AllSegmentsWalkerBase;
    template <typename T, typename TSegment> class AllSegmentsWalkerBase<T, TSegment> : public AllSegmentsWalkerBase<>
        where TSegment : Segment<T>
    {
        private: std::int32_t _minimumStringSegmentLength = 0;

        protected: AllSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) { _minimumStringSegmentLength = minimumStringSegmentLength; }

        protected: AllSegmentsWalkerBase() : this(DefaultMinimumStringSegmentLength) { }

        public: virtual void WalkAll(IList<T> &elements)
        {
            for (std::int32_t offset = 0, maxOffset = elements.Count() - _minimumStringSegmentLength; offset <= maxOffset; offset++)
            {
                for (std::int32_t length = _minimumStringSegmentLength, maxLength = elements.Count() - offset; length <= maxLength; length++)
                {
                    this->Iteration(this->CreateSegment(elements, offset, length));
                }
            }
        }

        protected: virtual TSegment CreateSegment(IList<T> &elements, std::int32_t offset, std::int32_t length) = 0;

        protected: virtual void Iteration(TSegment segment) = 0;
    }
}
