namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class AllSegmentsWalkerBase;
    template<> class AllSegmentsWalkerBase<>
    {
        public: static constexpr std::int32_t DefaultMinimumStringSegmentLength = 2;
    };
}
