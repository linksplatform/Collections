using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections
{
    /// <summary>
    /// <para>
    /// Represents the dictionary extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// <para>
        /// Gets the or default using the specified dictionary.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TKey">
        /// <para>The key.</para>
        /// <para></para>
        /// </typeparam>
        /// <typeparam name="TValue">
        /// <para>The value.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="dictionary">
        /// <para>The dictionary.</para>
        /// <para></para>
        /// </param>
        /// <param name="key">
        /// <para>The key.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The value.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out TValue value);
            return value;
        }

        /// <summary>
        /// <para>
        /// Gets the or add using the specified dictionary.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TKey">
        /// <para>The key.</para>
        /// <para></para>
        /// </typeparam>
        /// <typeparam name="TValue">
        /// <para>The value.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="dictionary">
        /// <para>The dictionary.</para>
        /// <para></para>
        /// </param>
        /// <param name="key">
        /// <para>The key.</para>
        /// <para></para>
        /// </param>
        /// <param name="valueFactory">
        /// <para>The value factory.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The value.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!dictionary.TryGetValue(key, out TValue value))
            {
                value = valueFactory(key);
                dictionary.Add(key, value);
                return value;
            }
            return value;
        }
    }
}
