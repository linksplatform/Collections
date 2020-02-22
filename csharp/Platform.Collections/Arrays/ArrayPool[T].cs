using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Platform.Disposables;
using Platform.Collections.Stacks;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Arrays
{
    /// <remarks>
    /// Original idea from http://geekswithblogs.net/blackrob/archive/2014/12/18/array-pooling-in-csharp.aspx
    /// </remarks>
    public class ArrayPool<T>
    {
        // May be use Default class for that later.
        [ThreadStatic]
        private static ArrayPool<T> _threadInstance;
        internal static ArrayPool<T> ThreadInstance => _threadInstance ?? (_threadInstance = new ArrayPool<T>());

        private readonly int _maxArraysPerSize;
        private readonly Dictionary<long, Stack<T[]>> _pool = new Dictionary<long, Stack<T[]>>(ArrayPool.DefaultSizesAmount);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayPool(int maxArraysPerSize) => _maxArraysPerSize = maxArraysPerSize;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ArrayPool() : this(ArrayPool.DefaultMaxArraysPerSize) { }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Disposable<T[]> AllocateDisposable(long size) => (Allocate(size), Free);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Disposable<T[]> Resize(Disposable<T[]> source, long size)
        {
            var destination = AllocateDisposable(size);
            T[] sourceArray = source;
            if (!sourceArray.IsNullOrEmpty())
            {
                T[] destinationArray = destination;
                Array.Copy(sourceArray, destinationArray, size < sourceArray.LongLength ? size : sourceArray.LongLength);
                source.Dispose();
            }
            return destination;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Clear() => _pool.Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual T[] Allocate(long size) => size <= 0L ? Array.Empty<T>() : _pool.GetOrDefault(size)?.PopOrDefault() ?? new T[size];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void Free(T[] array)
        {
            if (array.IsNullOrEmpty())
            {
                return;
            }
            var stack = _pool.GetOrAdd(array.LongLength, size => new Stack<T[]>(_maxArraysPerSize));
            if (stack.Count == _maxArraysPerSize) // Stack is full
            {
                return;
            }
            stack.Push(array);
        }
    }
}
