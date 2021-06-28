using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections 
{
    /// <summary>
    /// <para>Represents a set of extension methods<see cref="T:T[]"/> array.</para>
    /// <para>Представляет набор методов расширения<see cref="T:T[]"/>.</para>
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <typeparam name="dictionary"> 
        /// <para>Dictionary that is a key pair:meaning.</para> 
        /// <para>Словарь который пара ключ:значение</para> 
        /// </typeparam>  
        /// <typeparam name="key"> 
        /// <para>Key.</para> 
        /// <para>Ключ.</para> 
        /// </typeparam> 
        /// <returns> 
        /// <para>The value of the active dictionary.</para> 
        /// <para>Значение активного словаря.</para> 
        /// </returns>        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out TValue value);
            return value;
        }

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
