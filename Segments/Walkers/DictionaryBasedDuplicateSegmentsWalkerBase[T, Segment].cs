using System;
using System.Collections.Generic;

namespace Platform.Collections.Segments.Walkers
{
    public abstract class DictionaryBasedDuplicateSegmentsWalkerBase<T, TSegment> : DuplicateSegmentsWalkerBase<T, TSegment>
        where TSegment : Segment<T>
    {
        public static readonly bool DefaultResetDictionaryOnEachWalk;

        private readonly bool _resetDictionaryOnEachWalk;
        protected IDictionary<TSegment, long> Dictionary;

        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, long> dictionary, int minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : base(minimumStringSegmentLength)
        {
            Dictionary = dictionary;
            _resetDictionaryOnEachWalk = resetDictionaryOnEachWalk;
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, long> dictionary, int minimumStringSegmentLength)
            : this(dictionary, minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase(IDictionary<TSegment, long> dictionary)
            : this(dictionary, DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength, bool resetDictionaryOnEachWalk)
            : this(resetDictionaryOnEachWalk ? null : new Dictionary<TSegment, long>(), minimumStringSegmentLength, resetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase(int minimumStringSegmentLength)
            : this(minimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        protected DictionaryBasedDuplicateSegmentsWalkerBase()
            : this(DefaultMinimumStringSegmentLength, DefaultResetDictionaryOnEachWalk)
        {
        }

        public override void WalkAll(IList<T> elements)
        {
            if (_resetDictionaryOnEachWalk)
            {
                var capacity = Math.Ceiling(Math.Pow(elements.Count, 2) / 2);
                Dictionary = new Dictionary<TSegment, long>((int)capacity);
            }
            base.WalkAll(elements);
        }

        protected override long GetSegmentFrequency(TSegment segment) => Dictionary.GetOrDefault(segment);

        protected override void SetSegmentFrequency(TSegment segment, long frequency) => Dictionary[segment] = frequency;
    }
}