namespace Platform::Collections::Segments::Walkers
{
    class AllSegmentsWalkerExtensions
    {
        public: template <Platform::Collections::System::Array<char> TArray, typename TSegment = std::span<char>> requires std::derived_from<TSegment, std::span<char>>
        static void WalkAll(AllSegmentsWalkerBase<char, TArray, TSegment>& walker, std::string string)
        {
            walker.WalkAll(TArray(string.begin(), string.end()));
        }
    };
}
