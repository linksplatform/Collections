using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections
{
    /// <summary>
    /// <para>Presents a set of methods for working with collections.</para>
    /// <para>Представляет набор методов для работы с коллекциями.</para>
    /// </summary>
    public static class ICollectionExtensions
    {
        /// <summary>
        /// <para>Checking collection for empty.</para>
        /// <para>Проверяет коллекцию на пустоту.</para>
        /// </summary>
        /// <param name="collection">
        /// <para>Method takes an elements collection of <see cref="ICollection<T>"/> type.</para>
        /// <para>Метода принимает колекцию элементов <see cref="ICollection<T>"/> типа.</para>
        /// </param>
        /// <returns>
        /// <para>Returns a <see cref="bool"/> type variable equal to False if the collection is empty else returns true.</para>
        /// <para>Возвращает переменную типа <see cref="bool"/> равной false если коллекция пустая иначе возвращает true.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) => collection == null || collection.Count == 0;

        /// <summary>
        /// <para>
        /// Determines whether all equal to default.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="collection">
        /// <para>The collection.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllEqualToDefault<T>(this ICollection<T> collection)
        {
            var equalityComparer = EqualityComparer<T>.Default;
            return collection.All(item => equalityComparer.Equals(item, default));
        }
    }
}
