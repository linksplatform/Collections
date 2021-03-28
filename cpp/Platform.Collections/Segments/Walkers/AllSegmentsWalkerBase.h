namespace Platform::Collections::Segments::Walkers
{
    template <typename ...> class AllSegmentsWalkerBase;
    template<> class AllSegmentsWalkerBase<>
    {
        public: inline static const std::int32_t DefaultMinimumStringSegmentLength = 2;
    };
}
