namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class AllSegmentsWalkerBase;
    template <typename T, Interfaces::IArray<T> TArray>
    class AllSegmentsWalkerBase<T, TArray> : public AllSegmentsWalkerBase<T, TArray, std::span<T>>
    {
        using base = AllSegmentsWalkerBase<T, TArray, std::span<T>>;
        using TSegment = std::span<T>;

        protected: explicit AllSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : base(minimumStringSegmentLength) {}

        protected: AllSegmentsWalkerBase() : base() { }

        protected: TSegment CreateSegment(TArray& elements, std::int32_t offset, std::int32_t length)
        {
            return std::span(std::ranges::begin(elements) + offset, length);
        }
    };
}
