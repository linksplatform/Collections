using System;
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
    /// <seealso cref="DuplicateSegmentsWalkerBase{T, TSegment}"/>
    public abstract class DictionaryBasedDuplicateSegmentsWalkerBase<T, TSegment> : DuplicateSegmentsWalkerBase<T, TSegment>
        where TSegment : Segment<T>
    {
        /// <summary>
        /// <para>
        /// The default reset dictionary on each walk.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly bool DefaultResetDictionaryOnEachWalk;
        private readonly bool _resetDictionaryOnEachWalk;
        /// <summary>
        /// <para>
        /// The dictionary.
        /// </para>
        /// <para></para>
        /// </summary>
        protected IDictionary<TSegment, long> Dictionary;

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
        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, long> dictionary, int minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : base(minimumStringSegmentLength)
        {
            Dictionary = dictionary;
            _resetDictionaryOnEachWalk = resetDictionaryOnEachWalk;
        }

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
        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, long> dictionary, int minimumStringSegmentLength) : this(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

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
        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, long> dictionary) : this(dictionary, DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

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
        protected DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength, bool resetDictionaryOnEachWalk) : this(resetDictionaryOnEachWalk ? null : new Dictionary<TSegment, long>(), minimumStringSegmentLength, resetDictionaryOnEachWalk) { }

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
        protected DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength) : this(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DictionaryBasedDuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DictionaryBasedDuplicateSegmentsWalkerBase() : this(DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk) { }

        /// <summary>
        /// <para>
        /// Walks the all using the specified elements.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void WalkAll(IList<T> elements)
        {
            if (_resetDictionaryOnEachWalk)
            {
                var capacity = Math.Ceiling(Math.Pow(elements.Count, 2) / 2);
                Dictionary = new Dictionary<TSegment, long>((int)capacity);
            }
            base.WalkAll(elements);
        }

        /// <summary>
        /// <para>
        /// Gets the segment frequency using the specified segment.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="segment">
        /// <para>The segment.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The long</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override long GetSegmentFrequency(TSegment segment) => Dictionary.GetOrDefault(segment);

        /// <summary>
        /// <para>
        /// Sets the segment frequency using the specified segment.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="segment">
        /// <para>The segment.</para>
        /// <para></para>
        /// </param>
        /// <param name="frequency">
        /// <para>The frequency.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void SetSegmentFrequency(TSegment segment, long frequency) => Dictionary[segment] = frequency;
    }
}