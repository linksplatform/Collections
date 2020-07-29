using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Arrays
{
    public static class GenericArrayExtensions
    {
        /// <summary>
        /// <para>Checks if an array exists, if so,  checks the array length using the  index variable type int, and if the array length is greater than the index - return array[index], otherwise - default value.</para>
        /// <para>Проверяет, существует ли массив, если да - идет проверка длины массива с помощью переменной index, и если длина массива больше индекса - возвращает array[index], иначе - значение по умолчанию.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>Array that will participate in verification.</para><para>Массив который будет учавствовать в проверке.</para></param>
        /// <param name="index"><para>Number type int to compare.</para><para>Число типа int для сравнения.</para></param>
        /// <returns><para>Array element or default value.</para><para>Элемент массива или же значение по умолчанию.</para></returns>        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetElementOrDefault<T>(this T[] array, int index) => array != null && array.Length > index ? array[index] : default;
        
        /// <summary>
        /// <para>Сhecks whether the array exists, if so, checks the array length using the  index variable type long, and if the array length is greater than the index - return array[index], otherwise - default value.</para>
        /// <para>Проверяет, существует ли массив, если да - идет проверка длины массива с помощью переменной index, и если длина массива больше индекса - возвращает array[index], иначе - значение по умолчанию.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>Array that will participate in verification.</para><para>Массив который будет учавствовать в проверке.</para></param>
        /// <param name="index"><para>Number type long to compare.</para><para>Число типа long для сравнения.</para></param>
        /// <returns><para>Array element or default value.</para><para>Элемент массива или же значение по умолчанию.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetElementOrDefault<T>(this T[] array, long index) => array != null && array.LongLength > index ? array[index] : default;

        /// <summary>
        /// <para>Checks whether the array exist, if so, checks the array length using the index varible type int, and if the array length is greater than the index, set the element variable to array[index] and return true.</para>
        /// <para>Проверяет, существует ли массив, если да, то  идет проверка  длины массива с помощью переменной index  типа int, и если длина массива больше значения index, устанавливает значение переменной element - array[index] и возвращает true.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>Array that will participate in verification.</para><para>Массив который будет учавствовать в проверке.</para></param>
        /// <param name="index"><para>Number type int to compare.</para><para>Число типа int для сравнения.</para></param>
        /// <param name="element"><para>Passing the argument by reference, if successful, it will take the value array[index] otherwise default value.</para><para>Передает аргумент по ссылке, в случае успеха он примет значение array[index] в противном случае значение по умолчанию.</para></param>
        /// <returns><para>True if successful otherwise false.</para><para>True в случае успеха, в противном случае false</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetElement<T>(this T[] array, int index, out T element)
        {
            if (array != null && array.Length > index)
            {
                element = array[index];
                return true;
            }
            else
            {
                element = default;
                return false;
            }
        }
        
        /// <summary>  
        /// <para>Checks whether the array exist, if so,  checks the array length using the index varible type long, and if the array length is greater than the index,  set the element variable to array[index] and return true.</para>
        /// <para>Проверяет, существует ли массив, если да, то идет проверка длины массива с помощью переменной index  типа long, и если длина массива больше значения index, устанавливает значение переменной element - array[index] и возвращает true.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>Array that will participate in verification.</para><para>Массив который будет учавствовать в проверке.</para></param>
        /// <param name="index"><para>Number type long to compare.</para><para>Число типа long для сравнения.</para></param>
        /// <param name="element"><para>Passing the argument by reference, if successful, it will take the value array[index] otherwise default value.</para><para>Передает аргумент по ссылке, в случае успеха он примет значение array[index] в противном случае значение по умолчанию.</para></param>
        /// <returns><para>True if successful otherwise false.</para><para>True в случае успеха, в противном случае false</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetElement<T>(this T[] array, long index, out T element)
        {
            if (array != null && array.LongLength > index)
            {
                element = array[index];
                return true;
            }
            else
            {
                element = default;
                return false;
            }
        }

        /// <summary>
        /// <para>Copying  of elements from one array to another array.</para>
        /// <para>Копирует  элементы из одного массива в другой массив.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>The array  to copy.</para><para>Массив который необходимо скопировать.</para></param>
        /// <returns><para>Copy of the array.</para><para>Копию массива.</para></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Clone<T>(this T[] array)
        {
            var copy = new T[array.LongLength];
            Array.Copy(array, 0L, copy, 0L, array.LongLength);
            return copy;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> ShiftRight<T>(this T[] array) => array.ShiftRight(1L);
        
        /// <summary>
        /// <para>Extending the array boundaries to shift elements and then copying it, but with the condition that shift > 0. If shift = = 0, the extension will not occur, but cloning will still be applied. If shift < 0, a NotImplementedException is thrown.</para>
        /// <para>Расширение границ массива на shift элементов и последующее его копирование, но с условием что shift > 0. Если же shift == 0 - расширение не произойдет , но клонирование все равно применится. Если shift < 0, выбросится исключение NotImplementedException.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>Array to expand  Elements.</para><para>Массив для расширения элементов.</para></param>
        /// <param name="shift"><para>The number to expand the array</para><para>Число на которое необходимо рассширить массив.</para></param>
        /// <returns>
        /// <para>If the value of the shift variable is < 0, it returns a NotImplementedException exception. If shift = = 0, the array is cloned, but the extension will not be applied. Otherwise, if the value shift > 0, the length of the array is increased by the shift elements and the array is cloned.</para>
        /// <para>Если значение переменной shift < 0,  возвращается исключение NotImplementedException. Если shift = = 0, то массив клонируется, но расширение не применяется. В противном случае, если значение shift > 0, длина массива увеличивается на shift элементов  и массив клонируется.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> ShiftRight<T>(this T[] array, long shift)
        {
            if (shift < 0)
            {
                throw new NotImplementedException();
            }
            if (shift == 0)
            {
                return array.Clone<T>();
            }
            else
            {
                var restrictions = new T[array.LongLength + shift];
                Array.Copy(array, 0L, restrictions, shift, array.LongLength);
                return restrictions;
            }
        }

        /// <summary>
        /// <para>One of the array values with index on variable position++ type int is passed to the element variable.</para>
        /// <para>Одно из значений массива с индексом переменной position++ типа int назначается в переменную element.</para>
        /// </summary>
        /// <param name="array"><para>An array whose specific value will be assigned to the element variable.</para><para>Массив, определенное значений которого присваивается переменной element</para></param>
        /// <param name="position"><para>Reference to a position in an array of int type.</para><para>Ссылка на позицию в массиве типа int.</para></param>
        /// <param name="element"><para>The variable which needs to be assigned a specific value from the array.</para><para>Переменная, которой нужно присвоить определенное значение из массива.</para></param>
        /// <typeparam name="T"><para>Array elements type.</para>Тип элементов массива.<para></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this T[] array, ref int position, T element) => array[position++] = element;

        /// <summary>
        /// <para>One of the array values with index on variable position++ type long is passed to the element variable.</para>
        /// <para>Одно из значений массива с индексом переменной possition++ типа long назначается в переменную element.</para>
        /// </summary>
        /// <param name="array"><para>An array whose specific value will be assigned to the element variable.</para><para>Массив, определенное значений которого присваивается переменной element</para></param>
        /// <param name="position"><para>Reference to a position in an array of long type.</para><para>Ссылка на позицию в массиве типа long.</para></param>
        /// <param name="element"><para>The variable which needs to be assigned a specific value from the array.</para><para>Переменная, которой нужно присвоить определенное значение из массива.</para></param>
        /// <typeparam name="T"><para>Array elements type.</para>Тип элементов массива.<para></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this T[] array, ref long position, T element) => array[position++] = element;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TReturnConstant AddAndReturnConstant<TElement, TReturnConstant>(this TElement[] array, ref long position, TElement element, TReturnConstant returnConstant)
        {
            array.Add(ref position, element);
            return returnConstant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddFirst<T>(this T[] array, ref long position, IList<T> elements) => array[position++] = elements[0];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TReturnConstant AddFirstAndReturnConstant<TElement, TReturnConstant>(this TElement[] array, ref long position, IList<TElement> elements, TReturnConstant returnConstant)
        {
            array.AddFirst(ref position, elements);
            return returnConstant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TReturnConstant AddAllAndReturnConstant<TElement, TReturnConstant>(this TElement[] array, ref long position, IList<TElement> elements, TReturnConstant returnConstant)
        {
            array.AddAll(ref position, elements);
            return returnConstant;
        }

        /// <summary>
        /// <para>Adding a collection of elements starting from a specific position.</para>
        /// <para>Добавляет коллекции элементов начиная с определенной позиции.</para>
        /// </summary>
        /// <param name="array"><para>An array to which the collection of elements will be added.</para><para>Массив в который будет добавлена коллекция элементов.</para></param>
        /// <param name="position"><para>The position from which to start adding elements.</para><para>Позиция с которой начнется добавление элементов.</para></param>
        /// <param name="elements"><para>Added all collection of elements to array.</para><para>Добавляется вся коллекция элементов в массив. </para></param>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAll<T>(this T[] array, ref long position, IList<T> elements)
        {
            for (var i = 0; i < elements.Count; i++)
            {
                array.Add(ref position, elements[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TReturnConstant AddSkipFirstAndReturnConstant<TElement, TReturnConstant>(this TElement[] array, ref long position, IList<TElement> elements, TReturnConstant returnConstant)
        {
            array.AddSkipFirst(ref position, elements);
            return returnConstant;
        }
        
        /// <summary>
        /// <para>Adds all elements except the first.</para>
        /// <para>Добавляет все элементы, кроме первого.</para>
        /// </summary>
        /// <param name="array"><para>An array to which the collection of elements will be added.</para><para>Массив в который будет добавлена коллекция элементов.</para></param>
        /// <param name="position"><para>The position from which to start adding elements.</para><para>Позиция, с которой начинается добавление элементов.</para></param>
        /// <param name="elements"><para>List of added elements.</para><para>Список добавляемых элементов.</para></param>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>  
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSkipFirst<T>(this T[] array, ref long position, IList<T> elements) => array.AddSkipFirst(ref position, elements, 1);
        
        /// <summary>
        /// <para>Adds all but the first element, skipping a specified number of elements.</para>
        /// <para>Добавляет все элементы, кроме первого, пропуская определенное количество элементов.</para>
        /// </summary>
        /// <param name="array"><para>An array to which the collection of elements will be added.</para><para>Массив в который будет добавлена коллекция элементов.</para></param>
        /// <param name="position"><para>The position from which to start adding elements.</para><para>Позиция, с которой начинается добавление элементов.</para></param>
        /// <param name="elements"><para>List of added elements.</para><para>Список добавляемых элементов.</para></param>
        /// <param name="skip"></param>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSkipFirst<T>(this T[] array, ref long position, IList<T> elements, int skip)
        {
            for (var i = skip; i < elements.Count; i++)
            {
                array.Add(ref position, elements[i]);
            }
        }
    }
}
