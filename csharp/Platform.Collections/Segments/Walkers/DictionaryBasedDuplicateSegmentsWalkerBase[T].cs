using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>
    /// Represents the dictionary based duplicate segments walker base.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="DictionaryBasedDuplicateSegmentsWalkerBase{T, Segment{T}}"/>
    public abstract class DictionaryBasedDuplicateSegmentsWalkerBase<T> : DictionaryBasedDuplicateSegmentsWalkerBase<T, Segment<T>>
    {
        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DictionaryBasedDuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="dictionary">
        /// <para>A dictionary.</para>
        /// <para></para>
        /// </param>
        /// <param name="minimumStringSegmentLength">
        /// <para>A minimum string segment length.</para>
        /// <para></para>
        /// </param>
        /// <param name="resetDictionaryOnEachWalk">
        /// <para>A reset dictionary on each walk.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary, int minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : base(dictionary, minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DictionaryBasedDuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="dictionary">
        /// <para>A dictionary.</para>
        /// <para></para>
        /// </param>
        /// <param name="minimumStringSegmentLength">
        /// <para>A minimum string segment length.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary, int minimumStringSegmentLength) : base(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DictionaryBasedDuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="dictionary">
        /// <para>A dictionary.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary) : base(dictionary, DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DictionaryBasedDuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="minimumStringSegmentLength">
        /// <para>A minimum string segment length.</para>
        /// <para></para>
        /// </param>
        /// <param name="resetDictionaryOnEachWalk">
        /// <para>A reset dictionary on each walk.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : base(minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DictionaryBasedDuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="minimumStringSegmentLength">
        /// <para>A minimum string segment length.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength) : base(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DictionaryBasedDuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DictionaryBasedDuplicateSegmentsWalkerBase() : base(DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }
    }
}
