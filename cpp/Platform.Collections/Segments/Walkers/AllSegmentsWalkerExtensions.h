namespace Platform::Collections::Segments::Walkers
{
    class AllSegmentsWalkerExtensions
    {
        public: static void WalkAll(AllSegmentsWalkerBase<char> walker, std::string std::string) { walker.WalkAll(std::string.ToCharArray()); }

        public: template <typename TSegment> static void WalkAll(AllSegmentsWalkerBase<char, TSegment> walker, std::string std::string) where TSegment : Segment<char> => walker.WalkAll(std::string.ToCharArray());
    };
}
