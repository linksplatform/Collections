namespace Platform::Collections
{
    namespace StringExtensions
    {
        // TODO заметил, что часто встречается проверка на не null в функциях
        //  Ох уж эти шарперы. Всё-таки код чисто с value-types(не я придумал) и ссылками выглядит безопаснее
        static std::u16string CapitalizeFirstLetter(std::u16string string)
        {
            // TODO бонусная альтернативная реализация от Voider'а
            for(auto & it : string)
            {
                if(std::islower(it))
                {
                    it = std::toupper(it);
                    return string;
                }
            }
            return string;
        }

        static std::u16string Truncate(std::u16string string, std::int32_t maxLength)
        {
            return string.empty() ? std::u16string{} : string.substr(0, std::min(string.size(), (size_t)maxLength));
        }

        static std::u16string TrimSingle(std::u16string string, char charToTrim)
        {
            if (!string.empty())
            {
                if (string.size() == 1)
                {
                    if (string[0] == charToTrim)
                    {
                        return u"";
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
    };
}