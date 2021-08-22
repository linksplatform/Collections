using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>
    /// Represents the all segments walker extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class AllSegmentsWalkerExtensions
    {
        /// <summary>
        /// <para>
        /// Walks the all using the specified walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <param name="@string">
        /// <para>The string.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WalkAll(this AllSegmentsWalkerBase<char> walker, string @string) => walker.WalkAll(@string.ToCharArray());

        /// <summary>
        /// <para>
        /// Walks the all using the specified walker.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TSegment">
        /// <para>The segment.</para>
        /// <para></para>
        /// </typeparam>
        /// <param name="walker">
        /// <para>The walker.</para>
        /// <para></para>
        /// </param>
        /// <param name="@string">
        /// <para>The string.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WalkAll<TSegment>(this AllSegmentsWalkerBase<char, TSegment> walker, string @string) where TSegment : Segment<char> => walker.WalkAll(@string.ToCharArray());
    }
}
