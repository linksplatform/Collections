using System.Runtime.CompilerServices;
using Platform.Random;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Collections
{
    /// <summary>
    /// <para>
    /// Represents the bit string extensions.
    /// </para>
    /// <para></para>
    /// </summary>
    public static class BitStringExtensions
    {
        /// <summary>
        /// <para>
        /// Sets the random bits using the specified string.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="@string">
        /// <para>The string.</para>
        /// <para></para>
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetRandomBits(this BitString @string)
        {
            for (var i = 0; i < @string.Length; i++)
            {
                var value = RandomHelpers.Default.NextBoolean();
                @string.Set(i, value);
            }
        }

        /// <summary>
        /// <para>
        /// Creates a new BitString with random bits using the efficient constructor.
        /// </para>
        /// <para></para>
        /// </summary>
        /// <param name="length">
        /// <para>The length of the bit string.</para>
        /// <para></para>
        /// </param>
        /// <returns>
        /// <para>A new BitString with random bits.</para>
        /// <para></para>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BitString CreateWithRandomBits(long length)
        {
            var wordsCount = BitString.GetWordsCountFromIndex(length);
            var randomArray = new long[wordsCount];
            for (var i = 0L; i < wordsCount; i++)
            {
                randomArray[i] = RandomHelpers.Default.NextInt64();
            }
            return new BitString(randomArray, length);
        }
    }
}
