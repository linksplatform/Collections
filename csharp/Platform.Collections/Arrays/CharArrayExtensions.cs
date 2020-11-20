using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    public static unsafe class CharArrayExtensions
    { 
        /// <summary>
        /// <para>Generates an array hash code at the specified offset, based on the values ​​of the array elements.</para>
        /// <para>Генерирует хэш-код массива с указанным смещением, на основе значений элементов массива.</para>
        /// </summary>
        /// <param name="array"><para>Hash array.</para><para>Массив для хеширования.</para></param>
        /// <param name="offset"><para>Number of displaced elements.</para><para>Количество смещаемых элементов.</para></param>
        /// <param name="length"><para>Number of hashed values.</para><para>Количество хеширующихся значений.</para></param>
        /// <returns>
        /// <para>The hash code of the list.</para>
        /// <para>Хэш-код списка.</para>
        /// </returns>
        /// <remarks>
        /// Based on https://github.com/Microsoft/referencesource/blob/3b1eaf5203992df69de44c783a3eda37d3d4cd10/mscorlib/system/string.cs#L833
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GenerateHashCode(this char[] array, int offset, int length)
        {
            var hashSeed = 5381;
            var hashAccumulator = hashSeed;
            fixed (char* arrayPointer = &array[offset])
            {
                for (char* charPointer = arrayPointer, last = charPointer + length; charPointer < last; charPointer++)
                {
                    hashAccumulator = (hashAccumulator << 5) + hashAccumulator ^ *charPointer;
                }
            }
            return hashAccumulator + (hashSeed * 1566083941);
        }

        /// <summary>
        /// <para>Checks if all elements of two lists are equal.</para>
        /// <para>Проверяет равны ли все элементы двух списков.</para>
        /// </summary>
        /// <param name="left"><para>The first compared array.</para><para>Первый массив для сравнения.</para></param>
        /// <param name="leftOffset"><para>Number of displaced elements in first array.</para><para>Количество смещаемых элементов в первом массиве.</para></param>
        /// <param name="length"><para>Number of checked elements.</para><para>Количество проверяемых элементов.</para></param>
        /// <param name="right"><para>The second compared array.</para><para>Второй массив для сравнения.</para></param>
        /// <param name="rightOffset"><para>Number of displaced elements in second array.</para><para>Количество смещаемых элементов в втором массиве.</para></param>
        /// <returns>
        /// <para>If the passed arrays are equal to each other, true is returned, оtherwise false.</para>
        /// <para>Если переданные массивы равны друг другу, возвращается true, иначе же false.</para>
        /// </returns>
        /// <remarks>
        /// Based on https://github.com/Microsoft/referencesource/blob/3b1eaf5203992df69de44c783a3eda37d3d4cd10/mscorlib/system/string.cs#L364
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ContentEqualTo(this char[] left, int leftOffset, int length, char[] right, int rightOffset)
        {
            fixed (char* leftPointer = &left[leftOffset])
            {
                fixed (char* rightPointer = &right[rightOffset])
                {
                    char* leftPointerCopy = leftPointer, rightPointerCopy = rightPointer;
                    if (!CheckArraysMainPartForEquality(ref leftPointerCopy, ref rightPointerCopy, ref length))
                    {
                        return false;
                    }
                    CheckArraysRemainderForEquality(ref leftPointerCopy, ref rightPointerCopy, ref length);
                    return length <= 0;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool CheckArraysMainPartForEquality(ref char* left, ref char* right, ref int length)
        {
            while (length >= 10)
            {
                if ((*(int*)left != *(int*)right)
                 || (*(int*)(left + 2) != *(int*)(right + 2))
                 || (*(int*)(left + 4) != *(int*)(right + 4))
                 || (*(int*)(left + 6) != *(int*)(right + 6))
                 || (*(int*)(left + 8) != *(int*)(right + 8)))
                {
                    return false;
                }
                left += 10;
                right += 10;
                length -= 10;
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckArraysRemainderForEquality(ref char* left, ref char* right, ref int length)
        {
            // This depends on the fact that the String objects are
            // always zero terminated and that the terminating zero is not included
            // in the length. For odd string sizes, the last compare will include
            // the zero terminator.
            while (length > 0)
            {
                if (*(int*)left != *(int*)right)
                {
                    break;
                }
                left += 2;
                right += 2;
                length -= 2;
            }
        }
    }
}
