namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class AllSegmentsWalkerBase;
    template <typename T, Platform::Collections::System::Array TArray>
    class AllSegmentsWalkerBase<T, TArray> : public AllSegmentsWalkerBase<T, TArray, std::span<T>>
    {
        using base = AllSegmentsWalkerBase<T, TArray, std::span<T>>;

        protected: AllSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : base(minimumStringSegmentLength) {}

        protected: AllSegmentsWalkerBase() : base() { }

        protected: auto CreateSegment(TArray& elements, std::int32_t offset, std::int32_t length) override
        {
            return std::span<T>(elements.begin() + offset, length);
        }
    };
}
