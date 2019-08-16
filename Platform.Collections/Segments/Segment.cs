using System;
using System.Collections;
using System.Collections.Generic;
using Platform.Collections.Lists;

namespace Platform.Collections.Segments
{
    public class Segment<T> : IEquatable<Segment<T>>, IList<T>
    {
        public IList<T> Base { get; }
        public int Offset { get; }
        public int Length { get; }

        public Segment(IList<T> @base, int offset, int length)
        {
            Base = @base;
            Offset = offset;
            Length = length;
        }

        public override int GetHashCode() => this.GenerateHashCode();

        public virtual bool Equals(Segment<T> other) => this.EqualTo(other);

        public override bool Equals(object obj) => obj is Segment<T> other ? Equals(other) : false;

        #region IList

        public T this[int i]
        {
            get => Base[Offset + i];
            set => Base[Offset + i] = value;
        }

        public int Count => Length;

        public bool IsReadOnly => true;

        public int IndexOf(T item)
        {
            var index = Base.IndexOf(item);
            if (index >= Offset)
            {
                var actualIndex = index - Offset;
                if (actualIndex < Length)
                {
                    return actualIndex;
                }
            }
            return -1;
        }

        public void Insert(int index, T item) => throw new NotSupportedException();

        public void RemoveAt(int index) => throw new NotSupportedException();

        public void Add(T item) => throw new NotSupportedException();

        public void Clear() => throw new NotSupportedException();

        public bool Contains(T item) => IndexOf(item) >= 0;

        public void CopyTo(T[] array, int arrayIndex)
        {
            for (var i = 0; i < Length; i++)
            {
                array[arrayIndex++] = this[i];
            }
        }

        public bool Remove(T item) => throw new NotSupportedException();

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < Length; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
