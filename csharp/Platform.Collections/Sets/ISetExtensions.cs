using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Sets
{
    /// <summary>
    /// <para>
    /// Represents the set extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class ISetExtensions
    {
        /// <summary>
        /// <para>
        /// Adds the and return void using the specified set.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="element">
        /// <para>The element.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAndReturnVoid<T>(this ISet<T> set, T element) => set.Add(element);

        /// <summary>
        /// <para>
        /// Removes the and return void using the specified set.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="element">
        /// <para>The element.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAndReturnVoid<T>(this ISet<T> set, T element) => set.Remove(element);

        /// <summary>
        /// <para>
        /// Determines whether add and return true.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="element">
        /// <para>The element.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAndReturnTrue<T>(this ISet<T> set, T element)
        {
            set.Add(element);
            return true;
        }

        /// <summary>
        /// <para>
        /// Determines whether add first and return true.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddFirstAndReturnTrue<T>(this ISet<T> set, IList<T> elements)
        {
            AddFirst(set, elements);
            return true;
        }

        /// <summary>
        /// <para>
        /// Adds the first using the specified set.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddFirst<T>(this ISet<T> set, IList<T> elements) => set.Add(elements[0]);

        /// <summary>
        /// <para>
        /// Determines whether add all and return true.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAllAndReturnTrue<T>(this ISet<T> set, IList<T> elements)
        {
            set.AddAll(elements);
            return true;
        }

        /// <summary>
        /// <para>
        /// Adds the all using the specified set.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAll<T>(this ISet<T> set, IList<T> elements)
        {
            for (var i = 0; i < elements.Count; i++)
            {
                set.Add(elements[i]);
            }
        }

        /// <summary>
        /// <para>
        /// Determines whether add skip first and return true.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSkipFirstAndReturnTrue<T>(this ISet<T> set, IList<T> elements)
        {
            set.AddSkipFirst(elements);
            return true;
        }

        /// <summary>
        /// <para>
        /// Adds the skip first using the specified set.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSkipFirst<T>(this ISet<T> set, IList<T> elements) => set.AddSkipFirst(elements, 1);

        /// <summary>
        /// <para>
        /// Adds the skip first using the specified set.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <param name="skip">
        /// <para>The skip.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSkipFirst<T>(this ISet<T> set, IList<T> elements, int skip)
        {
            for (var i = skip; i < elements.Count; i++)
            {
                set.Add(elements[i]);
            }
        }

        /// <summary>
        /// <para>
        /// Determines whether do not contains.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="T">
        /// <para>The .</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="set">
        /// <para>The set.</para>
        /// <para></para>
        /// </param>
        /// <param name="element">
        /// <para>The element.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DoNotContains<T>(this ISet<T> set, T element) => !set.Contains(element);
    }
}
