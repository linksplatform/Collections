// Далее будет использоваться std::span<char>
// Next, std::span<char> will be used

namespace std
{
    template<typename TChar>
    requires
        std::same_as<TChar, char> ||
        std::same_as<TChar, wchar_t> ||
        std::same_as<TChar, char8_t> ||
        std::same_as<TChar, char16_t> ||
        std::same_as<TChar, char32_t>
    std::string to_string(std::span<TChar> segment)
    {
        return std::string(segment.begin(), segment.end());
    }
}