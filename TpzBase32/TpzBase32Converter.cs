﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace twopointzero.TpzBase32
{
    public static class TpzBase32Converter
    {
        private const string ZBase32Alphabet = "ybndrfg8ejkmcpqxot1uwisza345h769";

        /// <summary>
        /// ConvertToBitEnumerable converts the raw bit representation
        /// of the provided Int32 input argument into an enumerable of Boolean
        /// values with true members of the result representing the high bits
        /// in the input value, from LSb to MSb. This method can be considered
        /// a rough analog to the BitArray that already exists within the framework,
        /// though represented in terms of generics.
        /// </summary>
        /// <param name="input">The Int32 value whose raw bit states will be returned in enumerable form.</param>
        /// <returns>An enumerable of Boolean values representing the bit states of the input argument,
        /// ordered from least-significant bit to most-significant bit.</returns>
        public static IEnumerable<bool> ConvertToBitEnumerable(int input)
        {
            for (int i = 0; i < 32; i++)
            {
                yield return (((uint)input >> i) & 1) == 1;
            }
        }

        /// <summary>
        /// ConvertToQuintetEnumerable converts an enumerable of boolean values
        /// representing bits into an enumerable of bytes each of which represents
        /// (in its low bits) five bits from the input enumerable.
        /// </summary>
        /// <param name="input">A non-null enumerable of booleans representing bits.</param>
        /// <returns>A non-null enumerable of zero or more bytes, with each byte
        /// containing five bits worth of entries from the input enumerable.</returns>
        /// <remarks>The input enumerable can include any number of bits, not
        /// just in multiples of five, but always produces results which to the
        /// recipient may be indistinguishable from an equivalent input
        /// enumerable with additional false values. When round-tripping using
        /// these methods, additional steps will need to be taken externally
        /// in order to account for input enumerables containing a number of
        /// values that is not an even multiple of five.</remarks>
        public static IEnumerable<byte> ConvertToQuintetEnumerable(IEnumerable<bool> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            // We need to delgate to the method below so that the argument
            // check above runs immediately. If our yield calls were in this
            // method, the compiler's iterator support would defer the argument
            // until the first MoveNext call against the returned enumerable.
            return ConvertToQuintetEnumerableImpl(input);
        }

        /// <summary>
        /// ConvertToQuintetEnumerableImpl provides the iterator implementation
        /// that services ConvertToQuintetEnumerable, allowing ConvertToQuintetEnumerable
        /// to immediately perform input validation when called, instead of
        /// accidentally deferring it until the first enumerator MoveNext call;
        /// </summary>
        private static IEnumerable<byte> ConvertToQuintetEnumerableImpl(IEnumerable<bool> input)
        {
            byte accumulator = 0;
            byte offset = 0;
            var enumerator = input.GetEnumerator();
            while (enumerator.MoveNext())
            {
                byte current = enumerator.Current ? (byte)1 : (byte)0;
                accumulator |= (byte)(current << offset++);
                if (offset == 5)
                {
                    yield return accumulator;
                    accumulator = 0;
                    offset = 0;
                }
            }
            if (offset != 0)
            {
                yield return accumulator;
            }
        }

        /// <summary>
        /// EncodeQuintet encodes a single quintet of bits into the
        /// matching character within the z-base-32 encoding alphabet.
        /// </summary>
        /// <param name="input">A byte containing 5 low bits representing the
        /// value to be encoded. The value provided must fall with 0 through 31
        /// inclusively, otherwise an ArgumentOutOfRangeException will be thrown.</param>
        /// <returns>The character value representing the input argument as
        /// per the z-base-32 encoding alphabet.</returns>
        public static char EncodeQuintet(byte input)
        {
            if (input > 31)
            {
                throw new ArgumentOutOfRangeException("input");
            }

            return ZBase32Alphabet[input];
        }

        /// <summary>
        /// EncodeQuintets encodes an enumerable containing any number of
        /// bytes, each representing a quintet of bits, into an enumerable
        /// of Unicode characters where each input quintet is encoded into
        /// its z-base-32 encoded character representation.
        /// </summary>
        /// <param name="input">A non-null enumerable of bytes representing
        /// quintets of bits.</param>
        /// <returns>An enumerable of Unicode characters each of which
        /// is the z-base-32 encoded representation of its corresponding
        /// quintet in the input enumerable.</returns>
        /// <remarks>As the encoding is performed in a lazy streaming
        /// fashion, any byte values the fall outside the range of those
        /// that can be encoded (e.g. those with any of bits 6, 7, and/or 8
        /// high) will result in an ArgumentOutOfRange exception but only
        /// once the caller attempts to retrieve that specific value from
        /// the returned enumerator.</remarks>
        public static IEnumerable<char> EncodeQuintets(IEnumerable<byte> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return input.Select(o => EncodeQuintet(o));
        }

        /// <summary>
        /// ConvertToString accepts an enumerable of Unicode characters and
        /// concatenates them into a single string.
        /// </summary>
        /// <param name="input">A non-null enumerable of characters.</param>
        /// <returns>A string representing the concatenation of the input
        /// enumerable's characters. If the input enumerable contains zero
        /// members, an empty string is returned.</returns>
        public static string ConvertToString(IEnumerable<char> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            var sb = new StringBuilder();
            foreach(var item in input)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
    }
}