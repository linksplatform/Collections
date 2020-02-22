using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    public static class AllSegmentsWalkerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WalkAll(this AllSegmentsWalkerBase<char> walker, string @string) => walker.WalkAll(@string.ToCharArray());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WalkAll<TSegment>(this AllSegmentsWalkerBase<char, TSegment> walker, string @string) where TSegment : Segment<char> => walker.WalkAll(@string.ToCharArray());
    }
}
