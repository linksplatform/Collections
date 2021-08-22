using System;
using System.Globalization;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections
{
    /// <summary>
    /// <para>
    /// Represents the string extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// <para>
        /// Capitalizes the first letter using the specified string.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="@string">
        /// <para>The string.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The string.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CapitalizeFirstLetter(this string @string)
        {
            if (string.IsNullOrWhiteSpace(@string))
            {
                return @string;
            }
            var chars = @string.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                var category = char.GetUnicodeCategory(chars[i]);
                if (category == UnicodeCategory.UppercaseLetter)
                {
                    return @string;
                }
                if (category == UnicodeCategory.LowercaseLetter)
                {
                    chars[i] = char.ToUpper(chars[i]);
                    return new string(chars);
                }
            }
            return @string;
        }

        /// <summary>
        /// <para>
        /// Truncates the string.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="@string">
        /// <para>The string.</para>
        /// <para></para>
        /// </param>
        /// <param name="maxLength">
        /// <para>The max length.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The string</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Truncate(this string @string, int maxLength) => string.IsNullOrEmpty(@string) ? @string : @string.Substring(0, Math.Min(@string.Length, maxLength));

        /// <summary>
        /// <para>
        /// Trims the single using the specified string.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="@string">
        /// <para>The string.</para>
        /// <para></para>
        /// </param>
        /// <param name="charToTrim">
        /// <para>The char to trim.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The string</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string TrimSingle(this string @string, char charToTrim)
        {
            if (!string.IsNullOrEmpty(@string))
            {
                if (@string.Length == 1)
                {
                    if (@string[0] == charToTrim)
                    {
                        return "";
                    }
                    else
                    {
                        return @string;
                    }
                }
                else
                {
                    var left = 0;
                    var right = @string.Length - 1;
                    if (@string[left] == charToTrim)
                    {
                        left++;
                    }
                    if (@string[right] == charToTrim)
                    {
                        right--;
                    }
                    return @string.Substring(left, right - left + 1);
                }
            }
            else
            {
                return @string;
            }
        }
    }
}