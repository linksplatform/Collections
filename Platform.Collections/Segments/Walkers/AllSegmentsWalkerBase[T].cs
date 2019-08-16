using System.Collections.Generic;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    public abstract class AllSegmentsWalkerBase<T> : AllSegmentsWalkerBase<T, Segment<T>>
    {
        protected override Segment<T> CreateSegment(IList<T> elements, int offset, int length) => new Segment<T>(elements, offset, length);
    }
}
