using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections
{
    /// <summary>
    /// <para>
    /// Represents a chaining mechanism for BitString operations that applies multiple operations in a single memory pass.
    /// This provides better performance when applying multiple operations sequentially by reducing memory bandwidth usage.
    /// </para>
    /// <para></para>
    /// </summary>
    public class BitStringChain
    {
        private readonly BitString _target;
        private readonly List<IChainedOperation> _operations;

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="BitStringChain"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="target">
        /// <para>The target BitString to apply operations to.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BitStringChain(BitString target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
            _operations = new List<IChainedOperation>();
        }

        /// <summary>
        /// <para>
        /// Chains a NOT operation.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The current BitStringChain instance for method chaining.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BitStringChain Not()
        {
            _operations.Add(new NotOperation());
            return this;
        }

        /// <summary>
        /// <para>
        /// Chains an AND operation with another BitString.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="other">
        /// <para>The other BitString to AND with.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The current BitStringChain instance for method chaining.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BitStringChain And(BitString other)
        {
            _operations.Add(new AndOperation(other));
            return this;
        }

        /// <summary>
        /// <para>
        /// Chains an OR operation with another BitString.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="other">
        /// <para>The other BitString to OR with.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The current BitStringChain instance for method chaining.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BitStringChain Or(BitString other)
        {
            _operations.Add(new OrOperation(other));
            return this;
        }

        /// <summary>
        /// <para>
        /// Chains an XOR operation with another BitString.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="other">
        /// <para>The other BitString to XOR with.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The current BitStringChain instance for method chaining.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BitStringChain Xor(BitString other)
        {
            _operations.Add(new XorOperation(other));
            return this;
        }

        /// <summary>
        /// <para>
        /// Executes all chained operations in a single pass over the memory.
        /// This provides optimal memory bandwidth usage by applying all operations 
        /// to each memory location before moving to the next.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <returns>
        /// <para>The target BitString with all operations applied.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public BitString Execute()
        {
            if (_operations.Count == 0)
            {
                return _target;
            }

            // Get the working range for optimization
            _target.GetBorders(out long from, out long to);
            
            // Apply all operations in a single pass through memory
            var targetArray = _target.GetInternalArray();
            for (var i = from; i <= to; i++)
            {
                var currentValue = targetArray[i];
                
                // Apply each operation in sequence to the current word
                foreach (var operation in _operations)
                {
                    currentValue = operation.Apply(currentValue, i);
                }
                
                targetArray[i] = currentValue;
                _target.RefreshBordersByWord(i);
            }

            _operations.Clear();
            return _target;
        }

        private interface IChainedOperation
        {
            long Apply(long value, long wordIndex);
        }

        private class NotOperation : IChainedOperation
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Apply(long value, long wordIndex) => ~value;
        }

        private class AndOperation : IChainedOperation
        {
            private readonly long[] _otherArray;

            public AndOperation(BitString other)
            {
                _otherArray = other.GetInternalArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Apply(long value, long wordIndex) => value & _otherArray[wordIndex];
        }

        private class OrOperation : IChainedOperation
        {
            private readonly long[] _otherArray;

            public OrOperation(BitString other)
            {
                _otherArray = other.GetInternalArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Apply(long value, long wordIndex) => value | _otherArray[wordIndex];
        }

        private class XorOperation : IChainedOperation
        {
            private readonly long[] _otherArray;

            public XorOperation(BitString other)
            {
                _otherArray = other.GetInternalArray();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public long Apply(long value, long wordIndex) => value ^ _otherArray[wordIndex];
        }
    }
}