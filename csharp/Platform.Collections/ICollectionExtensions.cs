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
        /// <typeparam name="T">
        /// <para>Collection elements data type.</para>
        /// <para>Тип данных элемента коллекций.</para>
        /// </typeparam>
        /// <param name="collection">
        /// <para>Takes an elements collection of <see cref="ICollection<T>"/> type.</para>
        /// <para>Принимает колекцию элементов <see cref="ICollection<T>"/> типа.</para>
        /// </param>
        /// <returns>
        /// <para>The <see cref="bool"/> type variable equal to false if the collection is empty else returns true.</para>
        /// <para>Переменная типа <see cref="bool"/> равна false, если коллекция пуста, иначе возвращает true.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) => collection == null || collection.Count == 0;
        
        /// <summary>
        /// <para>Checks if all elements of the collection <see cref="default"/>.</para>
        /// <para>Проверяет являются ли все элементы коллекции <see cref="default"/>.</para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>Collection elements data type.</para>
        /// <para>Тип данных элементов коллекций.</para>
        /// </typeparam>
        /// <param>
        /// <para>Method takes an elements collection of <see cref="ICollection<T>"/> type.</para>
        /// <para>Метод принимает колекцию элементов <see cref="ICollection<T>"/> типа.</para>
        /// </param>
        /// <returns>
        /// <para>Returns a variable of type <see cref="bool"/> equal to true if all elements in the collection are default values else returns false.</para>
        /// <para>Возвращает переменную типа <see cref="bool"/> равной true если все элементы коллекции являются значениями по умолчанию иначе возвращает false.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AllEqualToDefault<T>(this ICollection<T> collection)
        {
            var equalityComparer = EqualityComparer<T>.Default;
            return collection.All(item => equalityComparer.Equals(item, default));
        }
    }
}
