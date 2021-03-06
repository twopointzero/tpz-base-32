﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using twopointzero.Validation;

namespace twopointzero.TpzBase32.InternalUseExtensions
{
    /// <summary>
    /// This extension method class is intended for use only within the
    /// tpz-base-32 library.
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Unpack converts the raw bit representation
        /// of the provided Int32 input argument into an enumerable of Boolean
        /// values with true members of the result representing the high bits
        /// in the input value, from LSb to MSb. This method can be considered
        /// a rough analog to the BitArray that already exists within the framework,
        /// though represented in terms of generics.
        /// </summary>
        /// <param name="input">The Int32 value whose raw bit states will be returned in enumerable form.</param>
        /// <returns>An enumerable of Boolean values representing the bit states of the input argument,
        /// ordered from least-significant bit to most-significant bit.</returns>
        /// <remarks>Note that the conversion process converts negative values
        /// via the two's complement behaviour you would reasonably expect with
        /// an Int32.</remarks>
        internal static IEnumerable<bool> Unpack(this int input)
        {
            return ((uint)input).Unpack(32);
        }

        private static IEnumerable<bool> Unpack(this uint input, byte bits)
        {
            for (int i = 0; i < bits; i++)
            {
                yield return ((input >> i) & 1) == 1;
            }
        }

        /// <summary>
        /// PackToQuintets converts an enumerable of boolean values
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
        internal static IEnumerable<byte> PackToQuintets(this IEnumerable<bool> input)
        {
            Validator.IsNotNull(input, "input");

            return input.PackToUInt32s(5).Select(o => (byte)o);
        }

        /// <summary>
        /// PackToUInt32s provides the iterator implementation
        /// that services methods like PackToQuintets, allowing them
        /// to immediately perform input validation when called, instead of
        /// accidentally deferring it until the first enumerator MoveNext call;
        /// </summary>
        private static IEnumerable<uint> PackToUInt32s(this IEnumerable<bool> input, byte bitsPerUInt32)
        {
            uint accumulator = 0;
            byte offset = 0;
            var enumerator = input.GetEnumerator();
            while (enumerator.MoveNext())
            {
                uint current = enumerator.Current ? (byte)1 : (byte)0;
                accumulator |= (current << offset++);
                if (offset == bitsPerUInt32)
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
        /// matching character within the tpz-base-32 encoding alphabet.
        /// </summary>
        /// <param name="input">A byte containing 5 low bits representing the
        /// value to be encoded. The value provided must fall with 0 through 31
        /// inclusively, otherwise an ArgumentOutOfRangeException will be thrown.</param>
        /// <returns>The character value representing the input argument as
        /// per the tpz-base-32 encoding alphabet.</returns>
        internal static char EncodeQuintet(this byte input)
        {
            Validator.IsLessThan(input, "input", 32);

            return Constants.EncodingAlphabet[input];
        }

        /// <summary>
        /// EncodeQuintets encodes an enumerable containing any number of
        /// bytes, each representing a quintet of bits, into an enumerable
        /// of Unicode characters where each input quintet is encoded into
        /// its tpz-base-32 encoded character representation.
        /// </summary>
        /// <param name="input">A non-null enumerable of bytes representing
        /// quintets of bits.</param>
        /// <returns>An enumerable of Unicode characters each of which
        /// is the tpz-base-32 encoded representation of its corresponding
        /// quintet in the input enumerable.</returns>
        /// <remarks>As the encoding is performed in a lazy streaming
        /// fashion, any byte values the fall outside the range of those
        /// that can be encoded (e.g. those with any of bits 6, 7, and/or 8
        /// high) will result in an ArgumentOutOfRange exception but only
        /// once the caller attempts to retrieve that specific value from
        /// the returned enumerator.</remarks>
        internal static IEnumerable<char> EncodeQuintets(this IEnumerable<byte> input)
        {
            Validator.IsNotNull(input, "input");

            return input.Select(o => o.EncodeQuintet());
        }

        /// <summary>
        /// AsString accepts an enumerable of Unicode characters and
        /// concatenates them into a single string.
        /// </summary>
        /// <param name="input">A non-null enumerable of characters.</param>
        /// <returns>A string representing the concatenation of the input
        /// enumerable's characters. If the input enumerable contains zero
        /// members, an empty string is returned.</returns>
        internal static string AsString(this IEnumerable<char> input)
        {
            Validator.IsNotNull(input, "input");

            var charArray = input as char[];
            if (charArray != null)
            {
                return new string(charArray);
            }

            var sb = new StringBuilder();
            foreach (var item in input)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }

        /// <summary>
        /// NormalizeToTpzBase32Alphabet processes the provided input string, correcting errors
        /// that commonly occur during the human transcription of numbers
        /// and characters. 0 (the number zero) becomes o (the lowercase
        /// letter O), l (the lowercase letter L) becomes 1 (the number one),
        /// 2 (the number two) becomes z (the lowercase letter Z), and v (the
        /// lowercase letter V) becomes u (the lowercase letter U), as these
        /// are all common transcription errors. All uppercase letters become
        /// lowercase, as the encoding is lowercase in nature.
        /// </summary>
        /// <param name="input">A non-null string containing zero or more
        /// characters that are expected to represent an encoded value.</param>
        /// <returns>A new string </returns>
        /// <remarks>This normalization process assumes that the entire input
        /// string is to be processed as an encoded value, and is not intended
        /// It will accept a string containing arbitrary text just a portion
        /// of which is a value in the encoded representation, and can be used
        /// to assist in locating encoded values within other larger pieces of
        /// text, but as a result it may modify other portions of the input
        /// string in unexpected ways.</remarks>
        internal static string NormalizeToTpzBase32Alphabet(this string input)
        {
            Validator.IsNotNull(input, "input");

            if (input.Length == 0)
            {
                return input;
            }

            return input.ToLower()
                .Replace('0', 'o')
                .Replace('l', '1')
                .Replace('2', 'z')
                .Replace('v', 'u');
        }

        /// <summary>
        /// NormalizeToTpzBase32Alphabet processes the provided input char, correcting errors
        /// that commonly occur during the human transcription of numbers
        /// and characters. 0 (the number zero) becomes o (the lowercase
        /// letter O), l (the lowercase letter L) becomes 1 (the number one),
        /// 2 (the number two) becomes z (the lowercase letter Z), and v (the
        /// lowercase letter V) becomes u (the lowercase letter U), as these
        /// are all common transcription errors. All uppercase letters become
        /// lowercase, as the encoding is lowercase in nature.
        /// </summary>
        /// <param name="input">A char containing a 
        /// character that is expected to represent an encoded value.</param>
        /// <returns>The input character, normalized if it was required.</returns>
        internal static char NormalizeToTpzBase32Alphabet(this char input)
        {
            if (input == '0')
            {
                return 'o';
            }
            if (input == 'l' || input == 'L')
            {
                return '1';
            }
            if (input == '2')
            {
                return 'z';
            }
            if (input == 'v' || input == 'V')
            {
                return 'u';
            }

            return char.IsUpper(input) ? char.ToLower(input) : input;
        }

        /// <summary>
        /// DecodeToQuintet decodes a single character within the tpz-base-32
        /// encoding alphabet into its matching 5 bit value, as a byte.
        /// </summary>
        /// <param name="input">A character in the tpz-base-32 encoding alphabet.
        /// The value provided must fall within the expected set of characters,
        /// otherwise an ArgumentOutOfRangeException will be thrown.</param>
        /// <returns>The 5 bit value, as a byte, representing the input
        /// argument once decoded via the tpz-base-32 encoding alphabet.</returns>
        internal static byte DecodeToQuintet(this char input)
        {
            var index = Constants.EncodingAlphabet.IndexOf(input.NormalizeToTpzBase32Alphabet());

            if (index == -1)
            {
                throw new ArgumentOutOfRangeException("input");
            }

            return (byte)index;
        }

        /// <summary>
        /// DecodeToQuintets decodes an enumerable containing any number of
        /// characters, each within the set of encoding characters, into
        /// an enumerable of 5 bit values represented as bytes.
        /// </summary>
        /// <param name="input">A non-null enumerable of characters each
        /// of which is within the set of encoding characters
        /// quintets of bits.</param>
        /// <returns>An enumerable of 5 bit values represented as bytes,
        /// decoded from each of the respective encoded characters in the
        /// input enumerable.</returns>
        /// <remarks>As the decoding is performed in a lazy streaming
        /// fashion, any input characters values the fall outside the set
        /// of encoding characters will result in an ArgumentOutOfRange
        /// exception but this exception will only be thrown once the caller
        /// attempts to retrieve that specific value from
        /// the returned enumerator.</remarks>
        internal static IEnumerable<byte> DecodeToQuintets(this IEnumerable<char> input)
        {
            Validator.IsNotNull(input, "input");

            return input.Select(o => o.DecodeToQuintet());
        }

        /// <summary>
        /// UnpackQuintets converts an enumerable of 5 bit values
        /// carried in bytes (in the low 5 bits) into an enumerable of booleans
        /// each of which represents a bit state from the input enumerable,
        /// ordered from least significant bit to most.
        /// </summary>
        /// <param name="input">A non-null enumerable of bytes where each byte
        /// represents a 5 bit value captured in the byte's low 5 bits.</param>
        /// <returns>A non-null enumerable of zero or more booleans, with a boolean
        /// representing the state of one bit from each of the input argument's 5 bit values.
        /// The returned boolean bits are ordered from first quintet to last and
        /// within each quintet are ordered from least signficant bit to most.</returns>
        internal static IEnumerable<bool> UnpackQuintets(this IEnumerable<byte> input)
        {
            Validator.IsNotNull(input, "input");

            return input.SelectMany(o => Unpack(o, 5));
        }

        /// <summary>
        /// PackToInt32s converts an enumerable of boolean values
        /// representing bits into an enumerable of Int32 values each of which
        /// represents 32 bits from the input enumerable.
        /// </summary>
        /// <param name="input">A non-null enumerable of booleans representing bits.</param>
        /// <returns>A non-null enumerable of zero or more Int32 values, with each
        /// containing 32 bits worth of entries from the input enumerable.</returns>
        /// <remarks>The input enumerable can include any number of bits, not
        /// just in multiples of 32, but always produces results which to the
        /// recipient may be indistinguishable from an equivalent input
        /// enumerable with additional false values. When round-tripping using
        /// these methods, additional steps will need to be taken externally
        /// in order to account for input enumerables containing a number of
        /// values that is not an even multiple of 32. Note also that the
        /// conversion process converts negative values via the two's
        /// complement behaviour you would reasonably expect with an Int32.</remarks>
        internal static IEnumerable<int> PackToInt32s(this IEnumerable<bool> input)
        {
            Validator.IsNotNull(input, "input");

            return input.PackToUInt32s(32).Select(o => (int)o);
        }
    }
}