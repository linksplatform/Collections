using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    /// <summary>
    /// <para>Represents a set of wrapper methods over <see cref="ArrayPool{T}"/> class methods to simplify access to them.</para>
    /// <para>Представляет набор методов обёрток над методами класса <see cref="ArrayPool{T}"/> для упрощения доступа к ним.</para>
    /// </summary>
    public static class ArrayPool
    {
        /// <summary>
        /// <para>
        /// The default sizes amount.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly int DefaultSizesAmount = 512;
        /// <summary>
        /// <para>
        /// The default max arrays per size.
        /// </para>
        /// <para></para>
        /// </summary>
        public static readonly int DefaultMaxArraysPerSize = 32;

        /// <summary>
        /// <para>Allocation of an array of a specified size from the array pool.</para>
        /// <para>Выделение массива указанного размера из пула массивов.</para>
        /// </summary>
        /// <typeparam name="T"><para>The array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="size"><para>The allocated array size.</para><para>Размер выделяемого массива.</para></param>
        /// <returns>
        /// <para>The array from a pool of arrays.</para>
        /// <para>Массив из пулла массивов.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Allocate<T>(long size) => ArrayPool<T>.ThreadInstance.Allocate(size);
        
        /// <summary>
        /// <para>Freeing an array into an array pool.</para>
        /// <para>Освобождение массива в пул массивов.</para>
        /// </summary>
        /// <typeparam name="T"><para>The array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>The array to be freed into the pull.</para><para>Массив который нужно освобоить в пулл.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Free<T>(T[] array) => ArrayPool<T>.ThreadInstance.Free(array);
    }
}