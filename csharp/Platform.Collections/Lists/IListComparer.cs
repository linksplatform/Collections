using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Lists
{
    public class IListComparer<T> : IComparer<IList<T>>
    {
        /// <summary>
        /// <para>Compares each list item to each other.</para>
        /// <para>Сравнивает каждый элемент списка друг с другом.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="left"><para>The first checked list.</para><para>Первый список для сравнения.</para></param>
        /// <param name="right"><para>The second checked list.</para><para>Второй список для сравнения.</para></param>
        /// <returns>
        /// <para>
        ///     A signed integer that indicates the relative values of <paramref name="left" /> and <paramref name="right" /> lists' elements, as shown in the following table. 
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Value</term>
        ///             <description>Meaning</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Is less than zero</term>
        ///             <description>First non equal element of <paramref name="left" /> list is less than first not equal element of <paramref name="right" /> list.</description>
        ///         </item>
        ///         <item>
        ///             <term>Zero</term>
        ///             <description>All elements of <paramref name="left" /> list equals to all elements of <paramref name="right" /> list.</description>
        ///         </item>
        ///         <item>
        ///             <term>Is greater than zero</term>
        ///             <description>First non equal element of <paramref name="left" /> list is greater than first not equal element of <paramref name="right" /> list.</description>
        ///         </item>
        ///     </list>
        /// <para>
        /// <para>
        ///     Целое число со знаком, которое указывает относительные значения элементов списков <paramref name="left" /> и <paramref name="right" /> как показано в следующей таблице.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Значение</term>
        ///             <description>Имеется в виду</description>
        ///         </listheader>
        ///         <item>
        ///             <term>Меньше нуля</term>
        ///             <description>Первый не равный элемент <paramref name="left" /> списка меньше первого неравного элемента <paramref name="right" /> списка.</description>
        ///         </item>
        ///         <item>
        ///             <term>Ноль</term>
        ///             <description>Все элементы <paramref name="left" /> списка равны всем элементам <paramref name="right" /> списка.</description>
        ///         </item>
        ///         <item>
        ///             <term>Больше нуля</term>
        ///             <description>Первый не равный элемент <paramref name="left" /> списка больше первого неравного элемента <paramref name="right" /> списка.</description>
        ///         </item>
        ///     </list>
        /// </para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(IList<T> left, IList<T> right) => left.CompareTo(right);
    }
}
