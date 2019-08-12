using System.Collections.Generic;

namespace Platform.Collections.Segments.Walkers
{
    public abstract class DictionaryBasedDuplicateSegmentsWalkerBase<T> : DictionaryBasedDuplicateSegmentsWalkerBase<T, Segment<T>>
    {
        public DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary, int minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : base(dictionary, minimumStringSegmentLength, resetDictionaryOnEachWalk)
        {
        }

        public DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary, int minimumStringSegmentLength)
            : base(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        public DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<Segment<T>, long> dictionary)
            : base(dictionary, DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        public DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : base(minimumStringSegmentLength, resetDictionaryOnEachWalk)
        {
        }

        public DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength)
            : base(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        public DictionaryBasedDuplicateSegmentsWalkerBase()
            : base(DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }
    }
}
