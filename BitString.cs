using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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

        static BitString()
        {
            BitSetsIn16Bits = new byte[65536][];

            for (var i = 0; i < 65536; i++)
            {
                // Calculating size of array (number of positive bits)
                var c = 0;
                for (var k = 1; k <= 65536; k = k << 1)
                    if ((i & k) == k) c++;

                var array = new byte[c];

                // Adding positive bits indices into array
                byte j = 0;
                c = 0;
                for (var k = 1; k <= 65536; k = k << 1)
                {
                    if ((i & k) == k)
                        array[c++] = j;
                    j++;
                }

                BitSetsIn16Bits[i] = array;
            }
        }

        /// <summary>A way to trigger static constructor.</summary>
        static public void Init()
        {
        }

        #region Constructors

        public BitString(BitString bits)
        {
            if (bits == null)
                throw new ArgumentNullException(nameof(bits));

            _length = bits._length;
            _array = new long[(_length + 63) / 64];

            _minPositiveWord = bits._minPositiveWord;
            _maxPositiveWord = bits._maxPositiveWord;

            if (_array.Length == 1)
                _array[0] = bits._array[0];
            else
                Array.Copy(bits._array, _array, _array.Length);
        }

        public BitString(long length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            _length = length;
            _array = new long[(_length + 63) / 64];
            _minPositiveWord = _array.Length - 1;
            _maxPositiveWord = 0;
        }

        public BitString(int length, bool defaultValue)
            : this(length)
        {
            if (defaultValue)
            {
                const int fillValue = unchecked((int)0xffffffff);
                for (var i = 0; i < _array.Length; i++)
                    _array[i] = fillValue;

                _minPositiveWord = 0;
                _maxPositiveWord = _array.Length - 1;
            }
        }

        #endregion

        private void EnsureArgumentIsValid(BitString other)
        {
            if (other == null)
                throw new ArgumentNullException();
            if (other._length != _length)
                throw new ArgumentException();
        }

        private long _minPositiveWord;
        private long _maxPositiveWord;

        public long Count => _length;

        public bool IsReadOnly => false;

        public bool IsSynchronized => false;

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
                    return;

                if (value < 0)
                    throw new ArgumentOutOfRangeException();

                // Currently we never shrink the array
                if (value > _length)
                {
                    var numints = (value + 63) / 64;
                    var oldNumints = (_length + 63) / 64;
                    if (numints > _array.Length)
                    {
                        var newArr = new long[numints];
                        Array.Copy(_array, newArr, _array.Length);
                        _array = newArr;
                    }
                    else
                    {
                        Array.Clear(_array, (int)oldNumints, (int)(numints - oldNumints));
                    }

                    var mask = (int)_length % 64;
                    if (mask > 0)
                    {
                        _array[oldNumints - 1] &= ((long)1 << mask) - 1;
                    }
                }

                _length = value;
            }
        }

        public object SyncRoot => this;

        public object Clone() => new BitString(this);

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
            EnsureArgumentIsValid(other);

            var ints = (_length + 63) / 64;
            var otherArray = other._array;
            for (long i = 0; i < ints; i++)
            {
                _array[i] &= otherArray[i];
                RefreshBordersByWord(i);
            }

            return this;
        }

        public BitString Or(BitString value)
        {
            EnsureArgumentIsValid(value);

            var ints = (_length + 63) / 64;
            for (long i = 0; i < ints; i++)
            {
                _array[i] |= value._array[i];
                RefreshBordersByWord(i);
            }

            return this;
        }

        public BitString Xor(BitString value)
        {
            EnsureArgumentIsValid(value);

            var ints = (_length + 63) / 64;
            for (long i = 0; i < ints; i++)
            {
                _array[i] ^= value._array[i];
                RefreshBordersByWord(i);
            }

            return this;
        }

        private void RefreshBordersByWord(long wordIndex)
        {
            if (_array[wordIndex] != 0)
            {
                if (wordIndex < _minPositiveWord)
                    _minPositiveWord = wordIndex;
                if (wordIndex > _maxPositiveWord)
                    _maxPositiveWord = wordIndex;
            }
            else
            {
                if (wordIndex == _minPositiveWord && wordIndex != _array.Length - 1)
                    _minPositiveWord++;
                if (wordIndex == _maxPositiveWord && wordIndex != 0)
                    _maxPositiveWord--;
            }
        }

        public bool Get(long index)
        {
            if (index < 0 || index >= _length)
                throw new ArgumentOutOfRangeException();

            return GetCore(index);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetCore(long index) => (_array[index >> 6] & (long)1 << (int)(index & 63)) != 0;

        public void Set(long index)
        {
            if (index < 0 || index >= _length)
                throw new ArgumentOutOfRangeException();

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
                return false;
        }

        public void Reset(long index)
        {
            if (index < 0 || index >= _length)
                throw new ArgumentOutOfRangeException();

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
            if (index < 0 || index >= _length)
                throw new ArgumentOutOfRangeException();

            if (value)
                SetCore(index);
            else
                ResetCore(index);
        }

        public void SetAll(bool value)
        {
            var fillValue = value ? unchecked((long)0xffffffffffffffff) : 0;
            var ints = (_length + 63) / 64;
            for (long i = 0; i < ints; i++) // TODO: use _minPositiveWord and _maxPositiveWord values
                _array[i] = fillValue;
            if (value)
            {
                _minPositiveWord = 0;
                _maxPositiveWord = _array.Length - 1;
            }
            else
            {
                _minPositiveWord = _array.Length - 1;
                _maxPositiveWord = 0;
            }
        }

        public int CountSet()
        {
            var result = 0;
            for (var i = 0; i < _array.Length; i++)
            {
                var n = _array[i];
                if (n != 0)
                {
                    result += BitSetsIn16Bits[(int)(n & 0xffffu)].Length +
                              BitSetsIn16Bits[(int)((n >> 16) & 0xffffu)].Length;
                    result += BitSetsIn16Bits[(int)((n >> 32) & 0xffffu)].Length +
                              BitSetsIn16Bits[(int)((n >> 48) & 0xffffu)].Length;
                }
            }
            return result;
        }

        public List<int> GetSetIndeces()
        {
            var result = new List<int>();
            for (var i = 0; i < _array.Length; i++)
            {
                var n = _array[i];
                if (n != 0)
                {
                    GetBits(n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);

                    for (var j = 0; j < bits0to15.Length; j++)
                        result.Add(bits0to15[j] + (i * 64));

                    for (var j = 0; j < bits16to31.Length; j++)
                        result.Add(bits16to31[j] + 16 + (i * 64));

                    for (var j = 0; j < bits32to47.Length; j++)
                        result.Add(bits32to47[j] + 32 + (i * 64));

                    for (var j = 0; j < bits48to63.Length; j++)
                        result.Add(bits48to63[j] + 48 + (i * 64));
                }
            }

            return result;
        }

        public List<ulong> GetSetUInt64Indices()
        {
            // TODO: Возможно нужно считать общее число установленных бит, тогда здесь можно будет создавать сразу массив
            var result = new List<ulong>();
            for (long i = 0; i < _array.Length; i++)
            {
                var n = _array[i];

                if (n == 0) continue;

                GetBits(n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);

                for (var j = 0; j < bits0to15.Length; j++)
                    result.Add(bits0to15[j] + ((ulong)i * 64));

                for (var j = 0; j < bits16to31.Length; j++)
                    result.Add(bits16to31[j] + 16UL + ((ulong)i * 64));

                for (var j = 0; j < bits32to47.Length; j++)
                    result.Add(bits32to47[j] + 32UL + ((ulong)i * 64));

                for (var j = 0; j < bits48to63.Length; j++)
                    result.Add(bits48to63[j] + 48UL + ((ulong)i * 64));
            }

            return result;
        }

        public int GetFirstSetBitIndex()
        {
            for (var i = 0; i < _array.Length; i++)
            {
                var n = _array[i];
                if (n != 0)
                {
                    GetBits(n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);

                    if (bits0to15.Length > 0)
                        return bits0to15[0] + (i * 64);
                    if (bits16to31.Length > 0)
                        return bits16to31[0] + 16 + (i * 64);
                    if (bits32to47.Length > 0)
                        return bits32to47[0] + 32 + (i * 64);
                    return bits48to63[0] + 48 + (i * 64);
                }
            }

            return -1;
        }

        public int GetLastSetBitIndex()
        {
            for (var i = _array.Length - 1; i >= 0; i--)
            {
                var n = _array[i];
                if (n != 0)
                {
                    GetBits(n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);

                    if (bits48to63.Length > 0)
                        return bits48to63[bits48to63.Length - 1] + 48 + (i * 64);
                    if (bits32to47.Length > 0)
                        return bits32to47[bits32to47.Length - 1] + 32 + (i * 64);
                    if (bits16to31.Length > 0)
                        return bits16to31[bits16to31.Length - 1] + 16 + (i * 64);
                    return bits0to15[bits0to15.Length - 1] + (i * 64);
                }
            }

            return -1;
        }

        public int GetSetIndecesCount()
        {
            var result = 0;
            for (var i = 0; i < _array.Length; i++)
            {
                var n = _array[i];
                if (n != 0)
                {
                    GetBits(n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);

                    result += bits0to15.Length + bits16to31.Length + bits32to47.Length + bits48to63.Length;
                }
            }
            return result;
        }

        public bool HaveCommonBits(BitString other)
        {
            if (Length != other.Length)
                throw new ArgumentException("Bit strings must have same size", nameof(other));

            var from = Math.Max(_minPositiveWord, other._minPositiveWord);
            var to = Math.Min(_maxPositiveWord, other._maxPositiveWord);

            var result = false;

            var otherArray = other._array;

            for (var i = from; i <= to; i++)
            {
                var v1 = _array[i];
                var v2 = otherArray[i];
                if (v1 != 0 && v2 != 0 && (v1 & v2) != 0)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public int CountCommonBits(BitString other)
        {
            if (Length != other.Length)
                throw new ArgumentException("Bit strings must have same size", nameof(other));

            var from = Math.Max(_minPositiveWord, other._minPositiveWord);
            var to = Math.Min(_maxPositiveWord, other._maxPositiveWord);

            var result = 0;

            var otherArray = other._array;

            for (var i = from; i <= to; i++)
            {
                var v1 = _array[i];
                var v2 = otherArray[i];
                var n = v1 & v2;
                if (n != 0)
                {
                    GetBits(n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);

                    result += bits0to15.Length + bits16to31.Length + bits32to47.Length + bits48to63.Length;
                }
            }

            return result;
        }

        public List<int> GetCommonIndices(BitString other)
        {
            if (Length != other.Length)
                throw new ArgumentException("Bit strings must have same size", nameof(other));

            var from = Math.Max(_minPositiveWord, other._minPositiveWord);
            var to = Math.Min(_maxPositiveWord, other._maxPositiveWord);

            var result = new List<int>();

            var otherArray = other._array;

            for (var i = from; i <= to; i++)
            {
                var v1 = _array[i];
                var v2 = otherArray[i];
                var n = v1 & v2;
                if (n != 0)
                {
                    GetBits(n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);

                    if (bits0to15.Length > 0)
                        result.Add(bits0to15[0] + ((int)i * 64));
                    else if (bits16to31.Length > 0)
                        result.Add(bits16to31[0] + 16 + ((int)i * 64));
                    else if (bits32to47.Length > 0)
                        result.Add(bits32to47[0] + 32 + ((int)i * 64));
                    else
                        result.Add(bits48to63[0] + 48 + ((int)i * 64));
                }
            }

            return result;
        }

        public int GetLastCommonBitIndex(BitString other)
        {
            if (Length != other.Length)
                throw new ArgumentException("Bit strings must have same size", nameof(other));

            var from = Math.Max(_minPositiveWord, other._minPositiveWord);
            var to = Math.Min(_maxPositiveWord, other._maxPositiveWord);

            var otherArray = other._array;

            for (var i = from; i <= to; i++)
            {
                var v1 = _array[i];
                var v2 = otherArray[i];
                var n = v1 & v2;
                if (n != 0)
                {
                    GetBits(n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63);

                    if (bits48to63.Length > 0)
                        return bits48to63[bits48to63.Length - 1] + 48 + ((int)i * 64);
                    if (bits32to47.Length > 0)
                        return bits32to47[bits32to47.Length - 1] + 32 + ((int)i * 64);
                    if (bits16to31.Length > 0)
                        return bits16to31[bits16to31.Length - 1] + 16 + ((int)i * 64);
                    return bits0to15[bits0to15.Length - 1] + ((int)i * 64);
                }
            }

            return -1;
        }

        private static void GetBits(long n, out byte[] bits0to15, out byte[] bits16to31, out byte[] bits32to47, out byte[] bits48to63)
        {
            bits0to15 = BitSetsIn16Bits[(int)(n & 0xffffu)];
            bits16to31 = BitSetsIn16Bits[(int)((n >> 16) & 0xffffu)];
            bits32to47 = BitSetsIn16Bits[(int)((n >> 32) & 0xffffu)];
            bits48to63 = BitSetsIn16Bits[(int)((n >> 48) & 0xffffu)];
        }
    }
}