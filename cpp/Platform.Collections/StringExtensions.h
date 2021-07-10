namespace Platform::Collections
{
    template<typename TChar>
    static auto CapitalizeFirstLetter(std::basic_string<TChar> string)
    {
        for (auto& it : string)
        {
            if (std::isalpha(it))
            {
                it = std::toupper(it);
                return string;
            }
        }
        return string;
    }

    template<typename TChar>
    static auto Truncate(std::basic_string<TChar> string, std::size_t maxLength)
    {
        return (string.empty()) ? std::basic_string<TChar>{} : string.substr(0, std::min(string.size(), maxLength));
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
