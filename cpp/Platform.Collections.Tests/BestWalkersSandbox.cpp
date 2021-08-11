#include <Platform.Collections.h>

using namespace Platform;
using namespace Platform::Interfaces;
// TODO: namespace Segments is useless
using namespace Platform::Collections::Segments::Walkers;

template<typename char_type>
auto span_as_string(const std::span<char_type>& span)
{
    std::basic_string<char_type> result;
    std::ranges::for_each(span, [&](auto item) { result += item; });
    return result;
}

struct walker_without_override_CreateSegment : public DictionaryBasedDuplicateSegmentsWalkerBase<walker_without_override_CreateSegment, char>
{
    using base = DictionaryBasedDuplicateSegmentsWalkerBase<walker_without_override_CreateSegment, char>;
    std::size_t _totalDuplicates{};

    walker_without_override_CreateSegment()
    : base(DefaultMinimumStringSegmentLength, true) {}

    void WalkAll(Interfaces::IArray<char> auto&& elements)
    {
        _totalDuplicates = 0;
        base::WalkAll(elements);
        std::printf("Unique string segments: %lu. Total duplicates: %lu.\n", dictionary.size(), _totalDuplicates);
    }

    void OnDuplicateFound(auto&& segment)
    {
        _totalDuplicates++;
        std::cout << span_as_string(segment) << std::endl;
    }
};

struct walker_with_override_CreateSegment : public DictionaryBasedDuplicateSegmentsWalkerBase<walker_with_override_CreateSegment, char>
{
    using base = DictionaryBasedDuplicateSegmentsWalkerBase<walker_with_override_CreateSegment, char>;

    std::size_t _totalDuplicates{};

    walker_with_override_CreateSegment()
    : base(DefaultMinimumStringSegmentLength, true) {}

    void WalkAll(Interfaces::IArray<char> auto&& elements)
    {
        _totalDuplicates = 0;
        base::WalkAll(elements);
        std::printf("Unique string segments: %lu. Total duplicates: %lu.\n", dictionary.size(), _totalDuplicates);
    }

    auto CreateSegment(Interfaces::IList auto&& elements, std::size_t offset, std::size_t length)
    {
        auto segment = base::CreateSegment(elements, offset, length);
        std::cout << "create segment [" << segment << "]\n";
        return segment;
    }
};

TEST(BestWalkers, Sandbox)
{
    std::string text = "abcdefg";

    auto walker1 = walker_without_override_CreateSegment{};
    walker1.WalkAll(text);
    auto walker2 = walker_with_override_CreateSegment{};
    walker2.WalkAll(text);
}
