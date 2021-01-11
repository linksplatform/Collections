using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    /// <summary>
    /// <para>Provides <see cref="T:TElement[]"/> array filler with additional methods that return a given constant of type <see cref="TReturnConstant"/>.</para>
    /// <para>Предоставляет заполнитель массива <see cref="T:TElement[]"/> c дополнительными методами, возвращающими заданную константу типа <see cref="TReturnConstant"/>.</para> 
    /// </summary>
    /// <typeparam name="TElement"><para>The elements's type.</para><para>Тип элементов массива.</para></typeparam>
    /// <typeparam name="TReturnConstant"><para>The return constant' type.</para><para>Тип возвращаемой константы.</para></typeparam>
    public class ArrayFiller<TElement, TReturnConstant> : ArrayFiller<TElement>
    {
        protected readonly TReturnConstant _returnConstant;

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ArrayFiller"/> class using the specified array, the offset from which filling will start and the constant returned when filling elements.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="ArrayFiller"/>, используя указанный массив, смещение с которого начнётся заполнение и константу возвращаемую при заполнении элементов.</para>
        /// </summary>
        /// <param name="array"><para>The array to fill.</para><para>Массив для заполнения.</para></param>
        /// <param name="offset"><para>The offset from which to start filling the array.</para><para>Смещение с которого начнётся заполнение массива.</para></param>
        /// <param name="returnConstant"><para>The constant's value.</para><para>Значение константы.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayFiller(TElement[] array, long offset, TReturnConstant returnConstant) : base(array, offset) => _returnConstant = returnConstant;

        /// <summary>
        /// <para>Initializes a new instance of the <see cref="ArrayFiller"/> class using the specified array and the constant returned when filling elements. Filling will start from the beginning of the array.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="ArrayFiller"/>, используя указанный массив и константу возвращаемую при заполнении элементов. Заполнение начнётся с начала массива.</para>
        /// </summary>
        /// <param name="array"><para>The array to fill.</para><para>Массив для заполнения.</para></param>
        /// <param name="returnConstant"><para>The constant's value.</para><para>Значение константы.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayFiller(TElement[] array, TReturnConstant returnConstant) : this(array, 0, returnConstant) { }

        /// <summary>
        /// <para>Adds an item in the array and returns the constant.</para>
        /// <para>Добавляет элемент в массив и возвращает константу.</para>
        /// </summary>
        /// <param name="element"><para>The element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>The constant's value.</para>
        /// <para>Значение константы.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddAndReturnConstant(TElement element) => _array.AddAndReturnConstant(ref _position, element, _returnConstant);

        /// <summary>
        /// <para>Adds the first element from the specified list to the filled array and returns the constant.</para>
        /// <para>Добавляет первый элемент из указанного списка в заполняемый массив и возвращает константу.</para>
        /// </summary>
        /// <param name="element"><para>The list from which the first item will be added.</para><para>Список из которого будет добавлен первый элемент.</para></param>
        /// <returns>
        /// <para>The constant's value.</para>
        /// <para>Значение константы.</para>
        /// <returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddFirstAndReturnConstant(IList<TElement> elements) => _array.AddFirstAndReturnConstant(ref _position, elements, _returnConstant);

        /// <summary>
        /// <para>Adds all elements from the specified list to the filled array and returns the constant.</para>
        /// <para>Добавляет все элементы из указанного списка в заполняемый массив и возвращает константу.</para>
        /// </summary>
        /// <param name="elements"><para>The list of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>The constant's value.</para>
        /// <para>Значение константы.</para>
        /// <returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddAllAndReturnConstant(IList<TElement> elements) => _array.AddAllAndReturnConstant(ref _position, elements, _returnConstant);

        /// <summary>
        /// <para>Adds the elements of the list to the array, skipping the first element and returns the constant.</para>
        /// <para>Добавляет элементы списка в массив пропуская первый элемент и возвращает константу.</para>
        /// </summary>
        /// <param name="elements"><para>The list of values to add.</para><para>Список значений для добавления.</para></param>
        /// <returns>
        /// <para>The constant's value.</para>
        /// <para>Значение константы.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddSkipFirstAndReturnConstant(IList<TElement> elements) => _array.AddSkipFirstAndReturnConstant(ref _position, elements, _returnConstant);
    }
}
