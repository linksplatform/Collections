using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Lists
{
    public class IListEqualityComparer<T> : IEqualityComparer<IList<T>>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(IList<T> left, IList<T> right) => left.EqualTo(right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetHashCode(IList<T> list) => list.GenerateHashCode();
    }
}
