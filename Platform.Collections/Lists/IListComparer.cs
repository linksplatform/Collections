using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Lists
{
    public class IListComparer<T> : IComparer<IList<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int Compare(IList<T> left, IList<T> right) => left.CompareTo(right);
    }
}
