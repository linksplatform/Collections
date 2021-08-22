using System.Runtime.CompilerServices;
using Platform.Collections.Segments;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Arrays
{
    /// <summary>
    /// <para>
    /// Represents the array string.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="Segment{T}"/>
    public class ArrayString<T> : Segment<T>
    {
        /// <summary>
        /// <para>
        /// Initializes a new <see cref="ArrayString"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="length">
        /// <para>A length.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayString(int length) : base(new T[length], 0, length) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="ArrayString"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="array">
        /// <para>A array.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayString(T[] array) : base(array, 0, array.Length) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="ArrayString"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="array">
        /// <para>A array.</para>
        /// <para></para>
        /// </param>
        /// <param name="length">
        /// <para>A length.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayString(T[] array, int length) : base(array, 0, length) { }
    }
}
