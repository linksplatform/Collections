using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    public static class ArrayPool
    {
        public static readonly int DefaultSizesAmount = 512;
        public static readonly int DefaultMaxArraysPerSize = 32;

        /// <summary>
        /// <para>Allocation of an array of a certain size from the array pool.</para>
        /// <para>Выделение массива определённого размера из пула массивов.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="size"><para>Allocated array size.</para><para>Размер выделяемого массива.</para></param>
        /// <returns>
        /// <para>The memory allocated for the array.</para>
        /// <para>Память выделенная для массива.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Allocate<T>(long size) => ArrayPool<T>.ThreadInstance.Allocate(size);

        /// <summary>
        /// <para>Freeing an array from an array pool.</para>
        /// <para>Освобождение массива из пула массивов.</para>
        /// </summary>
        /// <typeparam name="T"><para>Array elements type.</para><para>Тип элементов массива.</para></typeparam>
        /// <param name="array"><para>The array to be freed from the pool.</para><para>Массив который нужно освобоить из пулла.</para></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Free<T>(T[] array) => ArrayPool<T>.ThreadInstance.Free(array);
    }
}
