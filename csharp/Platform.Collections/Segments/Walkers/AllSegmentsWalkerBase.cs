#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>All Segment sWalker Base or Class with WalkAll method to walk through all segments in the dictionary.</para>
    /// <para>Базовый проходчик всех сегментов или Класс с методом WalkAll для прохода по всем сегментам в словаре.</para>
    /// </summary>
    public abstract class AllSegmentsWalkerBase
    {
        /// <summary>
        /// <para>
        /// Default value for the field _minimumStringSegmentLength object of the same class. Assigned when the constructor is called.
        /// </para>
        /// <para>
        /// Дефолтное значение для поля _minimumStringSegmentLength этого же класса, используемого в методе  WalkAll. Присваивается при вызове конструктора.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly int DefaultMinimumStringSegmentLength = 2;
    }
}
