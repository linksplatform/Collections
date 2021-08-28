#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Represents the base abstract class for walkers on all elements.</para>
    /// <para>Представляет базовый абстрактный класс для проходчиков по всем элементам.</para>
    /// </summary>
    public abstract class AllSegmentsWalkerBase
    {
        /// <summary>
        /// <para>>Gets a default minimum length of string segment.</para> 
        /// <para>Возвращает минимально допустимую длину сегмента.</para> 
        /// </summary>
        public static readonly int DefaultMinimumStringSegmentLength = 2;
    }
}
