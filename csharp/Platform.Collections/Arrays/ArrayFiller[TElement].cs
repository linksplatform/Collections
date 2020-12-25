using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    /// <summary>
    /// <para>Provides a set of methods for populating the elements of the class <see cref="Array"/>.</para>
    /// <para>Предоставляет набор методов для заполнения элементов класса <see cref="Array"/>.</para>
    /// </summary>
    /// <typeparam name="TElement"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
    public class ArrayFiller<TElement>
    {
        protected readonly TElement[] _array;
        protected long _position;

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ArrayFiller"/> class, using the specified array as the array to fill and the number of items to offset.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="ArrayFiller"/>, используя указанный массив в качестве заполняемого и число смещаемых в нем элементов.</para>
        /// </summary>
        /// <param name="array"><para>The array to fill.</para><para>Массив для заполнения.</para></param>
        /// <param name="offset"><para>The number of elements to displace in the array.</para><para>Количество смещаемых элементов в массиве.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayFiller(TElement[] array, long offset)
        {
            _array = array;
            _position = offset;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ArrayFiller"/> class, using the specified array as the array to fill and the default number of items to offset.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="ArrayFiller"/>, используя указанный массив в качестве заполняемого и число смещаемых в нем элементов по умолчанию.</para>
        /// </summary>
        /// <param name="array"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayFiller(TElement[] array) : this(array, 0) { }

        /// <summary>
        /// <para>Adds an item to the end of the array.</para>
        /// <para>Добавляет элемент в конец массива.</para>
        /// </summary>
        /// <param name="element"><para>Element to add.</para><para>Добавляемый элемент.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(TElement element) => _array[_position++] = element;

        /// <summary>
        /// <para>Adds an item to the end of the array and return true.</para>
        /// <para>Добавляет элемент в конец массива и возвращает true.</para>
        /// </summary>
        /// <param name="element"><para>Element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddAndReturnTrue(TElement element) => _array.AddAndReturnConstant(ref _position, element, true);

        /// <summary>
        /// <para>Adds a value to the array at the first index and return true.</para>
        /// <para>Добавляет значение в массив по первому индексу и возвращает true.</para>
        /// </summary>
        /// <param name="element"><para>Element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddFirstAndReturnTrue(IList<TElement> elements) => _array.AddFirstAndReturnConstant(ref _position, elements, true);
        
        /// <summary>
        /// <para>Adds all elements from the specified array to the array to fill and returns true.</para>
        /// <para>Добавляет все элементы из указанного в заполняемый массив и возвращает true.</para>
        /// </summary>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddAllAndReturnTrue(IList<TElement> elements) => _array.AddAllAndReturnConstant(ref _position, elements, true);

        /// <summary>
        /// <para>Adds values to the array skipping the first element.</para>
        /// <para>Добавляет значения в массив пропуская первый элемент.</para>
        /// </summary>
        /// <param name="elements"><para>The array of values to add.</para><para>Массив значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddSkipFirstAndReturnTrue(IList<TElement> elements) => _array.AddSkipFirstAndReturnConstant(ref _position, elements, true);
    }
}
