using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Sets
{
    /// <summary>
    /// <para>
    /// Represents the set filler.
    /// </para>
    /// <para></para>
    /// </summary>
    public class SetFiller<TElement, TReturnConstant>
    {
        /// <summary>
        /// <para>
        /// The set.
        /// </para>
        /// <para></para>
        /// </summary>
        protected readonly ISet<TElement> _set;
        /// <summary>
        /// <para>
        /// The return constant.
        /// </para>
        /// <para></para>
        /// </summary>
        protected readonly TReturnConstant _returnConstant;

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="SetFiller"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="set">
        /// <para>A set.</para>
        /// <para></para>
        /// </param>
        /// <param name="returnConstant">
        /// <para>A return constant.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SetFiller(ISet<TElement> set, TReturnConstant returnConstant)
        {
            _set = set;
            _returnConstant = returnConstant;
        }

        /// <summary>
        /// <para>
        /// Initializes a new <see cref="SetFiller"/> instance.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="set">
        /// <para>A set.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SetFiller(ISet<TElement> set) : this(set, default) { }

        /// <summary>
        /// <para>
        /// Adds the element.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="element">
        /// <para>The element.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(TElement element) => _set.Add(element);

        /// <summary>
        /// <para>
        /// Determines whether this instance add and return true.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="element">
        /// <para>The element.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddAndReturnTrue(TElement element) => _set.AddAndReturnTrue(element);

        /// <summary>
        /// <para>
        /// Determines whether this instance add first and return true.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddFirstAndReturnTrue(IList<TElement> elements) => _set.AddFirstAndReturnTrue(elements);

        /// <summary>
        /// <para>
        /// Determines whether this instance add all and return true.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddAllAndReturnTrue(IList<TElement> elements) => _set.AddAllAndReturnTrue(elements);

        /// <summary>
        /// <para>
        /// Determines whether this instance add skip first and return true.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The bool</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool AddSkipFirstAndReturnTrue(IList<TElement> elements) => _set.AddSkipFirstAndReturnTrue(elements);

        /// <summary>
        /// <para>
        /// Adds the and return constant using the specified element.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="element">
        /// <para>The element.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The return constant.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddAndReturnConstant(TElement element)
        {
            _set.Add(element);
            return _returnConstant;
        }

        /// <summary>
        /// <para>
        /// Adds the first and return constant using the specified elements.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The return constant.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddFirstAndReturnConstant(IList<TElement> elements)
        {
            _set.AddFirst(elements);
            return _returnConstant;
        }

        /// <summary>
        /// <para>
        /// Adds the all and return constant using the specified elements.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The return constant.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddAllAndReturnConstant(IList<TElement> elements)
        {
            _set.AddAll(elements);
            return _returnConstant;
        }

        /// <summary>
        /// <para>
        /// Adds the skip first and return constant using the specified elements.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="elements">
        /// <para>The elements.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>The return constant.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public TReturnConstant AddSkipFirstAndReturnConstant(IList<TElement> elements)
        {
            _set.AddSkipFirst(elements);
            return _returnConstant;
        }
    }
}
