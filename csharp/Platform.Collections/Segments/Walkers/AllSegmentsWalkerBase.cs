#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Represents the base abstract class for walkers on all elements.</para>
    /// <para>Представляет базовый класс для проходчиков по всем элементам.</para>
    /// </summary>
    public abstract class AllSegmentsWalkerBase
    {
        /// <summary>
        /// <para>The minimum length of the string segment.</para> 
        /// <para>Минимально допустимая длинна сегмента.</para> 
        /// </summary>
        public static readonly int DefaultMinimumStringSegmentLength = 2;
    }
}
    