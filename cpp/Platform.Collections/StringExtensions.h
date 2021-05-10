namespace Platform::Collections
{
    namespace StringExtensions
    {
        namespace
        {
            template<typename TChar>
            concept __is_char =
                std::same_as<TChar, char> ||
                std::same_as<TChar, wchar_t> ||
                std::same_as<TChar, char8_t> ||
                std::same_as<TChar, char16_t> ||
                std::same_as<TChar, char32_t>;
        }

        template<typename _Type>
        concept basic_string = requires()
        {
            requires System::Array<_Type>;
            requires __is_char<typename System::Common::Array<_Type>::TItem>;
            requires std::same_as<_Type, std::basic_string<typename System::Common::Array<_Type>::TItem>>;
        };

        template<basic_string TString>
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

        template<basic_string TString>
        static auto Truncate(const TString& string, std::int32_t maxLength)
        {
            return string.empty() ? TString{} : string.substr(0, std::min(string.size(), (size_t) maxLength));
        }

        template<basic_string TString>
        static auto TrimSingle(const TString& string, typename System::Common::Array<TString>::TItem charToTrim)
        {
            using TChar = typename System::Common::Array<TString>::TItem;

            if (!string.empty())
            {
                if (string.size() == 1)
                {
                    if (string[0] == charToTrim)
                    {
                        return (TString) (const TChar*) "";
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
    };// namespace StringExtensions
}// namespace Platform::Collections