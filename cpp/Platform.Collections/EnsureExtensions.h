namespace Platform::Collections
{
    namespace EnsureExtensions
    {
        template<typename T>
        static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, Platform::Collections::Interfaces::ICollection auto& argument, std::string argumentName, std::string message)
        {
            if (argument.IsNullOrEmpty())
            {
                throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
            }
        }

        template<typename T>
        static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, Platform::Collections::Interfaces::ICollection auto& argument, std::string argumentName)
        {
            ArgumentNotEmpty(root, argument, argumentName, {});
        }

        template<typename T>
        static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, Platform::Collections::Interfaces::ICollection auto& argument)
        {
            ArgumentNotEmpty(root, argument, {}, {});
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, std::string argument, std::string argumentName, std::string message)
        {
            if (StringExtensions::IsWhiteSpace(argument))
            {
                throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
            }
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, std::string argument, std::string argumentName)
        {
            ArgumentNotEmptyAndNotWhiteSpace(root, argument, argumentName, {});
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, std::string argument)
        {
            ArgumentNotEmptyAndNotWhiteSpace(root, argument, {}, {});
        }

        // DEBUG REGION

        template<typename T>
        static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, Platform::Collections::Interfaces::ICollection auto& argument, std::string argumentName, std::string message)
        {
            ArgumentNotEmpty(Platform::Exceptions::Ensure::Always, argument, argumentName, message);
        }

        template<typename T>
        static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, Platform::Collections::Interfaces::ICollection auto& argument, std::string argumentName)
        {
            ArgumentNotEmpty(Platform::Exceptions::Ensure::Always, argument, argumentName, {});
        }

        template<typename T>
        static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, Platform::Collections::Interfaces::ICollection auto& argument)
        {
            ArgumentNotEmpty(Platform::Exceptions::Ensure::Always, argument, {}, {});
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument, std::string argumentName, std::string message)
        {
            ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::Ensure::Always, argument, argumentName, message);
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument, std::string argumentName)
        {
            ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::Ensure::Always, argument, argumentName, {});
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument)
        {
            ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::Ensure::Always, argument, {}, {});
        }
    };// namespace EnsureExtensions
}// namespace Platform::Collections
