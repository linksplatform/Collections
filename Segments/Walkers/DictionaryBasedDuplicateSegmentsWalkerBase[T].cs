using System.Collections.Generic;

namespace Platform.Collections.Segments.Walkers
{
    public abstract class DictionaryBasedDuplicateSegmentsWalkerBase<T> : DictionaryBasedDuplicateSegmentsWalkerBase<T, Segment<T>>
    {
        public DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary, int minimumStringSegmentLength = DefaultMinimumStringSegmentLength, bool resetDictionaryOnEachWalk = DefaultResetDictionaryOnEachWalk)
            : base(dictionary, minimumStringSegmentLength, resetDictionaryOnEachWalk)
        {
        }

        public DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength = DefaultMinimumStringSegmentLength, bool resetDictionaryOnEachWalk = DefaultResetDictionaryOnEachWalk)
            : base(minimumStringSegmentLength, resetDictionaryOnEachWalk)
        {
        }

        public DictionaryBasedDuplicateSegmentsWalkerBase(bool resetDictionaryOnEachWalk = DefaultResetDictionaryOnEachWalk)
            : base(resetDictionaryOnEachWalk)
        {
        }
    }
}
