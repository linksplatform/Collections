#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    public abstract class DuplicateSegmentsWalkerBase<T, TSegment> : AllSegmentsWalkerBase<T, TSegment>
        where TSegment : Segment<T>
    {
        protected DuplicateSegmentsWalkerBase(int minimumStringSegmentLength) : base(minimumStringSegmentLength) { }

        protected DuplicateSegmentsWalkerBase() : base(DefaultMinimumStringSegmentLength) { }

        protected override void Iteration(TSegment segment)
        {
            var frequency = GetSegmentFrequency(segment);
            if (frequency == 1)
            {
                OnDublicateFound(segment);
            }
            SetSegmentFrequency(segment, frequency + 1);
        }

        protected abstract void OnDublicateFound(TSegment segment);
        protected abstract long GetSegmentFrequency(TSegment segment);
        protected abstract void SetSegmentFrequency(TSegment segment, long frequency);
    }
}
