using Platform.Collections.Segments;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Arrays
{
    public class ArrayString<T> : Segment<T>
    {
        public ArrayString(int length) : base(new T[length], 0, length) { }
        public ArrayString(T[] array) : base(array, 0, array.Length) { }
        public ArrayString(T[] array, int length) : base(array, 0, length) { }
    }
}
