namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class AllSegmentsWalkerBase;
    template <typename T> class AllSegmentsWalkerBase<T> : public AllSegmentsWalkerBase<T, Segment<T>>
    {
        protected: override Segment<T> CreateSegment(IList<T> &elements, std::int32_t offset, std::int32_t length) { return Segment<T>(elements, offset, length); }
    };
}
