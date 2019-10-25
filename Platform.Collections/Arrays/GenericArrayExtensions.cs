using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Arrays
{
    public static class GenericArrayExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Clone<T>(this T[] array)
        {
            var copy = new T[array.Length];
            Array.Copy(array, 0, copy, 0, array.Length);
            return copy;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> ShiftRight<T>(this T[] array) => array.ShiftRight(1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<T> ShiftRight<T>(this T[] array, int shift)
        {
            var restrictions = new T[array.Length + shift];
            Array.Copy(array, 0, restrictions, shift, array.Length);
            return restrictions;
        }
    }
}
