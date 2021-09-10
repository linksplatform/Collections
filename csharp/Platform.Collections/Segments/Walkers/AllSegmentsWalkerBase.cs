#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Represents a base class for walkers through all possible segments of a sequence.</para>
    /// <para>Представляет базовый класс для проходчиков по всем возможным сегментам последовательности.</para>
    /// </summary>
    public abstract class AllSegmentsWalkerBase
    {
        /// <summary>
        /// <para>A read-only field that represents the default minimum length of the sequence segment.</para> 
        /// <para>Поле только для чтения, которое представляет минимальную длину сегмента последовательности по умолчанию.</para> 
        /// </summary>
        public static readonly int DefaultMinimumStringSegmentLength = 2;
    }
}