using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Lists
{
    public static class IListExtensions
    {
        /// <summary>
        /// <para>Gets the element from specified index if the list is not null and the index is within the list's boundaries, otherwise it returns default value of type T.</para>
        /// <para>Получает элемент из указанного индекса, если список не является null и индекс находится в границах списка, в противном случае он возвращает значение по умолчанию типа T.</para>
        /// </summary>
        /// <param name="list"><para>The checked list.</para><para>Проверяемый список.</para></param>
        /// <param name="index"><para>The index of element.</para><para>Индекс элемента.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</typeparam>
        /// <returns>
        /// <para>If the list is not null and the index is within list's boundaries.</para>
        /// <para>Если значение верно - list[index], иначе же значение по умолчанию.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetElementOrDefault<T>(this IList<T> list, int index) => list != null && list.Count > index ? list[index] : default;

        /// <summary>
        /// <para>Checks if a list is passed, checks its length, and if successful, copies the value of list [index] into the variable element. Otherwise, the element variable has a default value.</para>
        /// <para>Проверяет, передан ли список, сверяет его длинy и в случае успеха копирует значение list[index] в переменную element. Иначе переменная element имеет значение по умолчанию.</para>
        /// </summary>
        /// <param name="list"><para>The checked list.</para><para>Список для проверки.</para></param>
        /// <param name="index"><para>The index of element..</para><para>Индекс элемента.</para></param>
        /// <param name="element"><para>Variable for passing the index value.</para><para>Переменная для передачи значения индекса.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
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
        /// <param name="list"><para>The list to add the value to.</para><para>Список в который нужно добавить значение.</para></param>
        /// <param name="element"><para>The item to add to the list.</para><para>Элемент который нужно добавить в список</para></param>
        /// <typeparam name="T"><para>The list's item type..</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>Returns true anyway.</para>
        /// <para>В любом случае возвращает true.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAndReturnTrue<T>(this IList<T> list, T element)
        {
            list.Add(element);
            return true;
        }

        /// <summary>
        /// <para>Adds a value to the list at the first index.</para>
        /// <para>Добавляет значение в список по первому индексу.</para>
        /// </summary>
        /// <param name="list"><para>The list to add the value to.</para><para>Список в который нужно добавить значение.</para></param>
        /// <param name="elements"><para>The item to add to the list.</para><para>Элемент который нужно добавить в список</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>Returns true anyway.</para>
        /// <para>В любом случае возвращает true.</para>
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
        /// <param name="list"><para>The list to add the value to.</para><para>Список в который нужно добавить значение.</para></param>
        /// <param name="elements"><para>The item to add to the list.</para><para>Элемент который нужно добавить в список</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddFirst<T>(this IList<T> list, IList<T> elements) => list.Add(elements[0]);

        /// <summary>
        /// <para>Adds a list of values to the  variable list.</para>
        /// <para>Добавляет cписок значений в переменную list.</para>
        /// </summary>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>Returns true anyway.</para>
        /// <para>В любом случае возвращает true.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddAllAndReturnTrue<T>(this IList<T> list, IList<T> elements)
        {
            list.AddAll(elements);
            return true;
        }

        /// <summary>
        /// <para>Adds a list of values to the  variable list.</para>
        /// <para>Добавляет cписок значений в переменную list.</para>
        /// </summary>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAll<T>(this IList<T> list, IList<T> elements)
        {
            for (var i = 0; i < elements.Count; i++)
            {
                list.Add(elements[i]);
            }
        }
    
        /// <summary>
        /// <para>Adds a list of values, skipping the first index.</para>
        /// <para>Добавляет список значений пропуская первый индекс.</para>
        /// </summary>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>Returns true anyway.</para>
        /// <para>В любом случае возвращает true.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddSkipFirstAndReturnTrue<T>(this IList<T> list, IList<T> elements)
        {
            list.AddSkipFirst(elements);
            return true;
        }

        /// <summary>
        /// <para>Adds a list of values, skipping the first index.</para>
        /// <para>Добавляет список значений пропуская первый индекс.</para>
        /// </summary>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <typeparam name="T"><para>List item types.</para><para>Тип элементов списка.</para></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSkipFirst<T>(this IList<T> list, IList<T> elements) => list.AddSkipFirst(elements, 1);

        /// <summary>
        /// <para>Adds a list of values skipping a specific index.</para>
        /// <para>Добавляет список значений пропуская определенный индекс</para>
        /// </summary>
        /// <param name="list"><para>The list to add the values to.</para><para>Список в который нужно добавить значения.</para></param>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <param name="skip"><para>Number of indexes to skip.</para><para>Количество пропускаемых индексов.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
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
        /// <param name="list"><para>The checked list.</para><para>Список для проверки.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>The number of items contained in the list, or 0.</para>
        /// <para>Число элементов содержащихся в списке, или же 0.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetCountOrZero<T>(this IList<T> list) => list?.Count ?? 0;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualTo<T>(this IList<T> left, IList<T> right) => EqualTo(left, right, ContentEqualTo);

        /// <summary>
        /// <para>Compares two lists for their identity.</para>
        /// <para>Сравниваются два списка на идентичность.</para>
        /// </summary>
        /// <param name="left"><para>The checked list.</para><para>Список для проверки.</para></param>
        /// <param name="right"><para>The checked list.</para><para>Список для проверки.</para></param>
        /// <param name="contentEqualityComparer"></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>If the passed lists are identical to each other, true is returned, оtherwise false.</para>
        /// <para>Если передаваемые списки идентичны друг другу, возвращается true, иначе же false.</para>
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
        /// <para>Сравнивает на идентичность каждый элемент списка.</para>
        /// </summary>
        /// <param name="left"><para>The checked list.</para><para>Список для проверки.</para></param>
        /// <param name="right"><para>The checked list.</para><para>Список для проверки.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>If every element of one list is not equal to every element of another list - return false, otherwise - true.</para>
        /// <para>Если каждый элемент одного списка не равен каждому элемента другого списка - return false, иначе - true. </para>
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
        /// <para>If the list is not empty, creates an array with and copies all elements of the list into it.</para>
        /// <para>Если список не пуст, создает массив и копирует в него все элементы списка.</para>
        /// </summary>
        /// <param name="list">Copy List.<para>Список для копирования.</para></param>
        /// <param name="predicate"><para></para><para></para></param>
        /// <typeparam name="T"><para></para><para></para></typeparam>
        /// <returns>
        /// <para></para>
        /// <para></para>
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
        /// <para>Копирует все элементы списка в массив и возвращает его</para>
        /// </summary>
        /// <param name="list"><para>Copy List.</para><para>Список для копирования.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
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
        /// <para>Uses the passed method on each item in the list.</para>
        /// <para>Использует переданный метод на каждый элемент списка.</para>
        /// </summary>
        /// <param name="list"><para>List of elements which will be used in action.</para><para>Список элементы которого будут использоваться в action.</para></param>
        /// <param name="action"><para>A function that will be called on each element of the list.</para><para>Функция которая будет вызываться на каждый элемент списка.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ForEach<T>(this IList<T> list, Action<T> action)
        {
            for (var i = 0; i < list.Count; i++)
            {
                action(list[i]);
            }
        }

        /// <summary>
        /// <para>Generates a hash code for each item in the list.</para>
        /// <para>Генерирует хеш-код каждого элемента списка.</para>
        /// </summary>
        /// <param name="list"><para>Hash list.</para><para>Список для хеширования.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>The hash code of each list item.</para>
        /// <para>Хеш-код каждого элемента списка.</para>
        /// </returns>
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] SkipFirst<T>(this IList<T> list) => list.SkipFirst(1);
    
        /// <summary>
        /// <para>Skips the specified number of elements in the list and builds an array from the remaining elements.</para>
        /// <para>Пропускает указанное количество элементов списка и составляет из оставшихся элементов массив.</para>
        /// </summary>
        /// <param name="list"><para>Copy List.</para><para>Список для копирования.</para></param>
        /// <param name="skip"><para>Number of items to skip.</para><para>Количество пропускаемых элементов.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>If the list is empty, or the number of skipped elements is greater than the list, it returns an empty array, otherwise - an array with the specified number of missing elements.</para>
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> ShiftRight<T>(this IList<T> list) => list.ShiftRight(1);

        /// <summary>
        /// <para>Shifts all elements of the list to the right by the specified number of elements and returns an array.</para>
        /// <para>Сдвигает вправо все элементы списка на указанное количество элементов и возвращает массив.</para>
        /// </summary>
        /// <param name="list"><para>Copy List.</para><para>Список для копирования.</para></param>
        /// <param name="skip"><para>Number of items to shift.</para><para>Количество сдвигаемых элементов.</para></param>
        /// <typeparam name="T"><para>The list's item type.</para><para>Тип элементов списка.</para></typeparam>
        /// <returns>
        /// <para>If the variable shift is less than zero - an error is returned, but if the variable shift is 0 an empty array is returned. Otherwise, an array is returned with the shift of the elements.</para>
        /// <para>Если переменная shift меньше нуля - возвращается ошибка, если же переменная shift равена 0 возвращается пустой массив. Иначе возвращается массив с сдвигом элементов.</para>
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
