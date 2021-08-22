using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Stacks
{
    /// <summary>
    /// <para>
    /// Represents the default stack.
    /// </para>
    /// <para></para>
    /// </summary>
    /// <seealso cref="Stack{TElement}"/>
    /// <seealso cref="IStack{TElement}"/>
    public class DefaultStack<TElement> : Stack<TElement>, IStack<TElement>
    {
        /// <summary>
        /// <para>
        /// Gets the is empty value.
        /// </para>
        /// <para></para>
        /// </summary>
        public bool IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Count <= 0;
        }
    }
}
