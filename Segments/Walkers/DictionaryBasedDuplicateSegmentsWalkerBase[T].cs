using System.Collections.Generic;

namespace Platform.Collections.Segments.Walkers
{
    public abstract class DictionaryBasedDuplicateSegmentsWalkerBase<T> : DictionaryBasedDuplicateSegmentsWalkerBase<T, Segment<T>>
    {
        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary, int minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : base(dictionary, minimumStringSegmentLength, resetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary, int minimumStringSegmentLength)
            : base(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary)
            : base(dictionary, DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : base(minimumStringSegmentLength, resetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength)
            : base(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase()
            : base(DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }
    }
}
