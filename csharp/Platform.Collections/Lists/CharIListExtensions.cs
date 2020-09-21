using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 

namespace Platform.Collections.Lists
{
    public static class CharIListExtensions
    {
        /// <summary>
        /// <para>Generates a hash code for the entire list based on the values of its elements.</para>
        /// <para>Генерирует хэш-код всего списка, на основе значений его элементов.</para>
        /// </summary>
        /// <param name="list"><para>The list to be hashed.</para><para>Список для хеширования.</para></param>
        /// <returns>
        /// <para>The hash code of the list.</para>
        /// <para>Хэш-код списка.</para>
        /// </returns>
        /// <remarks>
        /// Based on https://github.com/Microsoft/referencesource/blob/3b1eaf5203992df69de44c783a3eda37d3d4cd10/mscorlib/system/string.cs#L833
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenerateHashCode(this IList<char> list)
        {
            var hashSeed = 5381;
            var hashAccumulator = hashSeed;
            for (var i = 0; i < list.Count; i++)
            {
                hashAccumulator = (hashAccumulator << 5) + hashAccumulator ^ list[i];
            }
            return hashAccumulator + (hashSeed * 1566083941);
        }

        /// <summary>
        /// <para>Compares two lists for equality.</para>
        /// <para>Сравнивает два списка на равенство.</para>
        /// </summary>
        /// <param name="left"><para>The first compared list.</para><para>Первый список для сравнения.</para></param>
        /// <param name="right"><para>The second compared list.</para><para>Второй список для сравнения.</para></param>
        /// <returns>
        /// <para>True, if the passed lists are equal to each other оtherwise false.</para>
        /// <para>True, если переданные списки равны друг другу, иначе false.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualTo(this IList<char> left, IList<char> right) => left.EqualTo(right, ContentEqualTo);

        /// <summary>
        /// <para>Compares each element in the list for equality.</para>
        /// <para>Сравнивает на равенство каждый элемент списка.</para>
        /// </summary>
        /// <param name="left"><para>The first compared list.</para><para>Первый список для сравнения.</para></param>
        /// <param name="right"><para>The second compared list.</para><para>Второй список для сравнения.</para></param>
        /// <returns>
        /// <para>If at least one element of one list is not equal to the corresponding element from another list returns false, otherwise - true.</para>
        /// <para>Если как минимум один элемент одного списка не равен соответствующему элементу из другого списка возвращает false, иначе - true.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContentEqualTo(this IList<char> left, IList<char> right)
        {
            for (var i = left.Count - 1; i >= 0; --i)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
