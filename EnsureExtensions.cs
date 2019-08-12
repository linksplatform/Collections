using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Platform.Exceptions;
using Platform.Exceptions.ExtensionRoots;

#pragma warning disable IDE0060 // Remove unused parameter

namespace Platform.Collections
{
    public static class EnsureExtensions
    {
        #region Always

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotEmpty<T>(this EnsureAlwaysExtensionRoot root, ICollection<T> argument, string argumentName, string message)
        {
            if (argument.IsNullOrEmpty())
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotEmpty<T>(this EnsureAlwaysExtensionRoot root, ICollection<T> argument, string argumentName) => ArgumentNotEmpty(root, argument, argumentName, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotEmpty<T>(this EnsureAlwaysExtensionRoot root, ICollection<T> argument) => ArgumentNotEmpty(root, argument, null, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotEmptyAndNotWhiteSpace(this EnsureAlwaysExtensionRoot root, string argument, string argumentName, string message)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotEmptyAndNotWhiteSpace(this EnsureAlwaysExtensionRoot root, string argument, string argumentName) => ArgumentNotEmptyAndNotWhiteSpace(root, argument, argumentName, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ArgumentNotEmptyAndNotWhiteSpace(this EnsureAlwaysExtensionRoot root, string argument) => ArgumentNotEmptyAndNotWhiteSpace(root, argument, null, null);

        #endregion

        #region OnDebug

        [Conditional("DEBUG")]
        public static void ArgumentNotEmpty<T>(this EnsureOnDebugExtensionRoot root, ICollection<T> argument, string argumentName, string message) => Ensure.Always.ArgumentNotEmpty(argument, argumentName, message);

        [Conditional("DEBUG")]
        public static void ArgumentNotEmpty<T>(this EnsureOnDebugExtensionRoot root, ICollection<T> argument, string argumentName) => Ensure.Always.ArgumentNotEmpty(argument, argumentName, null);

        [Conditional("DEBUG")]
        public static void ArgumentNotEmpty<T>(this EnsureOnDebugExtensionRoot root, ICollection<T> argument) => Ensure.Always.ArgumentNotEmpty(argument, null, null);

        [Conditional("DEBUG")]
        public static void ArgumentNotEmptyAndNotWhiteSpace(this EnsureOnDebugExtensionRoot root, string argument, string argumentName, string message) => Ensure.Always.ArgumentNotEmptyAndNotWhiteSpace(argument, argumentName, message);

        [Conditional("DEBUG")]
        public static void ArgumentNotEmptyAndNotWhiteSpace(this EnsureOnDebugExtensionRoot root, string argument, string argumentName) => Ensure.Always.ArgumentNotEmptyAndNotWhiteSpace(argument, argumentName, null);

        [Conditional("DEBUG")]
        public static void ArgumentNotEmptyAndNotWhiteSpace(this EnsureOnDebugExtensionRoot root, string argument) => Ensure.Always.ArgumentNotEmptyAndNotWhiteSpace(argument, null, null);

        #endregion
    }
}
