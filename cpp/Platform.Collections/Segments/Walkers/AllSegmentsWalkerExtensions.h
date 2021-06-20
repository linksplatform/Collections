namespace Platform::Collections::Segments::Walkers
{
    template <Interfaces::IArray TArray, typename TSegment = std::span<char>>
    requires
        std::derived_from<TSegment, std::span<char>> &&
        requires(TArray array, std::string string) { TArray(string.begin(), string.end()); }
    static void WalkAll(AllSegmentsWalkerBase<char, TArray, TSegment> walker, std::string string)
    {
        walker.WalkAll(TArray(string.begin(), string.end()));
    }
}
