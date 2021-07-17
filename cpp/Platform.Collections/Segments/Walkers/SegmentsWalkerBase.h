namespace Platform::Collections::Segments::Walkers
{
    template<typename Self>
    class SegmentsWalkerBase
    {
    protected:
        constexpr auto&& self() & { return static_cast<Self&>(*this); }
        constexpr auto&& self() && { return static_cast<Self&&>(*this); }
        constexpr auto&& self() const & { return static_cast<const Self&>(*this); }
        constexpr auto&& self() const && { return static_cast<const Self&&>(*this); }

    public:
        static constexpr std::int32_t DefaultMinimumStringSegmentLength = 2;

        SegmentsWalkerBase() = default;
    };
}
