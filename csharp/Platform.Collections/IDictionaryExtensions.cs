using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections 
{
    /// <summary>
    /// <para>Represents a set of extension methods <see cref="IDictionary{TKey, TValue}"/> array.</para>
    /// <para>Представляет набор методов расширения <see cref="IDictionary{TKey, TValue}"/>.</para> 
    /// </summary>  
    public static class IDictionaryExtensions
    {
        /// <summary>    
        /// <para> 
        /// Returns the value associated with the passed parameters  
        /// <paramref name="key"/> in <paramref name="dictionary"/> 
        /// </para>  
        /// <para> 
        /// Возвращает значение связанное с переданными параметрами  
        /// <paramref name="key"/> в <paramref name="dictionary"/> 
        /// </para> 
        /// </summary>   
        /// 
        /// <typeparam name="dictionary"> 
        /// <para>  
        /// <see cref="IDictionary<TKey, TValue>"/> from which to find the value associated with the <paramref name="key"/>.
        /// </para> 
        /// <see cref="IDictionary<TKey, TValue>"/> из которого нужно найти значение, связанное с <paramref name="key"/>.
        /// </typeparam>   
        /// 
        /// <typeparam name="key"> 
        /// <para>The key.</para> 
        /// <para>Ключ.</para> 
        /// </typeparam>  
        /// 
        /// <returns> 
        /// <para> 
        /// The value associated with the   
        /// <paramref name="key"/> in the <paramref name="dictionary"/> 
        /// </para>  
        /// <para> 
        /// Значение, связанное с
        /// <paramref name="key" в <paramref name="dictionary"/>
        /// </para> 
        /// </returns>        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.TryGetValue(key, out TValue value);
            return value;
        }

        /// <summary>    
        /// <para>Gets the value associated with the key in the dictionary or adds it if it does not exist.</para> 
        /// <para>Получает значение, связанное с ключом в словаре, или добавляет его, если он не существует.</para> 
        /// </summary>   
        /// 
        /// <typeparam name="dictionary"> 
        /// <para>  
        /// An <see cref="IDictionary<TKey, TValue>"/> to find the value associated with the  
        /// <paramref name="key"/> or to which to add a key-value pair if the  
        /// <paramref name="key"/> does not exist.
        /// </para> 
        /// <para>  
        /// Экземпляр <see cref="IDictionary<TKey, TValue>"/> из которого нужно найти значение ключа 
        /// <paramref name="key"/>, или добавить его, если ключ не существует.
        /// </para> 
        /// </typeparam>   
        /// 
        /// <typeparam name="key"> 
        /// <para>The key of the element to add.</para> 
        /// <para>Ключ добавляемого элемента.</para> 
        /// </typeparam>  
        ///  
        /// <typeparam name="valueFactory">  
        /// <para>The function that gets the key and returns a value for it.</para> 
        /// <para>Функция, которая получает ключ и возвращает значение для него.</para> 
        /// </typeparam>  
        /// 
        /// <returns> 
        /// <para>The value associated with the 
        /// <paramref name="key"/> in the  
        /// <paramref name="dictionary"/> if it exists; otherwise new value. 
        /// </para> 
        /// <para>Значение, связанное с  
        /// <paramref name="key"/> в  
        /// <paramref name="dictionary"/> если оно существует, иначе новое значение. 
        /// </para> 
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
