namespace Platform::Collections
{
    // Maybe internal
    template<typename TChar>
    static auto IsWhiteSpace(const std::basic_string<TChar>& string)
    {
        using ctype = std::ctype<wchar_t>;
        auto& facet = std::use_facet<ctype>(std::locale());

        auto subrange = string | std::views::drop_while([&facet](auto c)
        {
            return facet.is(ctype::space, c);
        });

        return std::ranges::size(subrange) == 0;
    }

    template<typename TChar>
    static auto CapitalizeFirstLetter(std::basic_string<TChar> string)
    {
        using ctype = std::ctype<wchar_t>;
        auto& facet = std::use_facet<ctype>(std::locale());

        for (auto& symbol : string)
        {
            if (facet.is(ctype::alpha, symbol))
            {
                symbol = facet.toupper(symbol);
                return string;
            }
        }
        return string;
    }

    template<typename TChar>
    static auto Truncate(std::basic_string<TChar> string, std::size_t maxLength)
    {
        return (string.empty()) ? std::basic_string<TChar>{} : string.substr(0, maxLength);
    }

    template<typename TChar>
    static auto TrimSingle(const std::basic_string<TChar>& string, TChar charToTrim)
    {
        if (string.empty())
        {
            return string;
        }

        if (string.size() == 1)
        {
            if (string[0] == charToTrim)
            {
                return std::basic_string<TChar>{};
            }
            else
            {
                return string;
            }
        }

        auto left = 0;
        auto right = string.size() - 1;
        if (string[left] == charToTrim)
        {
            left++;
        }
        if (string[right] == charToTrim)
        {
            right--;
        }
        return string.substr(left, right - left + 1);
    }
}// namespace Platform::Collections
