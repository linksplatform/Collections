using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Disposables;
using Platform.Collections.Stacks;

namespace Platform.Collections.Arrays
{
    /// <summary>
    /// <para>Represents a set of arrays ready for reuse.</para>
    /// <para>Представляет собой набор массивов готовых к повторному использованию.</para>
    /// </summary>
    /// <typeparam name="T"><para>The array elements type.</para><para>Тип элементов массива.</para></typeparam>
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
        /// <param name="maxArraysPerSize"><para>The maximum number of arrays in the pool per size.</para><para>Максимальное количество массивов в пуле на каждый размер.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayPool(int maxArraysPerSize) => _maxArraysPerSize = maxArraysPerSize;

        /// <summary>
        /// <para>Initializes a new instance of the ArrayPool class using the maximum number of arrays per default size.</para>
        /// <para>Инициализирует новый экземпляр класса ArrayPool, используя максимальное количество массивов на каждый размер по умолчанию.</para>
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayPool() : this(ArrayPool.DefaultMaxArraysPerSize) { }

        /// <summary>
        /// <para>Retrieves an array from the pool, which will automatically return to the pool when the container is released.</para>
        /// <para>Извлекает из пула массив, который автоматически вернётся в пул при высвобождении контейнера.</para>
        /// </summary>
        /// <param name="size"><para>The allocated array size.</para><para>Размер выделяемого массива.</para></param>
        /// <returns>
        /// <para>The container to be freed containing either a new array or an array from the pool.</para>
        /// <para>Высвобождаемый контейнер содержащий либо новый массив, либо массив из пула.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Disposable<T[]> AllocateDisposable(long size) => (Allocate(size), Free);

        /// <summary>
        /// <para>Replaces the array with another array from the pool with the specified size.</para>
        /// <para>Заменяет массив на другой массив из пула с указанным размером.</para>
        /// </summary>
        /// <param name="source"><para>The source array.</para><para>Исходный массив.</para></param>
        /// <param name="size"><para>A new array size.</para><para>Новый размер массива.</para></param>
        /// <returns>
        /// <para>Array with the new number of elements.</para>
        /// <para>Массив с новым размером.</para>
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
        /// <para>Retrieves an array with the specified size from the pool.</para>
        /// <para>Извлекает из пула массив с указанным размером.</para>
        /// </summary>
        /// <param name="size"><para>The allocated array size.</para><para>Размер выделяемого массива.</para></param>
        /// <returns>
        /// <para>Pooled array or new array.</para>
        /// <para>Массив из пула или новый массив.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual T[] Allocate(long size) => size <= 0L ? Array.Empty<T>() : _pool.GetOrDefault(size)?.PopOrDefault() ?? new T[size];

        /// <summary>
        /// <para>Freeing the array to the pool for later reuse.</para>
        /// <para>Освобождение массива в пул для последующего повторного использования.</para>
        /// </summary>
        /// <param name="array"><para>The array to be freed into the pool.</para><para>Массив который нужно освободить в пул.</para></param>
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