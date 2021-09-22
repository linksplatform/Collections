using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments.Walkers
{
    /// <summary>
    /// <para>Represents the base class for walkers through all possible segments of a sequence.</para>
    /// <para>Представляет базовый класс для проходчиков по всем возможным сегментам последовательности.</para>
    /// </summary>
    /// <seealso cref="AllSegmentsWalkerBase"/>
    /// <seealso cref="AllSegmentsWalkerBase{T}"/>
    public abstract class AllSegmentsWalkerBase<T, TSegment> : AllSegmentsWalkerBase
        where TSegment : Segment<T>
    {
        /// <summary>
        /// <para>A read-only field that represents the minimum length of the sequence segment.</para>
        /// <para>Поле только для чтения, которое представляет минимальную длину сегмента последовательности.</para>
        /// </summary>
        private readonly int _minimumStringSegmentLength;

        /// <summary>
        /// <para>Constructor that initializes the field <see cref="_minimumStringSegmentLength"/>, inserts the default value there.</para>
        /// <para>Конструктор, который инициализирует поле <see cref="_minimumStringSegmentLength"/> , вставляет туда значение по умолчанию.</para>
        /// </summary>
        /// <param name="minimumStringSegmentLength">
        /// <para>The parameter that takes a field <see cref="DefaultMinimumStringSegmentLength"/>.</para>
        /// <para>Параметр, принимающий поле <see cref="DefaultMinimumStringSegmentLength"/>.</para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected AllSegmentsWalkerBase(int minimumStringSegmentLength) => _minimumStringSegmentLength = minimumStringSegmentLength;

        /// <summary>
        /// <para>Constructor referencing another constructor by passing a field to its parameter <see cref="DefaultMinimumStringSegmentLength"/>.</para>
        /// <para>Конструктор, ссылающийся на другой конструктор, передавая в его параметр поле <see cref="DefaultMinimumStringSegmentLength"/>.</para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected AllSegmentsWalkerBase() : this(DefaultMinimumStringSegmentLength) { }

        /// <summary>
        /// <para>
        /// Walks the all using the specified elements.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void WalkAll(IList<T> elements)
        {
            for (int offset = 0, maxOffset = elements.Count - _minimumStringSegmentLength; offset <= maxOffset; offset++)
            {
                for (int length = _minimumStringSegmentLength, maxLength = elements.Count - offset; length <= maxLength; length++)
                {
                    Iteration(CreateSegment(elements, offset, length));
                }
            }
        }

        /// <summary>
        /// <para>
        /// Creates the segment using the specified elements.
        /// </para>
        /// <para>Представляет базовый метод, для всех методов создания мегмента.</para>
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
        /// <para>Created megment</para>
        /// <para>Созданный мегмент</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract TSegment CreateSegment(IList<T> elements, int offset, int length);

        /// <summary>
        /// <para>
        /// Iterations the segment.
        /// </para>
        /// <para>Представляет базоый метод, для всех методов итерации.</para>
        /// </summary>
        /// <param name="segment">
        /// <para>The segment.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Iteration(TSegment segment);
    }
}
