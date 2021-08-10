namespace Platform::Collections::Ensure::Always
{
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

        if (IsWhiteSpace(argument))
        {
            throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
        }
    }
}

namespace Platform::Collections::Ensure::Always
{
#ifdef NDEBUG
    #define NDEBUG_CONSTEVAL consteval
#else
    #define NDEBUG_CONSTEVAL
#endif

    NDEBUG_CONSTEVAL
    static void ArgumentNotEmpty(auto&&... args)
    #ifdef NDEBUG
        noexcept {}
    #else
        { Always::ArgumentNotEmpty(std::forward<decltype(args)>(args)...); }
    #endif

    NDEBUG_CONSTEVAL
    static void ArgumentNotEmptyAndNotWhiteSpace(auto&&... args)
    #ifdef NDEBUG
        noexcept {}
    #else
        { Always::ArgumentNotEmptyAndNotWhiteSpace(std::forward<decltype(args)>(args)...); }
    #endif

#undef NDEBUG_CONSTEVAL
}
