namespace Platform::Collections::Segments::Walkers
{
    template<
        typename Self,
        typename T,
        std::derived_from<std::span<T>> TSegment = std::span<T>>

    class AllSegmentsWalkerBase : public Polymorph<Self>
    {
        using base = Polymorph<Self>;

        public: static constexpr std::size_t DefaultMinimumStringSegmentLength = 2;

        private: std::size_t _minimumStringSegmentLength = 0;

        public: explicit AllSegmentsWalkerBase(std::size_t minimumStringSegmentLength) : _minimumStringSegmentLength(minimumStringSegmentLength) { }

        public: explicit AllSegmentsWalkerBase() : _minimumStringSegmentLength(DefaultMinimumStringSegmentLength) { }

        public: void WalkAll(Interfaces::CList auto&& elements)
        {
            for (std::size_t offset = 0, maxOffset = std::ranges::size(elements) - _minimumStringSegmentLength; offset <= maxOffset; offset++)
            {
                for (std::size_t length = _minimumStringSegmentLength, maxLength = std::ranges::size(elements) - offset; length <= maxLength; length++)
                {
                    Iteration(CreateSegment(elements, offset, length));
                }
            }
        }

        public: TSegment CreateSegment(Interfaces::CList auto&& elements, std::size_t offset, std::size_t length) { return this->self().CreateSegment(elements, offset, length); }

        public: void Iteration(auto&& segment) { this->self().Iteration(segment); }

        public: auto CreateSegment(Interfaces::CList auto&& elements, std::size_t offset, std::size_t length)
            requires std::same_as<TSegment, std::span<T>>
        {
            return std::span<T>(std::ranges::begin(elements) + offset, length);
        }
    };
}
