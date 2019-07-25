using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Exceptions;
using Platform.Ranges;

// ReSharper disable ForCanBeConvertedToForeach

namespace Platform.Collections
{
    /// <remarks>
    ///     А что если хранить карту значений, где каждый бит будет означать присутствует ли блок в 64 бит в массиве значений.
    ///     64 бита по 0 бит, будут означать отсутствие 64-х блоков по 64 бита. Т.е. упаковка 512 байт в 8 байт.
    ///     Подобный принцип можно применять и к 64-ём блокам и т.п. По сути это карта значений. С помощью которой можно быстро
    ///     проверять есть ли значения непосредственно далее (ниже по уровню).
    ///     Или как таблица виртуальной памяти где номер блока означает его присутствие и адрес.
    /// 
    ///     TODO: Compare what is faster to store BitSetsIn16Bits or to calculate it
    ///     TODO: Avoid int usage (replace to long)
    ///     TODO: Synchronize with current BitArray implementation
    /// </remarks>
    public class BitString
    {
        private static readonly byte[][] BitSetsIn16Bits;
        private long[] _array;
        private long _length;
        private long _minPositiveWord;
        private long _maxPositiveWord;

        public bool this[int index]
        {
            get => Get(index);
            set => Set(index, value);
        }

        public long Length
        {
            get => _length;
            set
            {
                if (_length == value)
                {
                    return;
                }
                Ensure.Always.ArgumentInRange(value, new Range<long>(0, long.MaxValue), nameof(Length));
                // Currently we never shrink the array
                if (value > _length)
                {
                    var ints = (value + 63) / 64;
                    var oldInts = (_length + 63) / 64;
                    if (ints > _array.LongLength)
                    {
                        var copy = new long[ints];
                        Array.Copy(_array, copy, _array.LongLength);
                        _array = copy;
                    }
                    else
                    {
                        Array.Clear(_array, (int)oldInts, (int)(ints - oldInts));
                    }
                    var mask = (int)_length % 64;
                    if (mask > 0)
                    {
                        _array[oldInts - 1] &= ((long)1 << mask) - 1;
                    }
                }
                _length = value;
            }
        }

        #region Constructors

        static BitString()
        {
            BitSetsIn16Bits = new byte[65536][];
            int i, c, k;
            byte j;
            for (i = 0; i < 65536; i++)
            {
                // Calculating size of array (number of positive bits)
                for (c = 0, k = 1; k <= 65536; k <<= 1)
                {
                    if ((i & k) == k)
                    {
                        c++;
                    }
                }
                var array = new byte[c];
                // Adding positive bits indices into array
                for (j = 0, c = 0, k = 1; k <= 65536; k <<= 1)
                {
                    if ((i & k) == k)
                    {
                        array[c++] = j++;
                    }
                }
                BitSetsIn16Bits[i] = array;
            }
        }

        public BitString(BitString other)
        {
            Ensure.Always.ArgumentNotNull(other, nameof(other));
            _length = other._length;
            _array = new long[(_length + 63) / 64];
            _minPositiveWord = other._minPositiveWord;
            _maxPositiveWord = other._maxPositiveWord;
            if (_array.LongLength == 1)
            {
                _array[0] = other._array[0];
            }
            else
            {
                Array.Copy(other._array, _array, _array.LongLength);
            }
        }

        public BitString(long length)
        {
            Ensure.Always.ArgumentInRange(length, new Range<long>(0, long.MaxValue), nameof(length));
            _length = length;
            _array = new long[(_length + 63) / 64];
            _minPositiveWord = _array.LongLength - 1;
            _maxPositiveWord = 0;
        }

        public BitString(int length, bool defaultValue)
            : this(length)
        {
            if (defaultValue)
            {
                const int fillValue = unchecked((int)0xffffffff);
                for (var i = 0; i < _array.LongLength; i++)
                {
                    _array[i] = fillValue;
                }
                _minPositiveWord = 0;
                _maxPositiveWord = _array.LongLength - 1;
            }
        }

        #endregion

        public BitString Not()
        {
            var ints = (_length + 63) / 64;
            for (long i = 0; i < ints; i++)
            {
                _array[i] = ~_array[i];
                RefreshBordersByWord(i);
            }
            return this;
        }

        public BitString And(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, nameof(other));
            var ints = (_length + 63) / 64;
            var otherArray = other._array;
            for (long i = 0; i < ints; i++)
            {
                _array[i] &= otherArray[i];
                RefreshBordersByWord(i);
            }
            return this;
        }

        public BitString Or(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, nameof(other));
            var ints = (_length + 63) / 64;
            for (long i = 0; i < ints; i++)
            {
                _array[i] |= other._array[i];
                RefreshBordersByWord(i);
            }
            return this;
        }

        public BitString Xor(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, nameof(other));
            var ints = (_length + 63) / 64;
            for (long i = 0; i < ints; i++)
            {
                _array[i] ^= other._array[i];
                RefreshBordersByWord(i);
            }
            return this;
        }

        private void RefreshBordersByWord(long wordIndex)
        {
            if (_array[wordIndex] != 0)
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
            else
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
        }

        public bool Get(long index)
        {
            Ensure.Always.ArgumentInRange(index, new Range<long>(0, _length - 1), nameof(index));
            return GetCore(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetCore(long index) => (_array[index >> 6] & (long)1 << (int)(index & 63)) != 0;

        public void Set(long index)
        {
            Ensure.Always.ArgumentInRange(index, new Range<long>(0, _length - 1), nameof(index));
            SetCore(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetCore(long index)
        {
            var wordIndex = index >> 6;
            var mask = (long)1 << (int)(index & 63);
            _array[wordIndex] |= mask;
            RefreshBordersByWord(wordIndex);
        }

        public bool Add(long index)
        {
            var wordIndex = index >> 6;
            var mask = (long)1 << (int)(index & 63);
            if ((_array[wordIndex] & mask) == 0)
            {
                _array[wordIndex] |= mask;
                RefreshBordersByWord(wordIndex);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset(long index)
        {
            Ensure.Always.ArgumentInRange(index, new Range<long>(0, _length - 1), nameof(index));
            ResetCore(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ResetCore(long index)
        {
            var wordIndex = index >> 6;
            var mask = (long)1 << (int)(index & 63);
            _array[wordIndex] &= ~mask;
        }

        public void Set(long index, bool value)
        {
            Ensure.Always.ArgumentInRange(index, new Range<long>(0, _length - 1), nameof(index));
            if (value)
            {
                SetCore(index);
            }
            else
            {
                ResetCore(index);
            }
        }

        public void SetAll(bool value)
        {
            var fillValue = value ? unchecked((long)0xffffffffffffffff) : 0;
            var ints = (_length + 63) / 64;
            for (long i = 0; i < ints; i++) // TODO: use _minPositiveWord and _maxPositiveWord values
            {
                _array[i] = fillValue;
            }
            if (value)
            {
                _minPositiveWord = 0;
                _maxPositiveWord = _array.LongLength - 1;
            }
            else
            {
                _minPositiveWord = _array.LongLength - 1;
                _maxPositiveWord = 0;
            }
        }

        public long CountSet()
        {
            var result = 0L;
            for (var i = 0; i < _array.LongLength; i++)
            {
                var word = _array[i];
                if (word != 0)
                {
                    result += BitSetsIn16Bits[(int)(word & 0xffffu)].LongLength +
                              BitSetsIn16Bits[(int)((word >> 16) & 0xffffu)].LongLength;
                    result += BitSetsIn16Bits[(int)((word >> 32) & 0xffffu)].LongLength +
                              BitSetsIn16Bits[(int)((word >> 48) & 0xffffu)].LongLength;
                }
            }
            return result;
        }

        public List<int> GetSetIndeces()
        {
            var result = new List<int>();
            for (var i = 0; i < _array.LongLength; i++)
            {
                var word = _array[i];
                if (word != 0)
                {
                    GetBits(word, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);
                    for (var j = 0; j < bits0to15.Length; j++)
                    {
                        result.Add(bits0to15[j] + (i * 64));
                    }
                    for (var j = 0; j < bits16to31.Length; j++)
                    {
                        result.Add(bits16to31[j] + 16 + (i * 64));
                    }
                    for (var j = 0; j < bits32to47.Length; j++)
                    {
                        result.Add(bits32to47[j] + 32 + (i * 64));
                    }
                    for (var j = 0; j < bits48to63.Length; j++)
                    {
                        result.Add(bits48to63[j] + 48 + (i * 64));
                    }
                }
            }
            return result;
        }

        public List<ulong> GetSetUInt64Indices()
        {
            // TODO: Возможно нужно считать общее число установленных бит, тогда здесь можно будет создавать сразу массив
            var result = new List<ulong>();
            for (long i = 0; i < _array.LongLength; i++)
            {
                var word = _array[i];
                if (word == 0)
                {
                    continue;
                }
                GetBits(word, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);
                for (var j = 0; j < bits0to15.Length; j++)
                {
                    result.Add(bits0to15[j] + ((ulong)i * 64));
                }
                for (var j = 0; j < bits16to31.Length; j++)
                {
                    result.Add(bits16to31[j] + 16UL + ((ulong)i * 64));
                }
                for (var j = 0; j < bits32to47.Length; j++)
                {
                    result.Add(bits32to47[j] + 32UL + ((ulong)i * 64));
                }
                for (var j = 0; j < bits48to63.Length; j++)
                {
                    result.Add(bits48to63[j] + 48UL + ((ulong)i * 64));
                }
            }
            return result;
        }

        public long GetFirstSetBitIndex()
        {
            for (var i = 0; i < _array.LongLength; i++)
            {
                var word = _array[i];
                if (word != 0)
                {
                    GetBits(word, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);
                    return GetFirstSetBit(i, bits0to15, bits16to31, bits32to47, bits48to63);
                }
            }
            return -1;
        }

        public long GetLastSetBitIndex()
        {
            for (var i = _array.LongLength - 1; i >= 0; i--)
            {
                var word = _array[i];
                if (word != 0)
                {
                    GetBits(word, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);
                    return GetLastSetBit(i, bits0to15, bits16to31, bits32to47, bits48to63);
                }
            }
            return -1;
        }

        public long GetSetIndecesCount()
        {
            var total = 0L;
            for (var i = 0; i < _array.LongLength; i++)
            {
                var word = _array[i];
                if (word != 0)
                {
                    GetBits(word, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);
                    total += bits0to15.LongLength + bits16to31.LongLength + bits32to47.LongLength + bits48to63.LongLength;
                }
            }
            return total;
        }

        public bool HaveCommonBits(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, nameof(other));
            var from = Math.Max(_minPositiveWord, other._minPositiveWord);
            var to = Math.Min(_maxPositiveWord, other._maxPositiveWord);
            var result = false;
            var otherArray = other._array;
            for (var i = from; i <= to; i++)
            {
                var left = _array[i];
                var right = otherArray[i];
                if (left != 0 && right != 0 && (left & right) != 0)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public long CountCommonBits(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, nameof(other));
            var from = Math.Max(_minPositiveWord, other._minPositiveWord);
            var to = Math.Min(_maxPositiveWord, other._maxPositiveWord);
            var total = 0L;
            var otherArray = other._array;
            for (var i = from; i <= to; i++)
            {
                var left = _array[i];
                var right = otherArray[i];
                var combined = left & right;
                if (combined != 0)
                {
                    GetBits(combined, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);
                    total += bits0to15.LongLength + bits16to31.LongLength + bits32to47.LongLength + bits48to63.LongLength;
                }
            }
            return total;
        }

        public List<int> GetCommonIndices(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, nameof(other));
            var from = Math.Max(_minPositiveWord, other._minPositiveWord);
            var to = Math.Min(_maxPositiveWord, other._maxPositiveWord);
            var result = new List<int>();
            var otherArray = other._array;
            for (var i = from; i <= to; i++)
            {
                var left = _array[i];
                var right = otherArray[i];
                var combined = left & right;
                if (combined != 0)
                {
                    GetBits(combined, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);
                    if (bits0to15.Length > 0)
                    {
                        result.Add(bits0to15[0] + ((int)i * 64));
                    }
                    else if (bits16to31.Length > 0)
                    {
                        result.Add(bits16to31[0] + 16 + ((int)i * 64));
                    }
                    else if (bits32to47.Length > 0)
                    {
                        result.Add(bits32to47[0] + 32 + ((int)i * 64));
                    }
                    else
                    {
                        result.Add(bits48to63[0] + 48 + ((int)i * 64));
                    }
                }
            }
            return result;
        }

        public long GetLastCommonBitIndex(BitString other)
        {
            EnsureBitStringHasTheSameSize(other, nameof(other));
            var from = Math.Max(_minPositiveWord, other._minPositiveWord);
            var to = Math.Min(_maxPositiveWord, other._maxPositiveWord);
            var otherArray = other._array;
            for (var i = from; i <= to; i++)
            {
                var left = _array[i];
                var right = otherArray[i];
                var combined = left & right;
                if (combined != 0)
                {
                    GetBits(combined, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);
                    return GetLastSetBit((int)i, bits0to15, bits16to31, bits32to47, bits48to63);
                }
            }
            return -1;
        }

        private void EnsureBitStringHasTheSameSize(BitString other, string argumentName)
        {
            Ensure.Always.ArgumentNotNull(other, argumentName);
            if (other._length != _length)
            {
                throw new ArgumentException("Bit string must be the same size.", argumentName);
            }
        }

        private static long GetFirstSetBit(long i, byte[] bits0to15, byte[] bits16to31, byte[] bits32to47, byte[] bits48to63)
        {
            if (bits0to15.Length > 0)
            {
                return bits0to15[0] + (i * 64);
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

        private static long GetLastSetBit(long i, byte[] bits0to15, byte[] bits16to31, byte[] bits32to47, byte[] bits48to63)
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
            return bits0to15[bits0to15.Length - 1] + (i * 64);
        }

        private static void GetBits(long word, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63)
        {
            bits0to15 = BitSetsIn16Bits[(int)(word & 0xffffu)];
            bits16to31 = BitSetsIn16Bits[(int)((word >> 16) & 0xffffu)];
            bits32to47 = BitSetsIn16Bits[(int)((word >> 32) & 0xffffu)];
            bits48to63 = BitSetsIn16Bits[(int)((word >> 48) & 0xffffu)];
        }
    }
}