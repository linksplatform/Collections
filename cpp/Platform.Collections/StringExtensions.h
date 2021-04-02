namespace Platform::Collections
{
    class StringExtensions
    {
        // TODO заметил, что часто встречается проверка на не null в функциях
        //  Ох уж эти шарперы. Всё-таки код чисто с value-types(не я придумал) и ссылками выглядит безопаснее
        public: static std::string CapitalizeFirstLetter(std::string string)
        {
            // TODO бонусная альтернативная реализация от Voider'а
            for(auto & it : string) {
                if(std::islower(it)) {
                    it = std::toupper(it);
                    return string;
                }
            }
            return string;
        }

        public: static std::string Truncate(std::string string, std::int32_t maxLength)
        {
            return string.empty() ? std::string{} : string.substr(0, std::min(string.size(), (size_t)maxLength));
        }

        public: static std::string TrimSingle(std::string string, char charToTrim)
        {
            if (string.size() >= 1)
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
            else
            {
                return string;
            }
        }
    };
}