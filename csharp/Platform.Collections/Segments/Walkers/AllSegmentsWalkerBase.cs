#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Represents the base class for sinkers for all possible segments of the sequence.</para>
    /// <para>Представляет базовый класс для проходчиков по всем возможным сегментам последовательности.</para>
    /// </summary>
    public abstract class AllSegmentsWalkerBase
    {
        /// <summary>
        /// <para>Read-only field that represents the minimum length of the default sequence segment.</para> 
        /// <para>Поле только для чтения, которое представляет минимальную длину сегмента последовательности по умолчанию.</para> 
        /// </summary>
        public static readonly int DefaultMinimumStringSegmentLength = 2;
    }
}