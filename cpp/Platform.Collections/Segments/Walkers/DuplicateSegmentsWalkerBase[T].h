namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DuplicateSegmentsWalkerBase;
    template <typename T> class DuplicateSegmentsWalkerBase<T> : public DuplicateSegmentsWalkerBase<T, Segment<T>>
    {
    }
}
