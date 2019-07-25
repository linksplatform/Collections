namespace Platform.Collections.Arrays
{
    public static unsafe class CharArrayExtensions
    {
        /// <remarks>
        /// Based on https://github.com/Microsoft/referencesource/blob/3b1eaf5203992df69de44c783a3eda37d3d4cd10/mscorlib/system/string.cs#L833
        /// </remarks>
        public static int GenerateHashCode(this char[] array, int offset, int length)
        {
            var hashSeed = 5381;
            var hashAccumulator = hashSeed;
            fixed (char* src = &array[offset])
            {
                for (char* s = src, last = s + length; s < last; s++)
                {
                    hashAccumulator = (hashAccumulator << 5) + hashAccumulator ^ *s;
                }
            }
            return hashAccumulator + (hashSeed * 1566083941);
        }

        /// <remarks>
        /// Based on https://github.com/Microsoft/referencesource/blob/3b1eaf5203992df69de44c783a3eda37d3d4cd10/mscorlib/system/string.cs#L364
        /// </remarks>
        public static bool ContentEqualTo(this char[] left, int leftOffset, int length, char[] right, int rightOffset)
        {
            fixed (char* ap = &left[leftOffset])
            {
                fixed (char* bp = &right[rightOffset])
                {
                    char* a = ap, b = bp;
                    if (!CheckArraysMainPartForEquality(ref a, ref b, ref length))
                    {
                        return false;
                    }
                    CheckArraysRemainderForEquality(ref a, ref b, ref length);
                    return length <= 0;
                }
            }
        }

        private static bool CheckArraysMainPartForEquality(ref char* a, ref char* b, ref int length)
        {
            while (length >= 10)
            {
                if ((*(int*)a != *(int*)b)
                 || (*(int*)(a + 2) != *(int*)(b + 2))
                 || (*(int*)(a + 4) != *(int*)(b + 4))
                 || (*(int*)(a + 6) != *(int*)(b + 6))
                 || (*(int*)(a + 8) != *(int*)(b + 8)))
                {
                    return false;
                }
                a += 10;
                b += 10;
                length -= 10;
            }
            return true;
        }

        private static void CheckArraysRemainderForEquality(ref char* a, ref char* b, ref int length)
        {
            // This depends on the fact that the String objects are
            // always zero terminated and that the terminating zero is not included
            // in the length. For odd string sizes, the last compare will include
            // the zero terminator.
            while (length > 0)
            {
                if (*(int*)a != *(int*)b)
                {
                    break;
                }
                a += 2;
                b += 2;
                length -= 2;
            }
        }
    }
}
