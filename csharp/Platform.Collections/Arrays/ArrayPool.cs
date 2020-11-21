using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    public static class ArrayPool
    {
        public static readonly int DefaultSizesAmount = 512;
        public static readonly int DefaultMaxArraysPerSize = 32;

        /// <summary>
        /// <para>Allocating a block of a certain size from a memory area.</para>
        /// <para>Выделение блока определенного размера из области памяти.</para>
        /// </summary>
        /// <typeparam name="T"><para>Type to allocate array for.</para><para>Тип для аллокации массива.</para></typeparam>
        /// <param name="size"><para>The memory block size.</para><para>Размер блока памяти.</para></param>
        /// <returns>
        /// <para>The memory allocated for the array.</para>
        /// <para>Память выделенная для массива.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Allocate<T>(long size) => ArrayPool<T>.ThreadInstance.Allocate(size);

        /// <summary>
        /// <para>Freeing a block of allocated heap.</para>
        /// <para>Освобождение блока выделенной памяти.</para>
        /// </summary>
        /// <typeparam name="T"><para>Type to allocate array for.</para><para>Тип для аллокации массива.</para></typeparam>
        /// <param name="array"><para>The memory allocated for the array.</para><para>Память выделенная для массива.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Free<T>(T[] array) => ArrayPool<T>.ThreadInstance.Free(array);
    }
}
