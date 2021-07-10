namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class DuplicateSegmentsWalkerBase;
    template <typename T, Interfaces::IArray TArray>
    class DuplicateSegmentsWalkerBase<T, TArray> : public DuplicateSegmentsWalkerBase<T, TArray, std::span<T>>
    {
        using base = DuplicateSegmentsWalkerBase<T, TArray, std::span<T>>;

        protected: DuplicateSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : base(minimumStringSegmentLength) { }

        protected: DuplicateSegmentsWalkerBase() : base() { }
    };
}
