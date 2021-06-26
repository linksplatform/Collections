namespace Platform::Collections
{
    namespace EnsureExtensions
    {
        namespace Always
        {
            template<typename T>
            static void ArgumentNotEmpty(Interfaces::IEnumerable auto&& argument, const std::string& argumentName = {}, const std::string& message = {})
            {
                if (std::ranges::empty(argument))
                {
                    throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
                }
            }

            static void ArgumentNotEmptyAndNotWhiteSpace(const std::string& argument, const std::string& argumentName, const std::string& message)
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
        }

        namespace OnDebug
        {
            static void ArgumentNotEmpty(Interfaces::IEnumerable auto&& argument, const std::string& argumentName = {}, const std::string& message = {})
            {
            #ifndef NDEBUG
                Always::ArgumentNotEmpty(std::forward<decltype(argument)>(argument),
                                         std::forward<decltype(argumentName)>(argumentName),
                                         std::forward<decltype(message)>(message));
            #endif
            }

            static void ArgumentNotEmptyAndNotWhiteSpace(const std::string& argument, const std::string& argumentName = {}, const std::string& message = {})
            {
            #ifndef NDEBUG
                Always::ArgumentNotEmptyAndNotWhiteSpace(std::forward<decltype(argument)>(argument),
                                                         std::forward<decltype(argumentName)>(argumentName),
                                                         std::forward<decltype(message)>(message));
            #endif
            }
        }
    };// namespace EnsureExtensions
}// namespace Platform::Collections
