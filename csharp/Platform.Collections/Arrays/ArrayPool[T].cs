using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Disposables;
using Platform.Collections.Stacks;

namespace Platform.Collections.Arrays
{
    /// <remarks>
    /// Original idea from http://geekswithblogs.net/blackrob/archive/2014/12/18/array-pooling-in-csharp.aspx
    /// </remarks>
    public class ArrayPool<T>
    {
        // May be use Default class for that later.
        [ThreadStatic]
        private static ArrayPool<T> _threadInstance;
        internal static ArrayPool<T> ThreadInstance => _threadInstance ?? (_threadInstance = new ArrayPool<T>());

        private readonly int _maxArraysPerSize;
        private readonly Dictionary<long, Stack<T[]>> _pool = new Dictionary<long, Stack<T[]>>(ArrayPool.DefaultSizesAmount);

        /// <summary>
        /// <para>Initializes a new instance of the ArrayPool class.</para>
        /// <para>Инициализирует новый экземпляр класса ArrayPool.</para>
        /// </summary>
        /// <param name="maxArraysPerSize"><para>The maximum size of the array in the pool.</para><para>Максимальный размер массива в пуле.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayPool(int maxArraysPerSize) => _maxArraysPerSize = maxArraysPerSize;

        /// <summary>
        /// <para>Initializes a new instance of the ArrayPool class with a default argument.</para>
        /// <para>Инициализирует новый экземпляр класса ArrayPool с аргументом по умолчанию.</para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayPool() : this(ArrayPool.DefaultMaxArraysPerSize) { }

        /// <summary>
        /// <para>Retrieves an array of the specified size from the array pool, which is automatically freed after calling the Disposable destructor.</para>
        /// <para>Извлекает из пула массивов массив с указанным размером, который автоматически освободится, после вызова Disposable деструктора.</para>
        /// </summary>
        /// <param name="size"><para>The allocated array size.</para><para>Размер выделяемого массива.</para></param>
        /// <returns>
        /// <para>An array of the specified size from the array pool, if it is not in the pool, a new one is created.</para>
        /// <para>Массив указанного размера из пула массивов, если его нет в пуле, cоздаётся новый.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Disposable<T[]> AllocateDisposable(long size) => (Allocate(size), Free);

        /// <summary>
        /// <para>Changes the number of elements in the array to the specified value.</para>
        /// <para>Изменяет количество элементов массива до указанной величины.</para>
        /// </summary>
        /// <param name="source"><para>Source array.</para><para>Исходный массив.</para></param>
        /// <param name="size"><para>The size of the new array.</para><para>Размер нового массива.</para></param>
        /// <returns>
        /// <para>Array with new number of elements.</para>
        /// <para>Массив с новым количеством элементов.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Disposable<T[]> Resize(Disposable<T[]> source, long size)
        {
            var destination = AllocateDisposable(size);
            T[] sourceArray = source;
            if (!sourceArray.IsNullOrEmpty())
            {
                T[] destinationArray = destination;
                Array.Copy(sourceArray, destinationArray, size < sourceArray.LongLength ? size : sourceArray.LongLength);
                source.Dispose();
            }
            return destination;
        }

        /// <summary>
        /// <para>Clears the pool.</para>
        /// <para>Очищает пул.</para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Clear() => _pool.Clear();

        /// <summary>
        /// <para>Retrieves an array with the specified size from the array pool.</para>
        /// <para>Извлекает из пула массивов массив с указанным размером.</para>
        /// </summary>
        /// <typeparam name="T"><para>The array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="size"><para>The allocated array size.</para><para>Размер выделяемого массива.</para></param>
        /// <returns>
        /// <para>An array of the specified size from the array pool, if it is not in the pool, a new one is created.</para>
        /// <para>Массив указанного размера из пула массивов, если его нет в пуле, cоздаётся новый.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual T[] Allocate(long size) => size <= 0L ? Array.Empty<T>() : _pool.GetOrDefault(size)?.PopOrDefault() ?? new T[size];

        /// <summary>
        /// <para>Freeing the array into the array pool if the array is not empty or the stack is not full.</para>
        /// <para>Освобождение массива в пул массивов, если массив не пуст, или стек не полон.</para>
        /// </summary>
        /// <param name="array"><para>The array to be freed into the pull.</para><para>Массив который нужно освобоить в пулл.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Free(T[] array)
        {
            if (array.IsNullOrEmpty())
            {
                return;
            }
            var stack = _pool.GetOrAdd(array.LongLength, size => new Stack<T[]>(_maxArraysPerSize));
            if (stack.Count == _maxArraysPerSize) // Stack is full
            {
                return;
            }
            stack.Push(array);
        }
    }
}
