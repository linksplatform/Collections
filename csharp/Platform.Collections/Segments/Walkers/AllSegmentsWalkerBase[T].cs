using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Represents the base abstract class for walkers on all elements.</para>
    /// <para>Представляет базовый абстрактный класс для проходчиков по всем элементам.</para>
    /// </summary>
    /// <seealso cref="AllSegmentsWalkerBase{T, Segment{T}}"/>
    public abstract class AllSegmentsWalkerBase<T> : AllSegmentsWalkerBase<T, Segment<T>>
    {
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="Segment"/> class.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="Segment"/> .</para>
        /// </summary>
        /// <param name="elements">
        /// <para>List of elements.</para>
        /// <para>Список  элементов.</para>
        /// </param>
        /// <param name="offset">
        /// <para>An offset relative to the <paramref name="elements"/> list from which the segment starts.</para>
        /// <para>Смещение относительно списка <paramref name="elements"/>, с которого начинается сегмент.</para>
        /// </param>
        /// <param name="length">
        /// <para>A segment's length.</para>
        /// <para>Длина сегмента.</para>
        /// </param>
        /// <returns>
        /// <para>An instance of the <see cref="Segment{T}"/> class.</para>
        /// <para>Экземпляр класса <see cref="Segment{T}"/> .</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Segment<T> CreateSegment(IList<T> elements, int offset, int length) => new Segment<T>(elements, offset, length);
    }
}