#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Provides the base class from which the classes that represent all segments walker are derived. This is an abstract class.</para>
    /// <para>Предоставляет базовый класс, от которого наследуются классы, реализующие AllSegmentsWalkerBase. Это абстрактный класс.</para>
    /// </summary>
    public abstract class AllSegmentsWalkerBase
    {
        /// <summary>
        /// <para>Gets default minimum string segment length.</para> 
        /// <para>Возвращает дефолтную минимальную длинну строки мегмента.</para> 
        /// <para></para>
        /// </summary>
        public static readonly int DefaultMinimumStringSegmentLength = 2;
    }
}
