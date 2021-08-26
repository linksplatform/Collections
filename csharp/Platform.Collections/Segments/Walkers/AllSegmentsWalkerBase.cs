#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>
    /// Represents the all segments walker base.
    /// </para>
    /// <para>
    /// Представляет Все базовые сегменты ходьбы
    /// </para>
    /// <para></para>
    /// </summary>
    public abstract class AllSegmentsWalkerBase
    {
        /// <summary>
        /// <para>
        /// Default value for the field _minimumStringSegmentLength object of the same class. Assigned when the constructor is called.
        /// </para>
        /// <para>
        /// Дефолтное значение для поля _minimumStringSegmentLength объекта этого же класса. Присваивается при вызове конструктора.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly int DefaultMinimumStringSegmentLength = 2;
    }
}
