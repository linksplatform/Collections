using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>All Segment sWalker Base or Class with WalkAll method to walk through all segments in the dictionary.</para>
    /// <para>Базовый проходчик всех сегментов или Класс с методом WalkAll для прохода по всем сегментам в словаре.</para>
    /// </summary>
    /// <seealso cref="AllSegmentsWalkerBase{T, Segment{T}}"/>
    public abstract class AllSegmentsWalkerBase<T> : AllSegmentsWalkerBase<T, Segment<T>>
    {
         /// <summary>
        /// <para>Calls the segment constructor, initializes 3 fields on the segment instance</para>
        /// <para>Вызывает конструктор сегмента, инициализирует 3 поля у экземпляра сегмента.</para>
        /// </summary>
        /// <param name="elements"> <para>The original list in which the elements of this segment are located.</para> <para>Исходный список в котором находятся элементы этого сегмента.</para> </param>
        /// <param name="offset"> <para>Offset relative to the list.</para> <para>Смещение относительно списка.</para> </param>
        /// <param name="length"> <para>Segment length.</para> <para>Длина сегмента.</para> </param>
        /// <returns>
        /// <para>An instance of the Segment class.</para> <para>Экземпляр класса Segment<T>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override Segment<T> CreateSegment(IList<T> elements, int offset, int length) => new Segment<T>(elements, offset, length);
    }
}
