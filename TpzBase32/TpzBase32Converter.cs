using System;
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
        /// <remarks>Note that the conversion process converts negative values
        /// via the two's complement behaviour you would reasonably expect with
        /// an Int32.</remarks>
        public static IEnumerable<bool> ConvertToBitEnumerable(int input)
        {
            return ConvertToBitEnumerable((uint)input, 32);
        }

        private static IEnumerable<bool> ConvertToBitEnumerable(uint input, byte bits)
        {
            for (int i = 0; i < bits; i++)
            {
                yield return ((input >> i) & 1) == 1;
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

            return ConvertBitsToUInt32(input, 5).Select(o => (byte)o);
        }

        /// <summary>
        /// ConvertBitsToUInt32 provides the iterator implementation
        /// that services methods like ConvertToQuintetEnumerable, allowing them
        /// to immediately perform input validation when called, instead of
        /// accidentally deferring it until the first enumerator MoveNext call;
        /// </summary>
        private static IEnumerable<uint> ConvertBitsToUInt32(IEnumerable<bool> input, byte bitsPerUInt32)
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

        /// <summary>
        /// Normalize processes the provided input string, correcting errors
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
        public static string Normalize(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

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
        /// DecodeQuintet decodes a single character within the z-base-32
        /// encoding alphabet into its matching 5 bit value, as a byte.
        /// </summary>
        /// <param name="input">A character in the z-base-32 encoding alphabet.
        /// The value provided must fall within the expected set of characters,
        /// otherwise an ArgumentOutOfRangeException will be thrown.</param>
        /// <returns>The 5 bit value, as a byte, representing the input
        /// argument once decoded via the z-base-32 encoding alphabet.</returns>
        public static byte DecodeQuintet(char input)
        {
            var index = ZBase32Alphabet.IndexOf(input);

            if (index == -1)
            {
                throw new ArgumentOutOfRangeException("input");
            }

            return (byte)index;
        }

        /// <summary>
        /// DecodeQuintets decodes an enumerable containing any number of
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
        public static IEnumerable<byte> DecodeQuintets(IEnumerable<char> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return input.Select(o => DecodeQuintet(o));
        }

        /// <summary>
        /// ConvertQuintetsToBitEnumerable converts an enumerable of 5 bit values
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
        public static IEnumerable<bool> ConvertQuintetsToBitEnumerable(IEnumerable<byte> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return input.SelectMany(o => ConvertToBitEnumerable(o, 5));
        }

        /// <summary>
        /// ConvertToIntEnumerable converts an enumerable of boolean values
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
        public static IEnumerable<int> ConvertToIntEnumerable(IEnumerable<bool> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            return ConvertBitsToUInt32(input, 32).Select(o => (int)o);
        }
    }
}