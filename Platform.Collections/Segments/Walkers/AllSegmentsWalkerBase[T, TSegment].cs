using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    public abstract class AllSegmentsWalkerBase<T, TSegment> : AllSegmentsWalkerBase
        where TSegment : Segment<T>
    {
        private readonly int _minimumStringSegmentLength;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected AllSegmentsWalkerBase(int minimumStringSegmentLength) => _minimumStringSegmentLength = minimumStringSegmentLength;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected AllSegmentsWalkerBase() : this(DefaultMinimumStringSegmentLength) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void WalkAll(IList<T> elements)
        {
            for (int offset = 0, maxOffset = elements.Count - _minimumStringSegmentLength; offset <= maxOffset; offset++)
            {
                for (int length = _minimumStringSegmentLength, maxLength = elements.Count - offset; length <= maxLength; length++)
                {
                    Iteration(CreateSegment(elements, offset, length));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract TSegment CreateSegment(IList<T> elements, int offset, int length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Iteration(TSegment segment);
    }
}
