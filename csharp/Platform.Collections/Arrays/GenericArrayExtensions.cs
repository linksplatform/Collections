using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

        /// <summary>
        /// <para>Shifts all the elements of the array by one position to the right.</para>
        /// <para>Сдвигает вправо все элементы массива на одну позицию.</para>
        /// </summary>
        /// <typeparam name="T"><para>The array item type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>The array to copy from.</para><para>Массив для копирования.</para></param>
        /// <returns>
        /// <para>Array with a shift of elements by one position.</para>
        /// <para>Массив со сдвигом элементов на одну позицию.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> ShiftRight<T>(this T[] array) => array.ShiftRight(1L);
        
        /// <summary>
        /// <para>Shifts all elements of the array to the right by the specified number of elements.</para>
        /// <para>Сдвигает вправо все элементы массива на указанное количество элементов.</para>
        /// </summary>
        /// <typeparam name="T"><para>The array item type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>The array to copy from.</para><para>Массив для копирования.</para></param>
        /// <param name="skip"><para>The number of items to shift.</para><para>Количество сдвигаемых элементов.</para></param>        
        /// <returns>
        /// <para>If the value of the shift variable is less than zero - an <see cref="NotImplementedException"/> exception is thrown, but if the value of the shift variable is 0 - an exact copy of the array is returned. Otherwise, an array is returned with the shift of the elements.</para>
        /// <para>Если значение переменной shift меньше нуля - выбрасывается исключение <see cref="NotImplementedException"/>, если же значение переменной shift равно 0 - возвращается точная копия массива. Иначе возвращается массив со сдвигом элементов.</para>
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
        /// <para>Adding in array the passed element at the specified position and increments position value by one.</para>
        /// <para>Добавляет в массив переданный элемент на указанную позицию и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para>Тип элементов массива.<para></typeparam>
        /// <param name="array"><para>The array to add the element to.</para><para>Массив в который необходимо добавить элемент.</para></param>
        /// <param name="position"><para>A reference to the position of type int where the element will be added.</para><para>Ссылка на позицию типа int, в которую будет добавлен элемент.</para></param>
        /// <param name="element"><para>The element to add to the array.</para><para>Элемент, который нужно добавить в массив.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this T[] array, ref int position, T element) => array[position++] = element;

        /// <summary>
        /// <para>Adding in array the passed element at the specified position and increments position value by one.</para>
        /// <para>Добавляет в массив переданный элемент на указанную позицию и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para>Тип элементов массива.<para></typeparam>
        /// <param name="array"><para>The array to add the element to.</para><para>Массив в который необходимо добавить элемент.</para></param>
        /// <param name="position"><para>A reference to the position of type long where the element will be added.</para><para>Ссылка на позицию типа long, в которую будет добавлен элемент.</para></param>
        /// <param name="element"><para>The element to add to the array</para><para>Элемент который необходимо добавить в массив.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this T[] array, ref long position, T element) => array[position++] = element;

        /// <summary>
        /// <para>Adding in array the passed element, at the specified position and increments position value by one.</para>
        /// <para>Добавляет в массив переданный элемент на указанную позицию и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="TElement"><para>The array element type.</para><para>Тип элемента массива.</para></typeparam>
        /// <typeparam name="TReturnConstant"><para>Type of return constant.</para><para>Тип возвращаемой константы.</para></typeparam>
        /// <param name="array"><para>The array to add the element to.</para><para>Массив в который необходимо добавить элемент.</para></param>
        /// <param name="position"><para>Reference to the position to which the element will be added.</para><para>Ссылка на позицию, в которую будет добавлен элемент.</para></param>
        /// <param name="element"><para>The element to add to the array.</para><para>Элемент который необходимо добавить в массив.</para></param>
        /// <param name="returnConstant"><para>The constant value that will be returned.</para><para>Значение константы, которое будет возвращено.</para></param>
        /// <returns>
        /// <para>The constant value passed as an argument.</para>
        /// <para>Значение константы, переданное в качестве аргумента.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TReturnConstant AddAndReturnConstant<TElement, TReturnConstant>(this TElement[] array, ref long position, TElement element, TReturnConstant returnConstant)
        {
            array.Add(ref position, element);
            return returnConstant;
        }

        /// <summary>
        /// <para>Adds the first element from the passed collection to the array, at the specified position and increments position value by one.</para>
        /// <para>Добавляет в массив первый элемент из переданной коллекции, на указанную позицию и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array element type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>The array to add the element to.</para><para>Массив в который необходимо добавить элемент.</para></param>
        /// <param name="position"><para>Reference to the position to which the element will be added.</para><para>Ссылка на позицию, в которую будет добавлен элемент.</para></param>
        /// <param name="elements"><para>List, the first element of which will be added to the array.</para><para>Список, первый элемент которого будет добавлен в массив.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddFirst<T>(this T[] array, ref long position, IList<T> elements) => array[position++] = elements[0];

        /// <summary>
        /// <para>Adds the first element from the passed collection to the array, at the specified position and increments position value by one.</para>
        /// <para>Добавляет в массив первый элемент из переданной коллекции, на указанную позицию и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="TElement"><para>The array element type.</para><para>Тип элемента массива.</para></typeparam>
        /// <typeparam name="TReturnConstant"><para>Type of return constant.</para><para>Тип возвращаемой константы.</para></typeparam>
        /// <param name="array"><para>The array to add the element to.</para><para>Массив в который необходимо добавить элемент.</para></param>
        /// <param name="position"><para>Reference to the position to which the element will be added.</para><para>Ссылка на позицию, в которую будет добавлен элемент.</para></param>
        /// <param name="elements"><para>List, the first element of which will be added to the array.</para><para>Список, первый элемент которого будет добавлен в массив.</para></param>
        /// <param name="returnConstant"><para>The constant value that will be returned.</para><para>Значение константы, которое будет возвращено.</para></param>
        /// <returns>
        /// <para>The constant value passed as an argument.</para>
        /// <para>Значение константы, переданное в качестве аргумента.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TReturnConstant AddFirstAndReturnConstant<TElement, TReturnConstant>(this TElement[] array, ref long position, IList<TElement> elements, TReturnConstant returnConstant)
        {
            array.AddFirst(ref position, elements);
            return returnConstant;
        }

        /// <summary>
        /// <para>Adding in array all elements from the passed collection, at the specified position and increments position value by one.</para>
        /// <para>Добавляет в массив все элементы из переданной коллекции, на указанную позицию и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="TElement"><para>The array element type.</para><para>Тип элемента массива.</para></typeparam>
        /// <typeparam name="TReturnConstant"><para>Type of return constant.</para><para>Тип возвращаемой константы.</para></typeparam>
        /// <param name="array"><para>The array to add the element to.</para><para>Массив в который необходимо добавить элементы.</para></param>
        /// <param name="position"><para>Reference to the position from which elements will be added to the array.</para><para>Ссылка на позицию, начиная с которой будут добавляться элементы в массив.</para></param>
        /// <param name="elements"><para>List, whose elements will be added to the array.</para><para>Список, элементы которого будут добавленны в массив.</para></param>
        /// <param name="returnConstant"><para>The constant value that will be returned.</para><para>Значение константы, которое будет возвращено.</para></param>
        /// <returns>
        /// <para>The constant value passed as an argument.</para>
        /// <para>Значение константы, переданное в качестве аргумента.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TReturnConstant AddAllAndReturnConstant<TElement, TReturnConstant>(this TElement[] array, ref long position, IList<TElement> elements, TReturnConstant returnConstant)
        {
            array.AddAll(ref position, elements);
            return returnConstant;
        }

        /// <summary>
        /// <para>Adding in array a collection of elements, starting from a specific position and increments position value by one.</para>
        /// <para>Добавляет в массив все элементы коллекции, начиная с определенной позиции и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>The array to add the element to.</para><para>Массив в который необходимо добавить элементы.</para></param>
        /// <param name="position"><para>Reference to the position from which elements will be added to the array.</para><para>Ссылка на позицию, начиная с которой будут добавляться элементы в массив.</para></param>
        /// <param name="elements"><para>List, whose elements will be added to the array.</para><para>Список, элементы которого будут добавленны в массив.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAll<T>(this T[] array, ref long position, IList<T> elements)
        {
            for (var i = 0; i < elements.Count; i++)
            {
                array.Add(ref position, elements[i]);
            }
        }

        /// <summary>
        /// <para>Adding in array all elements of the collection, skipping the first position and increments position value by one.</para>
        /// <para>Добавляет в массив все элементы коллекции, пропуская первую позицию и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="TElement"><para>The array element type.</para><para>Тип элемента массива.</para></typeparam>
        /// <typeparam name="TReturnConstant"><para>Type of return constant.</para><para>Тип возвращаемой константы.</para></typeparam>
        /// <param name="array"><para>The array to add items to.</para><para>Массив в который необходимо добавить элементы.</para></param>
        /// <param name="position"><para>Reference to the position from which to start adding elements.</para><para>Ссылка на позицию, с которой начинается добавление элементов.</para></param>
        /// <param name="elements"><para>List, whose elements will be added to the array.</para><para>Список, элементы которого будут добавленны в массив.</para></param>
        /// <param name="returnConstant"><para>The constant value that will be returned.</para><para>Значение константы, которое будет возвращено.</para></param>
        /// <returns>
        /// <para>The constant value passed as an argument.</para>
        /// <para>Значение константы, переданное в качестве аргумента.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TReturnConstant AddSkipFirstAndReturnConstant<TElement, TReturnConstant>(this TElement[] array, ref long position, IList<TElement> elements, TReturnConstant returnConstant)
        {
            array.AddSkipFirst(ref position, elements);
            return returnConstant;
        }
        
        /// <summary>
        /// <para>Adding in array all elements of the collection, skipping the first position and increments position value by one.</para>
        /// <para>Добавляет в массив все элементы коллекции, пропуская первую позицию и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam> 
        /// <param name="array"><para>The array to add items to.</para><para>Массив в который необходимо добавить элементы.</para></param>
        /// <param name="position"><para>Reference to the position from which to start adding elements.</para><para>Ссылка на позицию, с которой начинается добавление элементов.</para></param>
        /// <param name="elements"><para>List, whose elements will be added to the array.</para><para>Список, элементы которого будут добавленны в массив.</para></param> 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddSkipFirst<T>(this T[] array, ref long position, IList<T> elements) => array.AddSkipFirst(ref position, elements, 1);
        
        /// <summary>
        /// <para>Adding in array all but the first element, skipping a specified number of positions and increments position value by one.</para>
        /// <para>Добавляет в массив все элементы коллекции, кроме первого, пропуская определенное количество позиций и увеличивает значение position на единицу.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>The array to add items to.</para><para>Массив в который необходимо добавить элементы.</para></param>
        /// <param name="position"><para>Reference to the position from which to start adding elements.</para><para>Ссылка на позицию, с которой начинается добавление элементов.</para></param>
        /// <param name="elements"><para>List, whose elements will be added to the array.</para><para>Список, элементы которого будут добавленны в массив.</para></param>
        /// <param name="skip"><para>Number of elements to skip.</para><para>Количество пропускаемых элементов.</para></param>
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
