namespace Platform::Collections
{
    // basic string is a collection with random_access_iterator (is Interfaces::IArray)
    template<typename Self>
    concept basic_string = requires
    {
        requires std::same_as<Self, std::basic_string<typename Interfaces::Array<Self>::Item>>;
    };

    template<basic_string TString, typename TChar = typename Interfaces::Array<TString>::Item>
    static auto CapitalizeFirstLetter(TString string)
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

    template<basic_string TString, typename TChar = typename Interfaces::Array<TString>::Item>
    static auto Truncate(const TString& string, std::int32_t maxLength)
    {
        return string.empty() ? TString{} : string.substr(0, std::min(string.size(), (size_t) maxLength));
    }

    template<basic_string TString, typename TChar = typename Interfaces::Array<TString>::Item>
    static auto TrimSingle(const TString& string, TChar charToTrim)
    {
        if (!string.empty())
        {
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
            else
            {
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
        }
        else
        {
            return string;
        }
    }
}// namespace Platform::Collections