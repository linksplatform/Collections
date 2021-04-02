namespace Platform::Collections
{
    class StringExtensions
    {
        // TODO заметил, что часто встречается проверка на не null в функциях
        //  Ох уж эти шарперы. Всё-таки код чисто с value-types(не я придумал) и ссылками выглядит безопаснее
        public: static std::string CapitalizeFirstLetter(std::string string)
        {
            // TODO бонусная альтернативная реализация от Voider'а
            for(auto& it : string) {
                if(std::islower(it)) {
                    it = std::toupper(it);
                }
                return string;
            }

            return string;
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