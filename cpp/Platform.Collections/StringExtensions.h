namespace Platform::Collections
{
    class StringExtensions
    {
        public: static std::string CapitalizeFirstLetter(std::string std::string)
        {
            if (std::string.IsNullOrWhiteSpace(std::string))
            {
                return std::string;
            }
            auto chars = std::string.ToCharArray();
            for (auto i = 0; i < chars.Length; i++)
            {
                auto category = char.GetUnicodeCategory(chars[i]);
                if (category == UnicodeCategory.UppercaseLetter)
                {
                    return std::string;
                }
                if (category == UnicodeCategory.LowercaseLetter)
                {
                    chars[i] = char.ToUpper(chars[i]);
                    return std::string(chars);
                }
            }
            return std::string;
        }

        public: static std::string Truncate(std::string std::string, std::int32_t maxLength) { return std::string.IsNullOrEmpty(std::string) ? std::string : std::string.Substring(0, Math.Min(std::string.Length, maxLength)); }

        public: static std::string TrimSingle(std::string std::string, char charToTrim)
        {
            if (!std::string.IsNullOrEmpty(std::string))
            {
                if (std::string.Length == 1)
                {
                    if (std::string[0] == charToTrim)
                    {
                        return "";
                    }
                    else
                    {
                        return std::string;
                    }
                }
                else
                {
                    auto left = 0;
                    auto right = std::string.Length - 1;
                    if (std::string[left] == charToTrim)
                    {
                        left++;
                    }
                    if (std::string[right] == charToTrim)
                    {
                        right--;
                    }
                    return std::string.Substring(left, right - left + 1);
                }
            }
            else
            {
                return std::string;
            }
        }
    };
}