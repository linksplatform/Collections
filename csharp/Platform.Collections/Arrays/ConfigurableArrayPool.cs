using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Platform.Collections.Arrays
{
    /// <summary>
    /// A configurable array pool implementation based on .NET's ConfigurableArrayPool design.
    /// This is a simplified version for comparison purposes.
    /// </summary>
    /// <typeparam name="T">The type of arrays in the pool.</typeparam>
    public sealed class ConfigurableArrayPool<T> : IDisposable
    {
        private const int DefaultMaxArrayLength = 1024 * 1024; // 1MB
        private const int DefaultMaxNumberOfArraysPerBucket = 50;
        private const int DefaultNumberOfBuckets = 17; // Covers array sizes from 16 to 1MB
        
        private readonly Bucket[] _buckets;
        private readonly int _maxArrayLength;
        private bool _disposed;

        public ConfigurableArrayPool() : this(DefaultMaxArrayLength, DefaultMaxNumberOfArraysPerBucket)
        {
        }

        public ConfigurableArrayPool(int maxArrayLength, int maxArraysPerBucket)
        {
            if (maxArrayLength <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxArrayLength));
            if (maxArraysPerBucket <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxArraysPerBucket));

            _maxArrayLength = maxArrayLength;
            
            // Create buckets for different array sizes
            _buckets = new Bucket[DefaultNumberOfBuckets];
            for (int i = 0; i < _buckets.Length; i++)
            {
                _buckets[i] = new Bucket(GetMaxSizeForBucket(i), maxArraysPerBucket);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetMaxSizeForBucket(int binIndex) => 16 << binIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int SelectBucketIndex(int bufferSize)
        {
            // Find the bucket index for the given buffer size
            uint size = (uint)(bufferSize - 1) >> 4;
            int index = 0;
            while (size > 0)
            {
                size >>= 1;
                index++;
            }
            return index;
        }

        public T[] Rent(int minimumLength)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ConfigurableArrayPool<T>));
                
            if (minimumLength <= 0)
                return Array.Empty<T>();

            if (minimumLength > _maxArrayLength)
            {
                // Array too large for pool, allocate new
                return new T[minimumLength];
            }

            int bucketIndex = SelectBucketIndex(minimumLength);
            if (bucketIndex < _buckets.Length)
            {
                var bucket = _buckets[bucketIndex];
                T[]? array = bucket.Rent();
                if (array != null)
                {
                    return array;
                }
            }

            // No available array in pool, allocate new
            int arraySize = bucketIndex < _buckets.Length ? 
                GetMaxSizeForBucket(bucketIndex) : minimumLength;
            return new T[arraySize];
        }

        public void Return(T[]? array, bool clearArray = false)
        {
            if (array == null || _disposed)
                return;

            if (array.Length == 0 || array.Length > _maxArrayLength)
                return; // Can't pool this array

            if (clearArray)
            {
                Array.Clear(array, 0, array.Length);
            }

            int bucketIndex = SelectBucketIndex(array.Length);
            if (bucketIndex < _buckets.Length && array.Length == GetMaxSizeForBucket(bucketIndex))
            {
                _buckets[bucketIndex].Return(array);
            }
            // If array doesn't fit exactly in a bucket, just let it be GC'd
        }

        public void Dispose()
        {
            _disposed = true;
        }

        private sealed class Bucket
        {
            private readonly int _maxArrayLength;
            private readonly int _maxArraysPerBucket;
            private readonly T[][] _arrays;
            private SpinLock _lock;
            private int _count;

            public Bucket(int maxArrayLength, int maxArraysPerBucket)
            {
                _maxArrayLength = maxArrayLength;
                _maxArraysPerBucket = maxArraysPerBucket;
                _arrays = new T[maxArraysPerBucket][];
                _lock = new SpinLock(false);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public T[]? Rent()
            {
                bool lockTaken = false;
                try
                {
                    _lock.Enter(ref lockTaken);
                    
                    if (_count > 0)
                    {
                        return _arrays[--_count];
                    }
                }
                finally
                {
                    if (lockTaken)
                        _lock.Exit();
                }
                return null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Return(T[] array)
            {
                bool lockTaken = false;
                try
                {
                    _lock.Enter(ref lockTaken);
                    
                    if (_count < _maxArraysPerBucket)
                    {
                        _arrays[_count++] = array;
                    }
                    // If bucket is full, just let the array be GC'd
                }
                finally
                {
                    if (lockTaken)
                        _lock.Exit();
                }
            }
        }
    }
}