﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Arrays
{
    public class ArrayFiller<TElement, TReturnConstant> : ArrayFiller<TElement>
    {
        protected readonly TReturnConstant _returnConstant;

        /// <summary>
        /// <para>Initializes a new instance of the class <see cref="ArrayFiller"/> using the specified array, the offset from which filling will start and the constant returned when filling elements.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="ArrayFiller"/> используя указанный массив, смещение с которого начнётся заполнение и возвращаемую константу при заполнении элементов.</para>
        /// </summary>
        /// <param name="array"><para>The array to fill.</para><para>Массив для заполнения.</para></param>
        /// <param name="offset"><para>The offset from which to start filling the array.</para><para>Смещение с которого начнётся заполнение массива.</para></param>
        /// <param name="returnConstant"><para>The value for the constant returned when filling the array.</para><para>Значение для константы возвращаемой при заполнении массива.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayFiller(TElement[] array, long offset, TReturnConstant returnConstant) : base(array, offset) => _returnConstant = returnConstant;

        /// <summary>
        /// <para>Initializes a new instance of the class <see cref="ArrayFiller"/> using the specified array, the offset from which filling will start and the constant returned when filling elements.</para>
        /// <para>Инициализирует новый экземпляр класса <see cref="ArrayFiller"/> используя указанный массив и возвращаемую константу при заполнении элементов.</para>
        /// </summary>
        /// <param name="array"><para>The array to fill.</para><para>Массив для заполнения.</para></param>
        /// <param name="returnConstant"><para>The value for the constant returned by corresponding methods.</para><para>Значение для константы возвращаемой соответствующими методами.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayFiller(TElement[] array, TReturnConstant returnConstant) : this(array, 0, returnConstant) { }

        /// <summary>
        /// <para>Adds an item to the end of the array and return constant.</para>
        /// <para>Добавляет элемент в конец массива и возвращает константу.</para>
        /// </summary>
        /// <param name="element"><para>The element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>The constant value.</para>
        /// <para>Значение константы.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddAndReturnConstant(TElement element) => _array.AddAndReturnConstant(ref _position, element, _returnConstant);

        /// <summary>
        /// <para>Adds a value to the array at the first index and return constant.</para>
        /// <para>Добавляет значение в массив по первому индексу и возвращает константу.</para>
        /// </summary>
        /// <param name="element"><para>The element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>The constant value.</para>
        /// <para>Значение константы.</para>
        /// <returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddFirstAndReturnConstant(IList<TElement> elements) => _array.AddFirstAndReturnConstant(ref _position, elements, _returnConstant);

        /// <summary>
        /// <para>Adds all elements from the specified array to the array to fill and return constant.</para>
        /// <para>Добавляет все элементы из указанного в заполняемый массив и возвращает константу.</para>
        /// </summary>
        /// <param name="elements"><para>Array of values to add.</para><para>Массив значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>The constant value.</para>
        /// <para>Значение константы.</para>
        /// <returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddAllAndReturnConstant(IList<TElement> elements) => _array.AddAllAndReturnConstant(ref _position, elements, _returnConstant);

        /// <summary>
        /// <para>Adds values ​​to the array, skipping the first element and returning a constant.</para>
        /// <para>Добавляет значения в массив пропуская первый элемент и возвращает константу.</para>
        /// </summary>
        /// <param name="elements"><para>The array of values to add.</para><para>Массив значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>The constant value.</para>
        /// <para>Значение константы.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddSkipFirstAndReturnConstant(IList<TElement> elements) => _array.AddSkipFirstAndReturnConstant(ref _position, elements, _returnConstant);
    }
}
