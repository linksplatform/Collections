namespace Platform::Collections
{
    namespace EnsureExtensions
    {
        template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, Platform::Collections::System::ICollection auto& argument, std::string argumentName, std::string message)
        {
            if (argument.IsNullOrEmpty())
            {
                throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
            }
        }

        template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, Platform::Collections::System::ICollection auto& argument, std::string argumentName)
        {
            ArgumentNotEmpty(root, argument, argumentName, {});
        }

        template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, Platform::Collections::System::ICollection auto& argument)
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

        template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, Platform::Collections::System::ICollection auto& argument, std::string argumentName, std::string message)
        {
            #ifdef DEBUG
                ArgumentNotEmpty(Platform::Exceptions::Ensure::Always, argument, argumentName, message);
            #endif
        }

        template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, Platform::Collections::System::ICollection auto& argument, std::string argumentName)
        {
            #ifdef DEBUG
                ArgumentNotEmpty(Platform::Exceptions::Ensure::Always, argument, argumentName, {});
            #endif
        }

        template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, Platform::Collections::System::ICollection auto& argument)
        {
            #ifdef DEBUG
                ArgumentNotEmpty(Platform::Exceptions::Ensure::Always, argument, {}, {});
            #endif
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument, std::string argumentName, std::string message)
        {
            #ifdef DEBUG
                ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::Ensure::Always, argument, argumentName, message);
            #endif
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument, std::string argumentName)
        {
            #ifdef DEBUG
                ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::Ensure::Always, argument, argumentName, {});
            #endif
        }

        static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument)
        {
            #ifdef DEBUG
                ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::Ensure::Always, argument, {}, {});
            #endif
        }
    };
}
