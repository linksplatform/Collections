using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Collections.Arrays;
using Platform.Collections.Lists;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Segments
{
    /// <summary>
    /// <para>Represents the segment <see cref="IList"/>.</para>
    /// <para>Представляет сегмент <see cref="IList"/>.</para>
    /// </summary>
    /// <typeparam name="T"><para>The segment elements type.</para><para>Тип элементов сегмента.</para></typeparam>
    public class Segment<T> : IEquatable<Segment<T>>, IList<T>
    {
        /// <summary>
        /// <para>Returns the original list (of which this segment is a part).</para>
        /// <para>Возвращает исходный список (частью которого является этот сегмент).</para>
        /// </summary>
        public IList<T> Base
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }
        /// <summary>
        /// <para>Returns the offset of the relative source list (the index at which this segment starts).</para>
        /// <para>Возвращает смещение относительного исходного списка (индекс с которого начинается этот сегмент).</para>
        /// </summary>
        public int Offset
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }
        /// <summary>
        /// <para>Returns the length of a segment.</para>
        /// <para>Возвращает длину сегмента.</para>
        /// </summary>
        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="Segment"/> class, using the passed list as the original, <paramref name="offset"/> segment and its <paramref name="length" />.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="Segment"/>, используя переданный список как исходный, <paramref name="offset"/> сегмента и его <paramref name="length"/>.</para>
        /// </summary>
        /// <param name="base"><para>Reference to the original list containing the elements of this segment.</para><para>Ссылка на исходный список в котором находятся элементы этого сегмента.</para></param>
        /// <param name="offset"><para>The offset relative to the source list <paramref name="base"/> from which the segment starts.</para><para>Смещение относительно исходного списка <paramref name="base"/>, с которого начинается сегмент.</para></param>
        /// <param name="length"><para>Segment length.</para><para>Длина сегмента.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Segment(IList<T> @base, int offset, int length)
        {
            Base = @base;
            Offset = offset;
            Length = length;
        }
        
        /// <summary>
        /// <para>Gets the hash code of the current instance <see cref="Segment"/>.</para>
        /// <para>Возвращает хэш-код текущего экземпляра <see cref="Segment"/>.</para>
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() => this.GenerateHashCode();

        /// <summary>
        /// <para>Returns a value indicating whether the current <see cref="Segment" /> is equal to another <see cref="Segment" />.</para>
        /// <para>Возвращает значение определяющее, равен ли текущий <see cref="Segment"/> другому <see cref="Segment"/>.</para>
        /// </summary>
        /// <param name="other"><para>An <see cref="Segment"/> object to compare with the current <see cref="Segment"/>.</para><para>Объект <see cref="Segment"/> для сравнения с текущим <see cref="Segment"/>.<para></para></param>
        /// <returns>
        /// <para><see langword="true"/> if the current <see cref="Segment"/> is equal to the <paramref name="other"/> parameter; otherwise, <see langword="false"/>.</para>
        /// <para><see langword="true"/>, если текущий <see cref="Segment"/> эквивалентен параметру <paramref name="other"/>, в противном случае — <see langword="false"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual bool Equals(Segment<T> other) => this.EqualTo(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj) => obj is Segment<T> other ? Equals(other) : false;

        #region IList

        public T this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Base[Offset + i];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Base[Offset + i] = value;
        }

        /// <summary>
        /// <para>Gets the number of elements contained in the <see cref="Segment"/>.</para>
        /// <para>Получает число элементов, содержащихся в <see cref="Segment"/>.</para>
        /// </summary>
        /// <value>
        /// <para>The number of elements contained in the <see cref="Segment"/>.</para>
        /// <para>Число элементов, содержащихся в <see cref="Segment"/>.</para>
        /// </value>
        public int Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Length;
        }

        /// <summary>
        /// <para>Gets a value indicating whether the <see cref="Segment"/> is read-only.</para>
        /// <para>Получает значение, указывающее, является ли <see cref="Segment"/> доступным только для чтения.</para>
        /// </summary>
        /// <value>
        /// <para><see langword="true"/> if the <see cref="Segment"/> is read-only; otherwise, <see langword="false"/>.</para>
        /// <para>Значение <see langword="true"/>, если <see cref="Segment"/> доступен только для чтения, в противном случае — значение <see langword="false"/>.</para>
        /// </value>
        /// <remarks>
        /// <para>Any <see cref="Segment"/> is read-only.</para>
        /// <para>Любой <see cref="Segment"/> доступен только для чтения.</para>
        /// </remarks>
        public bool IsReadOnly
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => true;
        }
         
        /// <summary>
        /// <para>Determines the index of a specific item in the <see cref="Segment"/>.</para>
        /// <para>Определяет индекс заданного элемента в <see cref="Segment"/>.</para>
        /// </summary>
        /// <param name="item"><para>The object to locate in the <see cref="Segment"/>.</para><para>Элемент для поиска в <see cref="Segment"/>.</para></param>
        /// <returns>
        /// <para>The index of <paramref name="item"/> if found in the list; otherwise, -1.</para>
        /// <para>Индекс <paramref name="item"/>, если он найден в списке; в противном случае — значение -1.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int IndexOf(T item)
        {
            var index = Base.IndexOf(item);
            if (index >= Offset)
            {
                var actualIndex = index - Offset;
                if (actualIndex < Length)
                {
                    return actualIndex;
                }
            }
            return -1;
        }

        /// <summary>
        /// <para>Inserts an item to the <see cref="Segment"/> at the specified index.</para>
        /// <para>Вставляет элемент в <see cref="Segment"/> по указанному индексу.</para>
        /// </summary>
        /// <param name="index"><para>The zero-based index at which <paramref name="item"/> should be inserted.</para><para>Отсчитываемый от нуля индекс, по которому следует вставить элемент <paramref name="item"/>.</para></param>
        /// <param name="item"><para>The element to insert into the <see cref="Segment"/>.</para><para>Элемент, вставляемый в <see cref="Segment"/>.</para></param>
        /// <exception cref="NotSupportedException">
        /// <para>The <see cref="Segment"/> is read-only.</para>
        /// <para><see cref="Segment"/> доступен только для чтения.</para>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Insert(int index, T item) => throw new NotSupportedException();

        /// <summary>
        /// <para>Removes the <see cref="Segment"/> item at the specified index.</para>
        /// <para>Удаляет элемент <see cref="Segment"/> по указанному индексу.</para>
        /// </summary>
        /// <param name="index"><para>The zero-based index of the item to remove.</para><para>Отсчитываемый от нуля индекс элемента для удаления.</para></param>
        /// <exception cref="NotSupportedException">
        /// <para>The <see cref="Segment"/> is read-only.</para>
        /// <para><see cref="Segment"/> доступен только для чтения.</para>
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAt(int index) => throw new NotSupportedException();

        /// <summary>
        /// <para>Adds an item to the <see cref="Segment"/>.</para>
        /// <para>Добавляет элемент в сегмент <see cref="Segment"/>.</para>
        /// </summary>
        /// <param name="item"><para>The element to add to the <see cref="Segment"/>.</para><para>Элемент, добавляемый в <see cref="Segment"/>.</para></param>
        /// <exception cref="NotSupportedException">
        /// <para>The <see cref="Segment"/> is read-only.</para>
        /// <para><see cref="Segment"/> доступен только для чтения.</para>
        /// </exception> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T item) => throw new NotSupportedException();

        /// <summary>
        /// <para>Removes all items from the <see cref="Segment"/>.</para>
        /// <para>Удаляет все элементы из <see cref="Segment"/>.</para>
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// <para>The <see cref="Segment"/> is read-only.</para>
        /// <para><see cref="Segment"/> доступен только для чтения.</para>
        /// </exception>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear() => throw new NotSupportedException();

        /// <summary>
        /// <para>Determines whether the <see cref="Segment"/> contains a specific value.</para>
        /// <para>Определяет, содержит ли <see cref="Segment"/> определенное значение.</para>
        /// </summary>
        /// <param name="item"><para>The value to locate in the <see cref="Segment"/>.</para><para>Значение, которое нужно разместить в списке <see cref="Segment"/>.</para></param>
        /// <returns>
        /// <para><see langword="true"/> if the value is found in the <see cref="Segment"/>; otherwise, <see langword="false"/>.</para>
        /// <para>Значение <see langword="true"/>, если значение находится в <see cref="Segment"/>; в противном случае - <see langword="false"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item) => IndexOf(item) >= 0;

        /// <summary>
        /// <para>Copies the elements of the <see cref="Segment"/> into an array, starting at a specific array index.</para>
        /// <para>Копирует элементы <see cref="Segment"/> в массив, начиная с определенного индекса массива.</para>
        /// </summary>
        /// <param name="array"><para>A one-dimensional array that is the destination of the elements copied from <see cref="Segment"/></para><para>Одномерный массив, который является местом назначения элементов, скопированных из <see cref="Segment"/>.</para></param>
        /// <param name="arrayIndex"><para>The zero-based index in <paramref name="array"/> at which copying begins.</para><para>Отсчитываемый от нуля индекс в массиве <paramref name="array"/>, с которого начинается копирование.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void CopyTo(T[] array, int arrayIndex)
        {
            for (var i = 0; i < Length; i++)
            {
                array.Add(ref arrayIndex, this[i]);
            }
        }

        /// <summary>
        /// <para>Removes the first occurrence of a specific value from the <see cref="Segment"/>.</para>
        /// <para>Удаляет первое вхождение указанного значения из <see cref="Segment"/>.</para>
        /// </summary>
        /// <param name="item"><para>The value to remove from the <see cref="Segment"/>.</para><para>Значение, которые нужно удалить из <see cref="Segment"/>.</para></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException">
        /// <para>The <see cref="Segment"/> is read-only.</para>
        /// <para><see cref="Segment"/> доступен только для чтения.</para>
        /// </exception>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(T item) => throw new NotSupportedException();

        /// <summary>
        /// <para>Returns an enumerator that iterates through a <see cref="Segment"/>.</para>
        /// <para>Возвращает перечислитель, который осуществляет итерацию по <see cref="Segment"/>.</para>
        /// </summary>
        /// <returns>
        /// <para>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the the <see cref="Segment"/>.</para>
        /// <para>Объект <see cref="T:System.Collections.IEnumerator"/>, который можно использовать для перебора <see cref="Segment"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Length; i++)
            {
                yield return this[i];
            }
        }

        /// <summary>
        /// <para>Gets an enumerator that iterates through a <see cref="Segment"/>.</para>
        /// <para>Возвращает перечислитель, который осуществляет итерацию по <see cref="Segment"/>.</para>
        /// </summary>
        /// <returns>
        /// <para>An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.</para>
        /// <para>Объект <see cref="T:System.Collections.IEnumerator"/>, который можно использовать для перебора <see cref="Segment"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
