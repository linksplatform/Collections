namespace Platform::Collections::Segments::Walkers
{
    template <Platform::Collections::System::Array TArray, typename TSegment>
    requires
        std::derived_from<TSegment, std::span<char>> &&
        requires(TArray array, std::string string) {TArray(string.begin(), string.end());}
    static void WalkAll(AllSegmentsWalkerBase<char, TArray, TSegment>& walker, std::string string)
    {
        walker.WalkAll(TArray(string.begin(), string.end()));
    }
}
