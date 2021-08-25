using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>
    /// Represents the duplicate segments walker base.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="AllSegmentsWalkerBase{T, TSegment}"/>
    public abstract class DuplicateSegmentsWalkerBase<T, TSegment> : AllSegmentsWalkerBase<T, TSegment>
        where TSegment : Segment<T>
    {
        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="minimumStringSegmentLength">
        /// <para>A minimum string segment length.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DuplicateSegmentsWalkerBase(int minimumStringSegmentLength) : base(minimumStringSegmentLength) { }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="DuplicateSegmentsWalkerBase"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected DuplicateSegmentsWalkerBase() : base(DefaultMinimumStringSegmentLength) { }

        /// <summary>
        /// <para>
        /// Iterations the segment.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="segment">
        /// <para>The segment.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Iteration(TSegment segment)
        {
            var frequency = GetSegmentFrequency(segment);
            if (frequency == 1)
            {
                OnDublicateFound(segment);
            }
            SetSegmentFrequency(segment, frequency + 1);
        }

        /// <summary>
        /// <para>
        /// Ons the dublicate found using the specified segment.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="segment">
        /// <para>The segment.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void OnDublicateFound(TSegment segment);

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
        protected abstract long GetSegmentFrequency(TSegment segment);

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
        protected abstract void SetSegmentFrequency(TSegment segment, long frequency);
    }
}
