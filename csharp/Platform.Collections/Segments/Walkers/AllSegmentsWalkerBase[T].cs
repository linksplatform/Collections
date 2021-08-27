using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Provides the base class from which the classes that represent all segments walker are derived. This is an abstract class.</para>
    /// <para>Предоставляет базовый класс, от которого наследуются классы, реализующие AllSegmentsWalkerBase. Это абстрактный класс.</para>
    /// </summary>
    /// <seealso cref="AllSegmentsWalkerBase{T, Segment{T}}"/>
    public abstract class AllSegmentsWalkerBase<T> : AllSegmentsWalkerBase<T, Segment<T>>
    {
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="Segment"/> class, using the <paramref name="base"/> list of the segment, Offset relative to the list<paramref name="offset"/> and The segment's length <paramref name="length" />.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="Segment"/>, используя исходный список сегмента, <paramref name="base"/> Смещение относительно списка<paramref name="offset"/> и его длинну. <paramref name="length"/> </para>
        /// </summary>
        /// <param name="base"><para>The reference to the original list containing the elements of this segment.</para><para>Ссылка на исходный список в котором находятся элементы этого сегмента.</para></param>
        /// <param name="offset"><para>The offset relative to the <paramref name="base"/> list from which the segment starts.</para><para>Смещение относительно списка <paramref name="base"/>, с которого начинается сегмент.</para></param>
        /// <param name="length"><para>The segment's length.</para><para>Длина сегмента.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Segment<T> CreateSegment(IList<T> elements, int offset, int length) => new Segment<T>(elements, offset, length);
    }
}
