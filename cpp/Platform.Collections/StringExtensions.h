namespace Platform::Collections
{
    namespace StringExtensions
    {
        template<typename _Type>
        concept char_string = requires(_Type object, int index)
        {
            requires std::is_fundamental_v<std::remove_pointer_t<std::decay_t<_Type>>>;
            requires std::same_as<_Type, const std::remove_pointer_t<std::decay_t<_Type>>*>;
            std::basic_string<std::decay_t<decltype(object[index])>>(object);
        };

        template<typename _Type>
        concept basic_string = requires(_Type object, int index)
        {
            requires !(char_string<_Type>);
            requires std::same_as<_Type, std::basic_string<std::decay_t<decltype(object[index])>>>;
        };

        #define REDEFINITION_FOR_CSTRING(FName) \
        template<typename... TArgs>  \
        static auto FName(char_string auto string, TArgs... args) \
        { \
            using TString = std::basic_string<std::remove_pointer_t<std::decay_t<decltype(string[0])>>>; \
            return FName(TString(string), args...); \
        }

        static auto CapitalizeFirstLetter(basic_string auto string)
        {
            // TODO бонусная альтернативная реализация от Voider'а
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
        static auto TrimSingle(const TString& string, char charToTrim)
        {
            using TChar = std::decay_t<decltype(string[0])>;

            if (!string.empty())
            {
                if (string.size() == 1)
                {
                    if (string[0] == charToTrim)
                    {
                        return (TString)(const TChar*)"";
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

        REDEFINITION_FOR_CSTRING(CapitalizeFirstLetter)
        REDEFINITION_FOR_CSTRING(Truncate)
        REDEFINITION_FOR_CSTRING(TrimSingle)

        #undef REDEFINITION_FOR_CSTRING
    };// namespace StringExtensions
}// namespace Platform::Collections