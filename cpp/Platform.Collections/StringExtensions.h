namespace Platform::Collections
{
    namespace StringExtensions
    {
        template<typename TChar>
        concept __is_char =
            std::same_as<TChar, char> ||
            std::same_as<TChar, wchar_t> ||
            std::same_as<TChar, char8_t> ||
            std::same_as<TChar, char16_t> ||
            std::same_as<TChar, char32_t>;

        template<typename _Type>
        concept basic_string = requires()
        {
            requires System::Array<_Type>;
            requires __is_char<typename System::Common::Array<_Type>::TItem>;
            requires std::same_as<_Type, std::basic_string<typename System::Common::Array<_Type>::TItem>>;
        };

        template<typename _Type>
        concept char_string = requires(_Type object, int index)
        {
            requires System::Array<_Type>;
            requires __is_char<typename System::Common::Array<_Type>::TItem>;
            requires(!basic_string<_Type>);
        };

        template<typename _Type>
        concept native_string = char_string<_Type> || basic_string<_Type>;

        template<native_string TString, typename TChar = typename System::Common::Array<TString>::TItem>
        static auto CapitalizeFirstLetter(const TString& native_string)
        {
            std::basic_string<TChar> string;
            string = std::move(native_string);

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

        template<native_string TString, typename TChar = typename System::Common::Array<TString>::TItem>
        static auto Truncate(const TString& native_string, std::int32_t maxLength)
        {
            std::basic_string<TChar> string;
            string = std::move(native_string);

            return string.empty() ? TString{} : string.substr(0, std::min(string.size(), (size_t) maxLength));
        }

        template<native_string TString, typename TChar = typename System::Common::Array<TString>::TItem>
        static auto TrimSingle(const TString& native_string, TChar charToTrim)
        {
            std::basic_string<TChar> string;
            string = std::move(native_string);

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
    }// namespace StringExtensions
}// namespace Platform::Collections