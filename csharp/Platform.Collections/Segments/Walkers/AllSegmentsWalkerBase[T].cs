using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>
    /// Represents the all segments walker base.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="AllSegmentsWalkerBase{T, Segment{T}}"/>
    public abstract class AllSegmentsWalkerBase<T> : AllSegmentsWalkerBase<T, Segment<T>>
    {
        /// <summary>
        /// <para>
        /// Creates the segment using the specified elements.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <param name="offset">
        /// <para>The offset.</para>
        /// <para></para>
        /// </param>
        /// <param name="length">
        /// <para>The length.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A segment of t</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Segment<T> CreateSegment(IList<T> elements, int offset, int length) => new Segment<T>(elements, offset, length);
    }
}
