using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 

namespace Platform.Collections.Lists
{
    public class IListEqualityComparer<T> : IEqualityComparer<IList<T>>
    {
        /// <summary>
        /// <para>Compares two lists for equality.</para>
        /// <para>Сравнивает два списка на равенство.</para>
        /// </summary>
        /// <param name="left"><para>The first compared list.</para><para>Первый список для сравнения.</para></param>
        /// <param name="right"><para>The second compared list.</para><para>Второй список для сравнения.</para></param>
        /// <returns>
        /// <para>If the passed lists are equal to each other, true is returned, оtherwise false.</para>
        /// <para>Если переданные списки равны друг другу, возвращается true, иначе же false.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IList<T> left, IList<T> right) => left.EqualTo(right);

        /// <summary>
        /// <para>Generates a hash code for the entire list based on the values of its elements.</para>
        /// <para>Генерирует хэш-код всего списка, на основе значений его элементов.</para>
        /// </summary>
        /// <param name="list"><para>Hash list.</para><para>Список для хеширования.</para></param>
        /// <returns>
        /// <para>The hash code of the list.</para>
        /// <para>Хэш-код списка.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(IList<T> list) => list.GenerateHashCode();
    }
}
