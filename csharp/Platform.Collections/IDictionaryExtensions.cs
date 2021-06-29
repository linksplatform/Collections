using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections 
{
    /// <summary>
    /// <para>Represents a set of extension methods <see cref="IDictionary{TKey, TValue}"/> array.</para>
    /// <para>Представляет набор методов расширения <see cref="IDictionary{TKey, TValue}"/>.</para>
    public static class IDictionaryExtensions
    {
        /// <summary>  
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

        /// <summary>  
        /// <typeparam name="dictionary"> 
        /// <para>Dictionary that is a key pair:meaning.</para> 
        /// <para>Словарь который пара ключ:значение</para> 
        /// </typeparam>  
        /// <typeparam name="key"> 
        /// <para>Key.</para> 
        /// <para>Ключ.</para> 
        /// </typeparam>  
        /// <typeparam name="valueFactory">  
        /// <para>The function, will return the value of the argument, which is the key.</para> 
        /// <para>Функция, вернет значение по аргументу, который ключ.</para> 
        /// </typeparam> 
        /// <returns> 
        /// <para>Value that is not in the dictionary.</para> 
        /// <para>Значение, которого нет в dictionary.</para> 
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
