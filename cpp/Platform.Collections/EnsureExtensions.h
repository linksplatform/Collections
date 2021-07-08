namespace Platform::Collections::Always
{
    template<typename T>
    void ArgumentNotEmpty(Interfaces::IEnumerable auto&& argument, const std::string& argumentName = {}, const std::string& message = {})
    {
        if (std::ranges::empty(argument))
        {
            throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
        }
    }

    void ArgumentNotEmptyAndNotWhiteSpace(const std::string& argument, const std::string& argumentName, const std::string& message)
    {
        if (argument.empty())
        {
            throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
        }

        std::size_t count = 0;
        std::ranges::for_each(argument, [&count](auto&& item) { count += std::isspace(item); });
        auto is_whitespace = argument.size() == count;

        if (is_whitespace)
        {
            throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
        }
    }
}// namespace Platform::Collections::Always

namespace Platform::Collections::Always
{
    static void ArgumentNotEmpty(auto&&... args)
    {
    #ifndef NDEBUG
        Always::ArgumentNotEmpty(std::forward<decltype(args)>(args)...);
    #endif
    }

    static void ArgumentNotEmptyAndNotWhiteSpace(auto&&... args)
    {
    #ifndef NDEBUG
        Always::ArgumentNotEmptyAndNotWhiteSpace(std::forward<decltype(args)>(args)...);
    #endif
    }
}// namespace Platform::Collections::Always
