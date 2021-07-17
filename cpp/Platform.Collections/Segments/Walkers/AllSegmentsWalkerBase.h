namespace Platform::Collections::Segments::Walkers
{
    template<
        typename Self,
        typename T,
        std::derived_from<std::span<T>> TSegment = std::span<T>>

    class AllSegmentsWalkerBase : public SegmentsWalkerBase<Self>
    {
        using base = SegmentsWalkerBase<Self>;

        private: std::size_t _minimumStringSegmentLength = 0;

        public: explicit AllSegmentsWalkerBase(std::int32_t minimumStringSegmentLength) : _minimumStringSegmentLength(minimumStringSegmentLength) { }

        public: AllSegmentsWalkerBase() : AllSegmentsWalkerBase(base::DefaultMinimumStringSegmentLength) { }

        public: void WalkAll(Interfaces::IList auto&& elements)
        {
            for (std::size_t offset = 0, maxOffset = std::ranges::size(elements) - _minimumStringSegmentLength; offset <= maxOffset; offset++)
            {
                for (std::size_t length = _minimumStringSegmentLength, maxLength = std::ranges::size(elements) - offset; length <= maxLength; length++)
                {
                    Iteration(CreateSegment(elements, offset, length));
                }
            }
        }

        public: TSegment CreateSegment(Interfaces::IList auto&& elements, std::size_t offset, std::size_t length) { return base::self().CreateSegment(elements, offset, length); }

        public: void Iteration(const TSegment& segment) { base::self().Iteration(segment); }

        public: TSegment CreateSegment(Interfaces::IList auto&& elements, std::size_t offset, std::size_t length)
            requires std::same_as<TSegment, std::span<T>>
        {
            return std::span<T>(std::ranges::begin(elements) + offset, length);
        }
    };
}
