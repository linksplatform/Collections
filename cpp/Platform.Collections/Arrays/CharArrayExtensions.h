namespace Platform::Collections::Arrays
{
    public static unsafe class CharArrayExtensions
    {
        public: static std::int32_t GenerateHashCode(char array[], std::int32_t offset, std::int32_t length)
        {
            auto hashSeed = 5381;
            auto hashAccumulator = hashSeed;
            fixed (char* arrayPointer = &array[offset])
            {
                for (char* charPointer = arrayPointer, last = charPointer + length; charPointer < last; charPointer++)
                {
                    hashAccumulator = (hashAccumulator << 5) + hashAccumulator ^ *charPointer;
                }
            }
            return hashAccumulator + (hashSeed * 1566083941);
        }

        public: static bool ContentEqualTo(char left[], std::int32_t leftOffset, std::int32_t length, char right[], std::int32_t rightOffset)
        {
            fixed (char* leftPointer = &left[leftOffset])
            {
                fixed (char* rightPointer = &right[rightOffset])
                {
                    char* leftPointerCopy = leftPointer, rightPointerCopy = rightPointer;
                    if (!CheckArraysMainPartForEquality(leftPointerCopy, ref rightPointerCopy, ref length))
                    {
                        return false;
                    }
                    CheckArraysRemainderForEquality(leftPointerCopy, ref rightPointerCopy, ref length);
                    return length <= 0;
                }
            }
        }

        private: static bool CheckArraysMainPartForEquality(ref char* left, ref char* right, ref std::int32_t length)
        {
            while (length >= 10)
            {
                if ((*(std::int32_t*)left != *(std::int32_t*)right)
                 || (*(std::int32_t*)(left + 2) != *(std::int32_t*)(right + 2))
                 || (*(std::int32_t*)(left + 4) != *(std::int32_t*)(right + 4))
                 || (*(std::int32_t*)(left + 6) != *(std::int32_t*)(right + 6))
                 || (*(std::int32_t*)(left + 8) != *(std::int32_t*)(right + 8)))
                {
                    return false;
                }
                left += 10;
                right += 10;
                length -= 10;
            }
            return true;
        }

        private: static void CheckArraysRemainderForEquality(ref char* left, ref char* right, ref std::int32_t length)
        {
            while (length > 0)
            {
                if (*(std::int32_t*)left != *(std::int32_t*)right)
                {
                    break;
                }
                left += 2;
                right += 2;
                length -= 2;
            }
        }
    };
}
