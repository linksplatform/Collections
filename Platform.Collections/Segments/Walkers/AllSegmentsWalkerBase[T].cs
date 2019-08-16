using System.Collections.Generic;

namespace Platform.Collections.Segments.Walkers
{
    public abstract class AllSegmentsWalkerBase<T> : AllSegmentsWalkerBase<T, Segment<T>>
    {
        protected override Segment<T> CreateSegment(IList<T> elements, int offset, int length) => new Segment<T>(elements, offset, length);
    }
}
