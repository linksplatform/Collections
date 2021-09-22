using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Represents the base class for walkers through all possible segments of a sequence.</para>
    /// <para>Представляет базовый класс для проходчиков по всем возможным сегментам последовательности.</para>
    /// </summary>
    /// <seealso cref="AllSegmentsWalkerBase{T, Segment{T}}"/>
    /// <seealso cref="AllSegmentsWalkerBase"/>
    /// <typeparam name="T">
    /// <para>The sequence's element type.</para>
    /// <para>Тип элемента последовательности.</para>
    /// </typeparam>
    public abstract class AllSegmentsWalkerBase<T> : AllSegmentsWalkerBase<T, Segment<T>>
    {
        /// <summary>
        /// <para>Create a new instance of the <see cref="Segment{T}"/> class based on <paramref name="elements"/> sequence.</para>
        /// <para>Создаёт новый экземпляр класса <see cref="Segment{T}"/> на основе последовательности <paramref name="elements"/>.</para>
        /// </summary>
        /// <param name="elements">
        /// <para>A list of elements.</para>
        /// <para>Список элементов.</para>
        /// </param>
        /// <param name="offset">
        /// <para>An offset relative to the <paramref name="elements"/> sequence from which the segment starts.</para>
        /// <para>Смещение относительно списка <paramref name="elements"/>, с которого начинается сегмент.</para>
        /// </param>
        /// <param name="length">
        /// <para>A segment's length.</para>
        /// <para>Длина сегмента.</para>
        /// </param>
        /// <returns>
        /// <para>An instance of the <see cref="Segment{T}"/> class.</para>
        /// <para>Экземпляр класса <see cref="Segment{T}"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Segment<T> CreateSegment(IList<T> elements, int offset, int length) => new Segment<T>(elements, offset, length);
    }
}