using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    /// <summary>
    /// <para>Represents an <see cref="T:TElement[]"/> array filler.</para>
    /// <para>Представляет заполнитель массива <see cref="T:TElement[]"/>.</para>
    /// </summary>
    /// <typeparam name="TElement"><para>The elements' type.</para><para>Тип элементов массива.</para></typeparam>
    public class ArrayFiller<TElement>
    {
        /// <summary>
        /// <para>
        /// The array.
        /// </para>
        /// <para></para>
        /// </summary>
        protected readonly TElement[] _array;
        /// <summary>
        /// <para>
        /// The position.
        /// </para>
        /// <para></para>
        /// </summary>
        protected long _position;

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ArrayFiller"/> class using the specified array as the array to fill and the offset from which to start filling.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="ArrayFiller"/>, используя указанный массив в качестве заполняемого и смещение с которого начнётся заполнение.</para>
        /// </summary>
        /// <param name="array"><para>The array to fill.</para><para>Массив для заполнения.</para></param>
        /// <param name="offset"><para>The offset from which to start filling the array.</para><para>Смещение с которого начнётся заполнение массива.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayFiller(TElement[] array, long offset)
        {
            _array = array;
            _position = offset;
        }

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ArrayFiller"/> class using the specified array. Filling will start from the beginning of the array.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="ArrayFiller"/>, используя указанный массив. Заполнение начнётся с начала массива.</para>
        /// </summary>
        /// <param name="array"><para>The array to fill.</para><para>Массив для заполнения.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayFiller(TElement[] array) : this(array, 0) { }

        /// <summary>
        /// <para>Adds an item into the array.</para>
        /// <para>Добавляет элемент в массив.</para>
        /// </summary>
        /// <param name="element"><para>The element to add.</para><para>Добавляемый элемент.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(TElement element) => _array[_position++] = element;

        /// <summary>
        /// <para>Adds an item into the array and returns <see langword="true"/>.</para>
        /// <para>Добавляет элемент в массив и возвращает <see langword="true"/>.</para>
        /// </summary>
        /// <param name="element"><para>The element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>The <see langword="true"/> value.</para>
        /// <para>Значение <see langword="true"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddAndReturnTrue(TElement element) => _array.AddAndReturnConstant(ref _position, element, true);

        /// <summary>
        /// <para>Adds the first element from the specified list to the array to fill and returns <see langword="true"/>.</para>
        /// <para>Добавляет первый элемент из указанного списка в заполняемый массив и возвращает <see langword="true"/>.</para>
        /// </summary>
        /// <param name="elements"><para>The list from which the first item will be added.</para><para>Список из которого будет добавлен первый элемент.</para></param>
        /// <returns>
        /// <para>The <see langword="true"/> value.</para>
        /// <para>Значение <see langword="true"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddFirstAndReturnTrue(IList<TElement> elements) => _array.AddFirstAndReturnConstant(ref _position, elements, true);
        
        /// <summary>
        /// <para>Adds all elements from the specified list to the array to fill and returns <see langword="true"/>.</para>
        /// <para>Добавляет все элементы из указанного списка в заполняемый массив и возвращает <see langword="true"/>.</para>
        /// </summary>
        /// <param name="elements"><para>The list of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>The <see langword="true"/> value.</para>
        /// <para>Значение <see langword="true"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddAllAndReturnTrue(IList<TElement> elements) => _array.AddAllAndReturnConstant(ref _position, elements, true);

        /// <summary>
        /// <para>Adds values to the array skipping the first element and returns <see langword="true"/>.</para>
        /// <para>Добавляет значения в массив пропуская первый элемент и возвращает <see langword="true"/>.</para>
        /// </summary>
        /// <param name="elements"><para>A list from which elements will be added except the first.</para><para>Список из которого будут добавлены элементы кроме первого.</para></param>
        /// <returns>
        /// <para>The <see langword="true"/> value.</para>
        /// <para>Значение <see langword="true"/>.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddSkipFirstAndReturnTrue(IList<TElement> elements) => _array.AddSkipFirstAndReturnConstant(ref _position, elements, true);
    }
}
