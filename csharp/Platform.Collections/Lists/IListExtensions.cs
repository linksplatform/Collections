using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Lists
{
    public static class IListExtensions
    {
        /// <summary>
        /// <para>Gets the element from specified index if the list is not null and the index is within the list's boundaries, otherwise it returns default value of type T.</para>
        /// <para>Получает элемент из указанного индекса, если список не является null и индекс находится в границах списка, в противном случае он возвращает значение по умолчанию типа T.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The checked list.</para><para>Проверяемый список.</para></param>
        /// <param name="index"><para>The index of element.</para><para>Индекс элемента.</para></param>
        /// <returns>
        /// <para>If the specified index is within list's boundaries, then - list[index], otherwise the default value.</para>
        /// <para>Если указанный индекс находится в пределах границ списка, тогда - list[index], иначе же значение по умолчанию.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetElementOrDefault<T>(this IList<T> list, int index) => list != null && list.Count > index ? list[index] : default;

        /// <summary>
        /// <para>Checks if a list is passed, checks its length, and if successful, copies the value of list [index] into the element variable. Otherwise, the element variable has a default value.</para>
        /// <para>Проверяет, передан ли список, сверяет его длинy и в случае успеха копирует значение list[index] в переменную element. Иначе переменная element имеет значение по умолчанию.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The checked list.</para><para>Список для проверки.</para></param>
        /// <param name="index"><para>The index of element..</para><para>Индекс элемента.</para></param>
        /// <param name="element"><para>Variable for passing the index value.</para><para>Переменная для передачи значения индекса.</para></param>
        /// <returns>
        /// <para>True on success, false otherwise.</para>
        /// <para>True в случае успеха, иначе false.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetElement<T>(this IList<T> list, int index, out T element)
        {
            if (list != null && list.Count > index)
            {
                element = list[index];
                return true;
            }
            else
            {
                element = default;
                return false;
            }
        }

        /// <summary>
        /// <para>Adds a value to the list.</para>
        /// <para>Добавляет значение в список.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to add the value to.</para><para>Список в который нужно добавить значение.</para></param>
        /// <param name="element"><para>The item to add to the list.</para><para>Элемент который нужно добавить в список.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAndReturnTrue<T>(this IList<T> list, T element)
        {
            list.Add(element);
            return true;
        }

        /// <summary>
        /// <para>Adds the value with first index from other list to this list.</para>
        /// <para>Добавляет в этот список значение с первым индексом из другого списка.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to add the value to.</para><para>Список в который нужно добавить значение.</para></param>
        /// <param name="elements"><para>The item to add to the list.</para><para>Элемент который нужно добавить в список</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddFirstAndReturnTrue<T>(this IList<T> list, IList<T> elements)
        {
            list.AddFirst(elements);
            return true;
        }

        /// <summary>
        /// <para>Adds a value to the list at the first index.</para>
        /// <para>Добавляет значение в список по первому индексу.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to add the value to.</para><para>Список в который нужно добавить значение.</para></param>
        /// <param name="elements"><para>The item to add to the list.</para><para>Элемент который нужно добавить в список</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddFirst<T>(this IList<T> list, IList<T> elements) => list.Add(elements[0]);

        /// <summary>
        /// <para>Adds all elements from other list to this list and returns true.</para>
        /// <para>Добавляет все элементы из другого списка в этот список и возвращает true.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAllAndReturnTrue<T>(this IList<T> list, IList<T> elements)
        {
            list.AddAll(elements);
            return true;
        }

        /// <summary>
        /// <para>Adds all elements from other list to this list.</para>
        /// <para>Добавляет все элементы из другого списка в этот список.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>The list of values to add.</para><para>Список значений которые необходимо добавить.</para></param>        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAll<T>(this IList<T> list, IList<T> elements)
        {
            for (var i = 0; i < elements.Count; i++)
            {
                list.Add(elements[i]);
            }
        }
    
        /// <summary>
        /// <para>Adds values to the list skipping the first element.</para>
        /// <para>Добавляет значения в список пропуская первый элемент.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>The list of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSkipFirstAndReturnTrue<T>(this IList<T> list, IList<T> elements)
        {
            list.AddSkipFirst(elements);
            return true;
        }

        /// <summary>
        /// <para>Adds values to the list skipping the first element.</para>
        /// <para>Добавляет значения в список пропуская первый элемент.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSkipFirst<T>(this IList<T> list, IList<T> elements) => list.AddSkipFirst(elements, 1);

        /// <summary>
        /// <para>Adds values to the list skipping a specified number of first elements.</para>
        /// <para>Добавляет в список значения пропуская определенное количество первых элементов.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <param name="skip"><para>Number of elements to skip.</para><para>Количество пропускаемых элементов.</para></param>        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSkipFirst<T>(this IList<T> list, IList<T> elements, int skip)
        {
            for (var i = skip; i < elements.Count; i++)
            {
                list.Add(elements[i]);
            }
        }

        /// <summary>
        /// <para>Reads the number of elements in the list.</para>
        /// <para>Считывает число элементов списка.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The checked list.</para><para>Список для проверки.</para></param>
        /// <returns>
        /// <para>The number of items contained in the list or 0.</para>
        /// <para>Число элементов содержащихся в списке или же 0.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetCountOrZero<T>(this IList<T> list) => list?.Count ?? 0;

        /// <summary>
        /// <para>Compares two lists for equality.</para>
        /// <para>Сравнивает два списка на равенство.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="left"><para>The first compared list.</para><para>Первый список для сравнения.</para></param>
        /// <param name="right"><para>The second compared list.</para><para>Второй список для сравнения.</para></param>
        /// <returns>
        /// <para>If the passed lists are equal to each other, true is returned, оtherwise false.</para>
        /// <para>Если переданные списки равны друг другу, возвращается true, иначе же false.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualTo<T>(this IList<T> left, IList<T> right) => EqualTo(left, right, ContentEqualTo);

        /// <summary>
        /// <para>Compares two lists for equality.</para>
        /// <para>Сравнивает два списка на равенство.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="left"><para>The first compared list.</para><para>Первый список для проверки.</para></param>
        /// <param name="right"><para>The second compared list.</para><para>Второй список для сравнения.</para></param>
        /// <param name="contentEqualityComparer"><para>Function to test two lists for their content equality.</para><para>Функция для проверки двух списков на равенство их содержимого.</para></param>
        /// <returns>
        /// <para>If the passed lists are equal to each other, true is returned, оtherwise false.</para>
        /// <para>Если переданные списки равны друг другу, возвращается true, иначе же false.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualTo<T>(this IList<T> left, IList<T> right, Func<IList<T>, IList<T>, bool> contentEqualityComparer)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            var leftCount = left.GetCountOrZero();
            var rightCount = right.GetCountOrZero();
            if (leftCount == 0 && rightCount == 0)
            {
                return true;
            }
            if (leftCount == 0 || rightCount == 0 || leftCount != rightCount)
            {
                return false;
            }
            return contentEqualityComparer(left, right);
        }

        /// <summary>
        /// <para>Compares each element in the list for identity.</para>
        /// <para>Сравнивает на равенство каждый элемент списка.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="left"><para>The first compared list.</para><para>Первый список для сравнения.</para></param>
        /// <param name="right"><para>The second compared list.</para><para>Второй список для сравнения.</para></param>
        /// <returns>
        /// <para>If at least one element of one list is not equal to the corresponding element from another list returns false, otherwise - true.</para>
        /// <para>Если как минимум один элемент одного списка не равен соответствующему элементу из другого списка возвращает false, иначе - true.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContentEqualTo<T>(this IList<T> left, IList<T> right)
        {
            var equalityComparer = EqualityComparer<T>.Default;
            for (var i = left.Count - 1; i >= 0; --i)
            {
                if (!equalityComparer.Equals(left[i], right[i]))
                {
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// <para>Creates an array by copying all elements from the list that satisfy the predicate. If no list is passed, null is returned.</para>
        /// <para>Создаёт массив, копируя из списка все элементы которые удовлетворяют предикату. Если список не передан, возвращается null.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list">The list to copy from.<para>Список для копирования.</para></param>
        /// <param name="predicate"><para>A function that determines whether an element should be copied.</para><para>Функция определяющая должен ли копироваться элемент.</para></param>       
        /// <returns>
        /// <para>An array with copied elements from the list.</para>
        /// <para>Массив с скопированными элементами из списка.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToArray<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if (list == null)
            {
                return null;
            }
            var result = new List<T>(list.Count);
            for (var i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    result.Add(list[i]);
                }
            }
            return result.ToArray();
        }

        /// <summary>
        /// <para>Copies all the elements of the list into an array and returns it.</para>
        /// <para>Копирует все элементы списка в массив и возвращает его.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to copy from.</para><para>Список для копирования.</para></param>
        /// <returns>
        /// <para>An array with all the elements of the passed list.</para>
        /// <para>Массив со всеми элементами переданного списка.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ToArray<T>(this IList<T> list)
        {
            var array = new T[list.Count];
            list.CopyTo(array, 0);
            return array;
        }
        
        /// <summary>
        /// <para>Executes the passed action for each item in the list.</para>
        /// <para>Выполняет переданное действие для каждого элемента в списке.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list of elements for which the action will be executed.</para><para>Список элементов для которых будет выполняться действие.</para></param>
        /// <param name="action"><para>A function that will be called for each element of the list.</para><para>Функция которая будет вызываться для каждого элемента списка.</para></param>        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            for (var i = 0; i < list.Count; i++)
            {
                action(list[i]);
            }
        }

        /// <summary>
        /// <para>Generates a hash code for the entire list based on the values of its elements.</para>
        /// <para>Генерирует хэш-код всего списка, на основе значений его элементов.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>Hash list.</para><para>Список для хеширования.</para></param>
        /// <returns>
        /// <para>The hash code of the list.</para>
        /// <para>Хэш-код списка.</para>
        /// </returns>
        /// <remarks>
        /// Based on http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenerateHashCode<T>(this IList<T> list)
        {
            var hashAccumulator = 17;
            for (var i = 0; i < list.Count; i++)
            {
                hashAccumulator = unchecked((hashAccumulator * 23) + list[i].GetHashCode());
            }
            return hashAccumulator;
        }

        /// <summary>
        /// <para>Compares two lists.</para>
        /// <para>Сравнивает два списка.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="left"><para>The first compared list.</para><para>Первый список для сравнения.</para></param>
        /// <param name="right"><para>The second compared list.</para><para>Второй список для сравнения.</para></param>
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
        /// </para>
        /// <para>
        ///     Целое число со знаком, которое указывает относительные значения элементов списков <paramref name="left" /> и <paramref name="right" /> как показано в следующей таблице.
        ///     <list type="table">
        ///         <listheader>
        ///             <term>Значение</term>
        ///             <description>Смысл</description>
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
        public static int CompareTo<T>(this IList<T> left, IList<T> right)
        {
            var comparer = Comparer<T>.Default;
            var leftCount = left.GetCountOrZero();
            var rightCount = right.GetCountOrZero();
            var intermediateResult = leftCount.CompareTo(rightCount);
            for (var i = 0; intermediateResult == 0 && i < leftCount; i++)
            {
                intermediateResult = comparer.Compare(left[i], right[i]);
            }
            return intermediateResult;
        }

        /// <summary>
        /// <para>Skips one element in the list and builds an array from the remaining elements.</para>
        /// <para>Пропускает один элемент списка и составляет из оставшихся элементов массив.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to copy from.</para><para>Список для копирования.</para></param> 
        /// <returns>
        /// <para>If the list is empty, returns an empty array, otherwise - an array with a missing first element.</para>
        /// <para>Если список пуст, возвращает пустой массив, иначе - массив с пропущенным первым элементом.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SkipFirst<T>(this IList<T> list) => list.SkipFirst(1);
    
        /// <summary>
        /// <para>Skips the specified number of elements in the list and builds an array from the remaining elements.</para>
        /// <para>Пропускает указанное количество элементов списка и составляет из оставшихся элементов массив.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to copy from.</para><para>Список для копирования.</para></param>
        /// <param name="skip"><para>The number of items to skip.</para><para>Количество пропускаемых элементов.</para></param>      
        /// <returns>
        /// <para>If the list is empty, or the number of skipped elements is greater than the list, returns an empty array, otherwise - an array with the specified number of missing elements.</para>
        /// <para>Если список пуст, или количество пропускаемых элементов больше списка - возвращает пустой массив, иначе - массив с указанным количеством пропущенных элементов.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SkipFirst<T>(this IList<T> list, int skip)
        {
            if (list.IsNullOrEmpty() || list.Count <= skip)
            {
                return Array.Empty<T>();
            }
            var result = new T[list.Count - skip];
            for (int r = skip, w = 0; r < list.Count; r++, w++)
            {
                result[w] = list[r];
            }
            return result;
        }

        /// <summary>
        /// <para>Shifts all the elements of the list by one position to the right.</para>
        /// <para>Сдвигает вправо все элементы списка на одну позицию.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to copy from.</para><para>Список для копирования.</para></param>
        /// <returns>
        /// <para>Array with a shift of elements by one position.</para>
        /// <para>Массив со сдвигом элементов на одну позицию.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> ShiftRight<T>(this IList<T> list) => list.ShiftRight(1);

        /// <summary>
        /// <para>Shifts all elements of the list to the right by the specified number of elements.</para>
        /// <para>Сдвигает вправо все элементы списка на указанное количество элементов.</para>
        /// </summary>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <param name="list"><para>The list to copy from.</para><para>Список для копирования.</para></param>
        /// <param name="shift"><para>The number of items to shift.</para><para>Количество сдвигаемых элементов.</para></param>        
        /// <returns>
        /// <para>If the value of the shift variable is less than zero - an <see cref="NotImplementedException"/> exception is thrown, but if the value of the shift variable is 0 - an exact copy of the array is returned. Otherwise, an array is returned with the shift of the elements.</para>
        /// <para>Если значение переменной shift меньше нуля - выбрасывается исключение <see cref="NotImplementedException"/>, если же значение переменной shift равно 0 - возвращается точная копия массива. Иначе возвращается массив со сдвигом элементов.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> ShiftRight<T>(this IList<T> list, int shift)
        {
            if (shift < 0)
            {
                throw new NotImplementedException();
            }
            if (shift == 0)
            {
                return list.ToArray();
            }
            else
            {
                var result = new T[list.Count + shift];
                for (int r = 0, w = shift; r < list.Count; r++, w++)
                {
                    result[w] = list[r];
                }
                return result;
            }
        }
    }
}
