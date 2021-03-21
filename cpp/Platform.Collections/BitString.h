

// ReSharper disable ForCanBeConvertedToForeach

namespace Platform::Collections
{
    class BitString : public IEquatable<BitString>
    {
        private: static std::uint8_t _bitsSetIn16Bits[N];
        private: std::int64_t[] _array;
        private: std::int64_t _length = 0;
        private: std::int64_t _minPositiveWord = 0;
        private: std::int64_t _maxPositiveWord = 0;

        public: bool this[std::int64_t index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public: std::int64_t Length
        {
            get => _length;
            set
            {
                if (_length == value)
                {
                    return;
                }
                Platform::Ranges::EnsureExtensions::ArgumentInRange(Platform::Exceptions::Ensure::Always, value, GetValidLengthRange(), "Length");
                if (value > _length)
                {
                    auto words = GetWordsCountFromIndex(value);
                    auto oldWords = GetWordsCountFromIndex(_length);
                    if (words > _array.LongLength)
                    {
                        auto copy = std::int64_t[words];
                        Array.Copy(_array, copy, _array.LongLength);
                        _array = copy;
                    }
                    else
                    {
                        Array.Clear(_array, (std::int32_t)oldWords, (std::int32_t)(words - oldWords));
                    }
                    auto mask = (std::int32_t)(_length % 64);
                    if (mask > 0)
                    {
                        _array[oldWords - 1] &= (1L << mask) - 1;
                    }
                }
                else
                {
                    throw std::logic_error("Not implemented exception.");
                }
                _length = value;
            }
        }

        static BitString()
        {
            _bitsSetIn16Bits = std::uint8_t[65536][];
            std::int32_t i, c, k;
            std::uint8_t bitIndex = 0;
            for (i = 0; i < 65536; i++)
            {
                for (c = 0, k = 1; k <= 65536; k <<= 1)
                {
                    if ((i & k) == k)
                    {
                        c++;
                    }
                }
                auto array = std::uint8_t[c];
                for (bitIndex = 0, c = 0, k = 1; k <= 65536; k <<= 1)
                {
                    if ((i & k) == k)
                    {
                        array[c++] = bitIndex;
                    }
                    bitIndex++;
                }
                _bitsSetIn16Bits[i] = array;
            }
        }

        public: BitString(BitString other)
        {
            Platform::Exceptions::EnsureExtensions::ArgumentNotNull(Platform::Exceptions::Ensure::Always, other, "other");
            _length = other._length;
            _array = std::int64_t[this->GetWordsCountFromIndex(_length)];
            _minPositiveWord = other._minPositiveWord;
            _maxPositiveWord = other._maxPositiveWord;
            Array.Copy(other._array, _array, _array.LongLength);
        }

        public: BitString(std::int64_t length)
        {
            Platform::Ranges::EnsureExtensions::ArgumentInRange(Platform::Exceptions::Ensure::Always, length, this->GetValidLengthRange(), "length");
            _length = length;
            _array = std::int64_t[this->GetWordsCountFromIndex(_length)];
            this->MarkBordersAsAllBitsReset();
        }

        public: BitString(std::int64_t length, bool defaultValue)
            : this(length)
        {
            if (defaultValue)
            {
                this->SetAll();
            }
        }

        public: BitString Not()
        {
            for (auto i = 0L; i < _array.LongLength; i++)
            {
                _array[i] = ~_array[i];
                this->RefreshBordersByWord(i);
            }
            return this;
        }

        public: BitString ParallelNot()
        {
            auto threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                return this->Not();
            }
            auto partitioner = Partitioner.Create(0L, _array.LongLength, _array.LongLength / threads);
            Parallel.ForEach(partitioner.GetDynamicPartitions(), ParallelOptions { MaxDegreeOfParallelism = threads }, range =>
            {
                auto maximum = range.Item2;
                for (auto i = range.Item1; i < maximum; i++)
                {
                    _array[i] = ~_array[i];
                }
            });
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        public: BitString VectorNot()
        {
            if (!Vector.IsHardwareAccelerated || _array.LongLength >= std::numeric_limits<std::int32_t>::max())
            {
                return this->Not();
            }
            auto step = Vector<std::int64_t>.Count;
            if (_array.Length < step)
            {
                return this->Not();
            }
            this->VectorNotLoop(_array, step, 0, _array.Length);
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        public: BitString ParallelVectorNot()
        {
            auto threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                return this->VectorNot();
            }
            if (!Vector.IsHardwareAccelerated)
            {
                return this->ParallelNot();
            }
            auto step = Vector<std::int64_t>.Count;
            if (_array.Length < (step * threads))
            {
                return this->VectorNot();
            }
            auto partitioner = Partitioner.Create(0, _array.Length, _array.Length / threads);
            Parallel.ForEach(partitioner.GetDynamicPartitions(), ParallelOptions { MaxDegreeOfParallelism = threads }, range => this->VectorNotLoop(_array, step, range.Item1, range.Item2));
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        private: static void VectorNotLoop(std::int64_t array[], std::int32_t step, std::int32_t start, std::int32_t maximum)
        {
            auto i = start;
            auto range = maximum - start - 1;
            auto stop = range - (range % step);
            for (; i < stop; i += step)
            {
                (~Vector<std::int64_t>(array, i)).CopyTo(array, i);
            }
            for (; i < maximum; i++)
            {
                array[i] = ~array[i];
            }
        }

        public: BitString And(BitString other)
        {
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto otherArray = other._array;
            for (auto i = from; i <= to; i++)
            {
                _array[i] &= otherArray[i];
                this->RefreshBordersByWord(i);
            }
            return this;
        }

        public: BitString ParallelAnd(BitString other)
        {
            auto threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                return this->And(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto partitioner = Partitioner.Create(from, to + 1, (to - from) / threads);
            Parallel.ForEach(partitioner.GetDynamicPartitions(), ParallelOptions { MaxDegreeOfParallelism = threads }, range =>
            {
                auto maximum = range.Item2;
                for (auto i = range.Item1; i < maximum; i++)
                {
                    _array[i] &= other._array[i];
                }
            });
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        public: BitString VectorAnd(BitString other)
        {
            if (!Vector.IsHardwareAccelerated || _array.LongLength >= std::numeric_limits<std::int32_t>::max())
            {
                return this->And(other);
            }
            auto step = Vector<std::int64_t>.Count;
            if (_array.Length < step)
            {
                return this->And(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int32_t from, out std::int32_t to);
            this->VectorAndLoop(_array, other._array, step, from, to + 1);
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        public: BitString ParallelVectorAnd(BitString other)
        {
            auto threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                return this->VectorAnd(other);
            }
            if (!Vector.IsHardwareAccelerated)
            {
                return this->ParallelAnd(other);
            }
            auto step = Vector<std::int64_t>.Count;
            if (_array.Length < (step * threads))
            {
                return this->VectorAnd(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int32_t from, out std::int32_t to);
            auto partitioner = Partitioner.Create(from, to + 1, (to - from) / threads);
            Parallel.ForEach(partitioner.GetDynamicPartitions(), ParallelOptions { MaxDegreeOfParallelism = threads }, range => this->VectorAndLoop(_array, other._array, step, range.Item1, range.Item2));
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        private: static void VectorAndLoop(std::int64_t array[], std::int64_t otherArray[], std::int32_t step, std::int32_t start, std::int32_t maximum)
        {
            auto i = start;
            auto range = maximum - start - 1;
            auto stop = range - (range % step);
            for (; i < stop; i += step)
            {
                (Vector<std::int64_t>(array, i) & Vector<std::int64_t>(otherArray, i)).CopyTo(array, i);
            }
            for (; i < maximum; i++)
            {
                array[i] &= otherArray[i];
            }
        }

        public: BitString Or(BitString other)
        {
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int64_t from, out std::int64_t to);
            for (auto i = from; i <= to; i++)
            {
                _array[i] |= other._array[i];
                this->RefreshBordersByWord(i);
            }
            return this;
        }

        public: BitString ParallelOr(BitString other)
        {
            auto threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                return this->Or(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto partitioner = Partitioner.Create(from, to + 1, (to - from) / threads);
            Parallel.ForEach(partitioner.GetDynamicPartitions(), ParallelOptions { MaxDegreeOfParallelism = threads }, range =>
            {
                auto maximum = range.Item2;
                for (auto i = range.Item1; i < maximum; i++)
                {
                    _array[i] |= other._array[i];
                }
            });
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        public: BitString VectorOr(BitString other)
        {
            if (!Vector.IsHardwareAccelerated || _array.LongLength >= std::numeric_limits<std::int32_t>::max())
            {
                return this->Or(other);
            }
            auto step = Vector<std::int64_t>.Count;
            if (_array.Length < step)
            {
                return this->Or(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int32_t from, out std::int32_t to);
            this->VectorOrLoop(_array, other._array, step, from, to + 1);
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        public: BitString ParallelVectorOr(BitString other)
        {
            auto threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                return this->VectorOr(other);
            }
            if (!Vector.IsHardwareAccelerated)
            {
                return this->ParallelOr(other);
            }
            auto step = Vector<std::int64_t>.Count;
            if (_array.Length < (step * threads))
            {
                return this->VectorOr(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int32_t from, out std::int32_t to);
            auto partitioner = Partitioner.Create(from, to + 1, (to - from) / threads);
            Parallel.ForEach(partitioner.GetDynamicPartitions(), ParallelOptions { MaxDegreeOfParallelism = threads }, range => this->VectorOrLoop(_array, other._array, step, range.Item1, range.Item2));
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        private: static void VectorOrLoop(std::int64_t array[], std::int64_t otherArray[], std::int32_t step, std::int32_t start, std::int32_t maximum)
        {
            auto i = start;
            auto range = maximum - start - 1;
            auto stop = range - (range % step);
            for (; i < stop; i += step)
            {
                (Vector<std::int64_t>(array, i) | Vector<std::int64_t>(otherArray, i)).CopyTo(array, i);
            }
            for (; i < maximum; i++)
            {
                array[i] |= otherArray[i];
            }
        }

        public: BitString Xor(BitString other)
        {
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int64_t from, out std::int64_t to);
            for (auto i = from; i <= to; i++)
            {
                _array[i] ^= other._array[i];
                this->RefreshBordersByWord(i);
            }
            return this;
        }

        public: BitString ParallelXor(BitString other)
        {
            auto threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                return this->Xor(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto partitioner = Partitioner.Create(from, to + 1, (to - from) / threads);
            Parallel.ForEach(partitioner.GetDynamicPartitions(), ParallelOptions { MaxDegreeOfParallelism = threads }, range =>
            {
                auto maximum = range.Item2;
                for (auto i = range.Item1; i < maximum; i++)
                {
                    _array[i] ^= other._array[i];
                }
            });
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        public: BitString VectorXor(BitString other)
        {
            if (!Vector.IsHardwareAccelerated || _array.LongLength >= std::numeric_limits<std::int32_t>::max())
            {
                return this->Xor(other);
            }
            auto step = Vector<std::int64_t>.Count;
            if (_array.Length < step)
            {
                return this->Xor(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int32_t from, out std::int32_t to);
            this->VectorXorLoop(_array, other._array, step, from, to + 1);
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        public: BitString ParallelVectorXor(BitString other)
        {
            auto threads = Environment.ProcessorCount / 2;
            if (threads <= 1)
            {
                return this->VectorXor(other);
            }
            if (!Vector.IsHardwareAccelerated)
            {
                return this->ParallelXor(other);
            }
            auto step = Vector<std::int64_t>.Count;
            if (_array.Length < (step * threads))
            {
                return this->VectorXor(other);
            }
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonOuterBorders(this, other, out std::int32_t from, out std::int32_t to);
            auto partitioner = Partitioner.Create(from, to + 1, (to - from) / threads);
            Parallel.ForEach(partitioner.GetDynamicPartitions(), ParallelOptions { MaxDegreeOfParallelism = threads }, range => this->VectorXorLoop(_array, other._array, step, range.Item1, range.Item2));
            this->MarkBordersAsAllBitsSet();
            this->TryShrinkBorders();
            return this;
        }

        private: static void VectorXorLoop(std::int64_t array[], std::int64_t otherArray[], std::int32_t step, std::int32_t start, std::int32_t maximum)
        {
            auto i = start;
            auto range = maximum - start - 1;
            auto stop = range - (range % step);
            for (; i < stop; i += step)
            {
                (Vector<std::int64_t>(array, i) ^ Vector<std::int64_t>(otherArray, i)).CopyTo(array, i);
            }
            for (; i < maximum; i++)
            {
                array[i] ^= otherArray[i];
            }
        }

        private: void RefreshBordersByWord(std::int64_t wordIndex)
        {
            if (_array[wordIndex] == 0)
            {
                if (wordIndex == _minPositiveWord && wordIndex != _array.LongLength - 1)
                {
                    _minPositiveWord++;
                }
                if (wordIndex == _maxPositiveWord && wordIndex != 0)
                {
                    _maxPositiveWord--;
                }
            }
            else
            {
                if (wordIndex < _minPositiveWord)
                {
                    _minPositiveWord = wordIndex;
                }
                if (wordIndex > _maxPositiveWord)
                {
                    _maxPositiveWord = wordIndex;
                }
            }
        }

        public: bool TryShrinkBorders()
        {
            this->GetBorders(out std::int64_t from, out std::int64_t to);
            while (from <= to && _array[from] == 0)
            {
                from++;
            }
            if (from > to)
            {
                this->MarkBordersAsAllBitsReset();
                return true;
            }
            while (to >= from && _array[to] == 0)
            {
                to--;
            }
            if (to < from)
            {
                this->MarkBordersAsAllBitsReset();
                return true;
            }
            auto bordersUpdated = from != _minPositiveWord || to != _maxPositiveWord;
            if (bordersUpdated)
            {
                this->SetBorders(from, to);
            }
            return bordersUpdated;
        }

        public: bool Get(std::int64_t index)
        {
            Platform::Ranges::EnsureExtensions::ArgumentInRange(Platform::Exceptions::Ensure::Always, index, this->GetValidIndexRange(), "index");
            return {_array[this->GetWordIndexFromIndex(index}] & this->GetBitMaskFromIndex(index)) != 0;
        }

        public: void Set(std::int64_t index, bool value)
        {
            if (value)
            {
                this->Set(index);
            }
            else
            {
                this->Reset(index);
            }
        }

        public: void Set(std::int64_t index)
        {
            Platform::Ranges::EnsureExtensions::ArgumentInRange(Platform::Exceptions::Ensure::Always, index, this->GetValidIndexRange(), "index");
            auto wordIndex = this->GetWordIndexFromIndex(index);
            auto mask = this->GetBitMaskFromIndex(index);
            _array[wordIndex] |= mask;
            this->RefreshBordersByWord(wordIndex);
        }

        public: void Reset(std::int64_t index)
        {
            Platform::Ranges::EnsureExtensions::ArgumentInRange(Platform::Exceptions::Ensure::Always, index, this->GetValidIndexRange(), "index");
            auto wordIndex = this->GetWordIndexFromIndex(index);
            auto mask = this->GetBitMaskFromIndex(index);
            _array[wordIndex] &= ~mask;
            this->RefreshBordersByWord(wordIndex);
        }

        public: bool Add(std::int64_t index)
        {
            auto wordIndex = this->GetWordIndexFromIndex(index);
            auto mask = this->GetBitMaskFromIndex(index);
            if ((_array[wordIndex] & mask) == 0)
            {
                _array[wordIndex] |= mask;
                this->RefreshBordersByWord(wordIndex);
                return true;
            }
            else
            {
                return false;
            }
        }

        public: void SetAll(bool value)
        {
            if (value)
            {
                this->SetAll();
            }
            else
            {
                this->ResetAll();
            }
        }

        public: void SetAll()
        {
            inline static const std::int64_t fillValue = this->unchecked((std::int64_t)0xffffffffffffffff);
            auto words = this->GetWordsCountFromIndex(_length);
            for (auto i = 0; i < words; i++)
            {
                _array[i] = fillValue;
            }
            this->MarkBordersAsAllBitsSet();
        }

        public: void ResetAll()
        {
            inline static const std::int64_t fillValue = 0;
            this->GetBorders(out std::int64_t from, out std::int64_t to);
            for (auto i = from; i <= to; i++)
            {
                _array[i] = fillValue;
            }
            this->MarkBordersAsAllBitsReset();
        }

        public: List<std::int64_t> GetSetIndices()
        {
            auto result = List<std::int64_t>();
            GetBorders(out std::int64_t from, out std::int64_t to);
            for (auto i = from; i <= to; i++)
            {
                auto word = _array[i];
                if (word != 0)
                {
                    AppendAllSetBitIndices(result, i, word);
                }
            }
            return result;
        }

        public: List<std::uint64_t> GetSetUInt64Indices()
        {
            auto result = List<std::uint64_t>();
            GetBorders(out std::uint64_t from, out std::uint64_t to);
            for (auto i = from; i <= to; i++)
            {
                auto word = _array[i];
                if (word != 0)
                {
                    AppendAllSetBitIndices(result, i, word);
                }
            }
            return result;
        }

        public: std::int64_t GetFirstSetBitIndex()
        {
            auto i = _minPositiveWord;
            auto word = _array[i];
            if (word != 0)
            {
                return this->GetFirstSetBitForWord(i, word);
            }
            return -1;
        }

        public: std::int64_t GetLastSetBitIndex()
        {
            auto i = _maxPositiveWord;
            auto word = _array[i];
            if (word != 0)
            {
                return this->GetLastSetBitForWord(i, word);
            }
            return -1;
        }

        public: std::int64_t CountSetBits()
        {
            auto total = 0L;
            this->GetBorders(out std::int64_t from, out std::int64_t to);
            for (auto i = from; i <= to; i++)
            {
                auto word = _array[i];
                if (word != 0)
                {
                    total += this->CountSetBitsForWord(word);
                }
            }
            return total;
        }

        public: bool HaveCommonBits(BitString other)
        {
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonInnerBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto otherArray = other._array;
            for (auto i = from; i <= to; i++)
            {
                auto left = _array[i];
                auto right = otherArray[i];
                if (left != 0 && right != 0 && (left & right) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public: std::int64_t CountCommonBits(BitString other)
        {
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonInnerBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto total = 0L;
            auto otherArray = other._array;
            for (auto i = from; i <= to; i++)
            {
                auto left = _array[i];
                auto right = otherArray[i];
                auto combined = left & right;
                if (combined != 0)
                {
                    total += this->CountSetBitsForWord(combined);
                }
            }
            return total;
        }

        public: List<std::int64_t> GetCommonIndices(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, "other");
            GetCommonInnerBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto result = List<std::int64_t>();
            auto otherArray = other._array;
            for (auto i = from; i <= to; i++)
            {
                auto left = _array[i];
                auto right = otherArray[i];
                auto combined = left & right;
                if (combined != 0)
                {
                    AppendAllSetBitIndices(result, i, combined);
                }
            }
            return result;
        }

        public: List<std::uint64_t> GetCommonUInt64Indices(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, "other");
            GetCommonBorders(this, other, out std::uint64_t from, out std::uint64_t to);
            auto result = List<std::uint64_t>();
            auto otherArray = other._array;
            for (auto i = from; i <= to; i++)
            {
                auto left = _array[i];
                auto right = otherArray[i];
                auto combined = left & right;
                if (combined != 0)
                {
                    AppendAllSetBitIndices(result, i, combined);
                }
            }
            return result;
        }

        public: std::int64_t GetFirstCommonBitIndex(BitString other)
        {
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonInnerBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto otherArray = other._array;
            for (auto i = from; i <= to; i++)
            {
                auto left = _array[i];
                auto right = otherArray[i];
                auto combined = left & right;
                if (combined != 0)
                {
                    return this->GetFirstSetBitForWord(i, combined);
                }
            }
            return -1;
        }

        public: std::int64_t GetLastCommonBitIndex(BitString other)
        {
            this->EnsureBitStringHasTheSameSize(other, "other");
            this->GetCommonInnerBorders(this, other, out std::int64_t from, out std::int64_t to);
            auto otherArray = other._array;
            for (auto i = to; i >= from; i--)
            {
                auto left = _array[i];
                auto right = otherArray[i];
                auto combined = left & right;
                if (combined != 0)
                {
                    return this->GetLastSetBitForWord(i, combined);
                }
            }
            return -1;
        }

        public: bool Equals(void *obj) override { return obj is BitString std::string ? this->Equals(std::string) : false; }

        public: bool operator ==(const BitString &other) const
        {
            if (_length != other._length)
            {
                return false;
            }
            auto otherArray = other._array;
            if (_array.Length != otherArray.Length)
            {
                return false;
            }
            if (_minPositiveWord != other._minPositiveWord)
            {
                return false;
            }
            if (_maxPositiveWord != other._maxPositiveWord)
            {
                return false;
            }
            GetCommonBorders(this, other, out std::uint64_t from, out std::uint64_t to);
            for (auto i = from; i <= to; i++)
            {
                if (_array[i] != otherArray[i])
                {
                    return false;
                }
            }
            return true;
        }

        private: void EnsureBitStringHasTheSameSize(BitString other, std::string argumentName)
        {
            Platform::Exceptions::EnsureExtensions::ArgumentNotNull(Platform::Exceptions::Ensure::Always, other, argumentName);
            if (_length != other._length)
            {
                throw std::invalid_argument("Bit std::string must be the same size.", argumentName);
            }
        }

        private: void MarkBordersAsAllBitsReset() { this->SetBorders(_array.LongLength - 1, 0); }

        private: void MarkBordersAsAllBitsSet() { this->SetBorders(0, _array.LongLength - 1); }

        private: void GetBorders(out std::int64_t from, out std::int64_t to)
        {
            from = _minPositiveWord;
            to = _maxPositiveWord;
        }

        private: void GetBorders(out std::uint64_t from, out std::uint64_t to)
        {
            from = (std::uint64_t)_minPositiveWord;
            to = (std::uint64_t)_maxPositiveWord;
        }

        private: void SetBorders(std::int64_t from, std::int64_t to)
        {
            _minPositiveWord = from;
            _maxPositiveWord = to;
        }

        private: Range<std::int64_t> GetValidIndexRange() { return {0, _length - 1}; }

        private: static Range<std::int64_t> GetValidLengthRange() { return {0, std::numeric_limits<std::int64_t>::max()}; }

        private: static void AppendAllSetBitIndices(List<std::uint64_t> result, std::uint64_t wordIndex, std::int64_t wordValue)
        {
            GetBits(wordValue, out std::uint8_t bits00to15[], out std::uint8_t bits16to31[], out std::uint8_t bits32to47[], out std::uint8_t bits48to63[]);
            AppendAllSetIndices(result, wordIndex, bits00to15, bits16to31, bits32to47, bits48to63);
        }

        private: static void AppendAllSetBitIndices(List<std::int64_t> result, std::int64_t wordIndex, std::int64_t wordValue)
        {
            GetBits(wordValue, out std::uint8_t bits00to15[], out std::uint8_t bits16to31[], out std::uint8_t bits32to47[], out std::uint8_t bits48to63[]);
            AppendAllSetBitIndices(result, wordIndex, bits00to15, bits16to31, bits32to47, bits48to63);
        }

        private: static std::int64_t CountSetBitsForWord(std::int64_t word)
        {
            GetBits(word, out std::uint8_t bits00to15[], out std::uint8_t bits16to31[], out std::uint8_t bits32to47[], out std::uint8_t bits48to63[]);
            return bits00to15.LongLength + bits16to31.LongLength + bits32to47.LongLength + bits48to63.LongLength;
        }

        private: static std::int64_t GetFirstSetBitForWord(std::int64_t wordIndex, std::int64_t wordValue)
        {
            GetBits(wordValue, out std::uint8_t bits00to15[], out std::uint8_t bits16to31[], out std::uint8_t bits32to47[], out std::uint8_t bits48to63[]);
            return GetFirstSetBit(wordIndex, bits00to15, bits16to31, bits32to47, bits48to63);
        }

        private: static std::int64_t GetLastSetBitForWord(std::int64_t wordIndex, std::int64_t wordValue)
        {
            GetBits(wordValue, out std::uint8_t bits00to15[], out std::uint8_t bits16to31[], out std::uint8_t bits32to47[], out std::uint8_t bits48to63[]);
            return GetLastSetBit(wordIndex, bits00to15, bits16to31, bits32to47, bits48to63);
        }

        private: static void AppendAllSetBitIndices(List<std::int64_t> result, std::int64_t i, std::uint8_t bits00to15[], std::uint8_t bits16to31[], std::uint8_t bits32to47[], std::uint8_t bits48to63[])
        {
            for (auto j = 0; j < bits00to15.Length; j++)
            {
                result.Add(bits00to15[j] + (i * 64));
            }
            for (auto j = 0; j < bits16to31.Length; j++)
            {
                result.Add(bits16to31[j] + 16 + (i * 64));
            }
            for (auto j = 0; j < bits32to47.Length; j++)
            {
                result.Add(bits32to47[j] + 32 + (i * 64));
            }
            for (auto j = 0; j < bits48to63.Length; j++)
            {
                result.Add(bits48to63[j] + 48 + (i * 64));
            }
        }

        private: static void AppendAllSetIndices(List<std::uint64_t> result, std::uint64_t i, std::uint8_t bits00to15[], std::uint8_t bits16to31[], std::uint8_t bits32to47[], std::uint8_t bits48to63[])
        {
            for (auto j = 0; j < bits00to15.Length; j++)
            {
                result.Add(bits00to15[j] + (i * 64));
            }
            for (auto j = 0; j < bits16to31.Length; j++)
            {
                result.Add(bits16to31[j] + 16UL + (i * 64));
            }
            for (auto j = 0; j < bits32to47.Length; j++)
            {
                result.Add(bits32to47[j] + 32UL + (i * 64));
            }
            for (auto j = 0; j < bits48to63.Length; j++)
            {
                result.Add(bits48to63[j] + 48UL + (i * 64));
            }
        }

        private: static std::int64_t GetFirstSetBit(std::int64_t i, std::uint8_t bits00to15[], std::uint8_t bits16to31[], std::uint8_t bits32to47[], std::uint8_t bits48to63[])
        {
            if (bits00to15.Length > 0)
            {
                return bits00to15[0] + (i * 64);
            }
            if (bits16to31.Length > 0)
            {
                return bits16to31[0] + 16 + (i * 64);
            }
            if (bits32to47.Length > 0)
            {
                return bits32to47[0] + 32 + (i * 64);
            }
            return bits48to63[0] + 48 + (i * 64);
        }

        private: static std::int64_t GetLastSetBit(std::int64_t i, std::uint8_t bits00to15[], std::uint8_t bits16to31[], std::uint8_t bits32to47[], std::uint8_t bits48to63[])
        {
            if (bits48to63.Length > 0)
            {
                return bits48to63[bits48to63.Length - 1] + 48 + (i * 64);
            }
            if (bits32to47.Length > 0)
            {
                return bits32to47[bits32to47.Length - 1] + 32 + (i * 64);
            }
            if (bits16to31.Length > 0)
            {
                return bits16to31[bits16to31.Length - 1] + 16 + (i * 64);
            }
            return bits00to15[bits00to15.Length - 1] + (i * 64);
        }

        private: static void GetBits(std::int64_t word, out std::uint8_t bits00to15[], out std::uint8_t bits16to31[], out std::uint8_t bits32to47[], out std::uint8_t bits48to63[])
        {
            bits00to15 = _bitsSetIn16Bits[word & 0xffffu];
            bits16to31 = _bitsSetIn16Bits[(word >> 16) & 0xffffu];
            bits32to47 = _bitsSetIn16Bits[(word >> 32) & 0xffffu];
            bits48to63 = _bitsSetIn16Bits[(word >> 48) & 0xffffu];
        }

        public: static void GetCommonInnerBorders(BitString left, BitString right, out std::int64_t from, out std::int64_t to)
        {
            from = Math.Max(left._minPositiveWord, right._minPositiveWord);
            to = Math.Min(left._maxPositiveWord, right._maxPositiveWord);
        }

        public: static void GetCommonOuterBorders(BitString left, BitString right, out std::int64_t from, out std::int64_t to)
        {
            from = Math.Min(left._minPositiveWord, right._minPositiveWord);
            to = Math.Max(left._maxPositiveWord, right._maxPositiveWord);
        }

        public: static void GetCommonOuterBorders(BitString left, BitString right, out std::int32_t from, out std::int32_t to)
        {
            from = (std::int32_t)Math.Min(left._minPositiveWord, right._minPositiveWord);
            to = (std::int32_t)Math.Max(left._maxPositiveWord, right._maxPositiveWord);
        }

        public: static void GetCommonBorders(BitString left, BitString right, out std::uint64_t from, out std::uint64_t to)
        {
            from = (std::uint64_t)Math.Max(left._minPositiveWord, right._minPositiveWord);
            to = (std::uint64_t)Math.Min(left._maxPositiveWord, right._maxPositiveWord);
        }

        public: static std::int64_t GetWordsCountFromIndex(std::int64_t index) { return {index + 63} / 64; }

        public: static std::int64_t GetWordIndexFromIndex(std::int64_t index) { return index >> 6; }

        public: static std::int64_t GetBitMaskFromIndex(std::int64_t index) { return 1L << (std::int32_t)(index & 63); }

        public: override std::int32_t GetHashCode() { return base.GetHashCode(); }

        public: override std::string ToString() { return Platform::Converters::To<std::string>(base).data(); }
    };
}