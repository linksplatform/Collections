using System.Collections.Generic;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Lists
{
    public class IListEqualityComparer<T> : IEqualityComparer<IList<T>>
    {
        public bool Equals(IList<T> left, IList<T> right) => left.EqualTo(right);
        public int GetHashCode(IList<T> list) => list.GenerateHashCode();
    }
}
