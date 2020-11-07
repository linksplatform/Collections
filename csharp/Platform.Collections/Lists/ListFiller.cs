using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Lists
{
    public class ListFiller<TElement, TReturnConstant>
    {
        protected readonly List<TElement> _list;
        protected readonly TReturnConstant _returnConstant;

        /// <summary>
        /// <para>Initializes a new instance of the ListFiller class.</para>
        /// <para>Инициализирует новый экземпляр класса ListFiller.</para>
        /// </summary>
        /// <param name="list"><para>The list to be filled.</para><para>Список который будет заполняться.</para></param>
        /// <param name="returnConstant"><para>The value for the constant returned by corresponding methods.</para><para>Значение для константы возвращаемой соответствующими методами.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ListFiller(List<TElement> list, TReturnConstant returnConstant)
        {
            _list = list;
            _returnConstant = returnConstant;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ListFiller(List<TElement> list) : this(list, default) { }

        /// <summary>
        /// <para>Adds an item to the end of the list.</para>
        /// <para>Добавляет элемент в конец списка.</para>
        /// </summary>
        /// <param name="element"><para>Element to add.</para><para>Добавляемый элемент.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(TElement element) => _list.Add(element);

        /// <summary>
        /// <para>Adds an item to the end of the list and return true.</para>
        /// <para>Добавляет элемент в конец списка и возвращает true.</para>
        /// </summary>
        /// <param name="element"><para>Element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddAndReturnTrue(TElement element) => _list.AddAndReturnTrue(element);

        /// <summary>
        /// <para>Adds a value to the list at the first index and return true.</para>
        /// <para>Добавляет значение в список по первому индексу и возвращает true.</para>
        /// </summary>
        /// <param name="element"><para>Element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddFirstAndReturnTrue(IList<TElement> elements) => _list.AddFirstAndReturnTrue(elements);

        /// <summary>
        /// <para>Adds all elements from other list to this list and returns true.</para>
        /// <para>Добавляет все элементы из другого списка в этот список и возвращает true.</para>
        /// </summary>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddAllAndReturnTrue(IList<TElement> elements) => _list.AddAllAndReturnTrue(elements);

        /// <summary>
        /// <para>Adds values to the list skipping the first element.</para>
        /// <para>Добавляет значения в список пропуская первый элемент.</para>
        /// </summary>
        /// <param name="elements"><para>The list of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>True value in any case.</para>
        /// <para>Значение true в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddSkipFirstAndReturnTrue(IList<TElement> elements) => _list.AddSkipFirstAndReturnTrue(elements);
        
        /// <summary>
        /// <para>Adds an item to the end of the list and return constant.</para>
        /// <para>Добавляет элемент в конец списка и возвращает константу.</para>
        /// </summary>
        /// <param name="element"><para>Element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>Constant value in any case.</para>
        /// <para>Значение константы в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddAndReturnConstant(TElement element)
        {
            _list.Add(element);
            return _returnConstant;
        }

        /// <summary>
        /// <para>Adds a value to the list at the first index and return constant.</para>
        /// <para>Добавляет значение в список по первому индексу и возвращает константу.</para>
        /// </summary>
        /// <param name="element"><para>Element to add.</para><para>Добавляемый элемент.</para></param>
        /// <returns>
        /// <para>Constant value in any case.</para>
        /// <para>Значение константы в любом случае.</para>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddFirstAndReturnConstant(IList<TElement> elements)
        {
            _list.AddFirst(elements);
            return _returnConstant;
        }

        /// <summary>
        /// <para>Adds all elements from other list to this list and returns constant.</para>
        /// <para>Добавляет все элементы из другого списка в этот список и возвращает константу.</para>
        /// </summary>
        /// <param name="elements"><para>List of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>Constant value in any case.</para>
        /// <para>Значение константы в любом случае.</para>
        /// <returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddAllAndReturnConstant(IList<TElement> elements)
        {
            _list.AddAll(elements);
            return _returnConstant;
        }

        /// <summary>
        /// <para>Adds values to the list skipping the first element and return constant value.</para>
        /// <para>Добавляет значения в список пропуская первый элемент и возвращает значение константы.</para>
        /// </summary>
        /// <param name="elements"><para>The list of values to add.</para><para>Список значений которые необходимо добавить.</para></param>
        /// <returns>
        /// <para>constant value in any case.</para>
        /// <para>Значение константы в любом случае.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddSkipFirstAndReturnConstant(IList<TElement> elements)
        {
            _list.AddSkipFirst(elements);
            return _returnConstant;
        }
    }
}
