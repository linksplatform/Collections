namespace Platform::Collections
{
    class EnsureExtensions
    {
        public: template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, ICollection<T> &argument, std::string argumentName, std::string message)
        {
            if (argument.IsNullOrEmpty())
            {
                throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
            }
        }

        public: template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, ICollection<T> &argument, std::string argumentName) { ArgumentNotEmpty(root, argument, argumentName, {}); }

        public: template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, ICollection<T> &argument) { ArgumentNotEmpty(root, argument, {}, {}); }

        public: static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, std::string argument, std::string argumentName, std::string message)
        {
            if (std::string.IsNullOrWhiteSpace(argument))
            {
                throw std::invalid_argument(std::string("Invalid ").append(argumentName).append(" argument: ").append(message).append(1, '.'));
            }
        }

        public: static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, std::string argument, std::string argumentName) { ArgumentNotEmptyAndNotWhiteSpace(root, argument, argumentName, {}); }

        public: static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureAlwaysExtensionRoot root, std::string argument) { ArgumentNotEmptyAndNotWhiteSpace(root, argument, {}, {}); }

        public: template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, ICollection<T> &argument, std::string argumentName, std::string message) { Ensure.Always.ArgumentNotEmpty(argument, argumentName, message); }

        public: template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, ICollection<T> &argument, std::string argumentName) { Ensure.Always.ArgumentNotEmpty(argument, argumentName, {}); }

        public: template <typename T> static void ArgumentNotEmpty(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, ICollection<T> &argument) { Ensure.Always.ArgumentNotEmpty(argument, {}, {}); }

        public: static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument, std::string argumentName, std::string message) { Ensure.Always.ArgumentNotEmptyAndNotWhiteSpace(argument, argumentName, message); }

        public: static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument, std::string argumentName) { Ensure.Always.ArgumentNotEmptyAndNotWhiteSpace(argument, argumentName, {}); }

        public: static void ArgumentNotEmptyAndNotWhiteSpace(Platform::Exceptions::ExtensionRoots::EnsureOnDebugExtensionRoot root, std::string argument) { Ensure.Always.ArgumentNotEmptyAndNotWhiteSpace(argument, {}, {}); }
    };
}
