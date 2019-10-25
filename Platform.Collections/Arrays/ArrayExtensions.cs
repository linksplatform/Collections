using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections.Arrays
{
    public static class ArrayExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<TLink> ShiftRight<TLink>(this TLink[] array) => array.ShiftRight(1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IList<TLink> ShiftRight<TLink>(this TLink[] array, int shift)
        {
            var restrictions = new TLink[array.Length + shift];
            Array.Copy(array, 0, restrictions, shift, array.Length);
            return restrictions;
        }
    }
}
