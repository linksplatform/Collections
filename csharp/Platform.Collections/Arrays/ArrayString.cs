using System.Runtime.CompilerServices;
using Platform.Collections.Segments;

namespace Platform.Collections.Arrays
{
    public class ArrayString<T> : Segment<T>
    {
        /// <summary>
        /// <para>Initializes an object of the Segment class with the passed data.</para>
        /// <para>Инициализирует объект класса Segment с переданными данными.</para>
        /// </summary>
        /// <param name="length"><para>The length of the initialized array in the class object.</para><para>Длина инициализирующегося массива в обьекте класса.</para></param>
        /// <returns>
        /// <para>Segment class object.</para>
        /// <para>Объект класса Segment.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayString(int length) : base(new T[length], 0, length) { }

        /// <summary>
        /// <para>Initializes an object of the Segment class with the passed data.</para>
        /// <para>Инициализирует объект класса Segment с переданными данными.</para>
        /// </summary>
        /// <param name="array"><para>An array that will be initialized in the class object.</para><para>Массив который будет инициализирован в объекте класса.</para></param>
        /// <returns>
        /// <para>Segment class object.</para>
        /// <para>Объект класса Segment.</para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayString(T[] array) : base(array, 0, array.Length) { }

        /// <summary>
        /// <para>Initializes an object of the Segment class with the passed data.</para>
        /// <para>Инициализирует объект класса Segment с переданными данными.</para>
        /// </summary>
        /// <param name="array"><para>An array that will be initialized in the class object.</para><para>Массив который будет инициализирован в объекте класса.</para></param>
        /// <param name="length"><para><para>The length of the initialized array in the class object.</para><para>Длина инициализирующегося массива в обьекте класса.</para></param>
        /// <returns>
        /// <para>Segment class object.</para>
        /// <para>Объект класса Segment.</para> 
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayString(T[] array, int length) : base(array, 0, length) { }
    }
}
