using System;
using System.Globalization;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections
{
    public static class StringExtensions
    {
        public static string CapitalizeFirstLetter(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }
            var chars = str.ToCharArray();
            for (var i = 0; i < chars.Length; i++)
            {
                var category = char.GetUnicodeCategory(chars[i]);
                if (category == UnicodeCategory.UppercaseLetter)
                {
                    return str;
                }
                if (category == UnicodeCategory.LowercaseLetter)
                {
                    chars[i] = char.ToUpper(chars[i]);
                    return new string(chars);
                }
            }
            return str;
        }

        public static string Truncate(this string str, int maxLength) => string.IsNullOrEmpty(str) ? str : str.Substring(0, Math.Min(str.Length, maxLength));
    }
}