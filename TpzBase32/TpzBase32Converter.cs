using System;
using System.Linq;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32
{
    /// <summary>
    /// The TpzBase32Converter class provides a tpz-base-32 encoding and
    /// decoding implementation designed for code clarity and to provide
    /// a solid reference implementation for future optimized converters.
    /// (A dedicated Int32 converter will be created in the immediate future.)
    /// This class provides methods for encoding Int32 values into the
    /// tpz-base-32 encoding and decoding strings back to Int32 values.
    /// </summary>
    public static class TpzBase32Converter
    {
        /// <summary>
        /// Encodes a single Int32 value, returning a seven character string.
        /// </summary>
        /// <param name="input">Any Int32 value.</param>
        /// <returns>A seven character string representing the input argument
        /// in encoded form.</returns>
        public static string EncodeWithPadding(int input)
        {
            return input.Unpack().PackToQuintets().EncodeQuintets().AsString();
        }

        /// <summary>
        /// Encodes a single Int32 value, returning a string of between one
        /// and seven characters. Returned strings of less than seven
        /// characters indicate input values expressable in fewer bits of
        /// representation, and are equivalent in value to themselves
        /// padded to a length of seven characters using the character y (the
        /// lowercase Y character.)
        /// </summary>
        /// <param name="input">Any Int32 value.</param>
        /// <returns>A one-to-seven-character string representing the input
        /// argument in encoded form, trimmed of any unnecessary trailing
        /// characters that represent zero in the encoding.</returns>
        /// <remarks>Input values of zero are encoded as a single y (the
        /// lowercase Y) character in order to disambiguate them and
        /// to allow for clean round-tripping.</remarks>
        public static string Encode(int input)
        {
            char zero = Constants.EncodingAlphabet[0];

            // This looks like a perf optimization but it is more critically
            // a correctness factor, as per the rules 0 encodes to a single
            // y character whereas passing 0 into the normal encoding process
            // would trim it down to the empty string.
            if (input == 0)
            {
                return zero.ToString();
            }

            return EncodeWithPadding(input).TrimEnd(zero);
        }

        /// <summary>
        /// Decodes an encoded string representing a single Int32 value,
        /// returning that value.
        /// </summary>
        /// <param name="input">A non-null, non-empty string of from 1 to 7
        /// encoded characters in length. All characters within the string
        /// must be valid characters within the encoding. Any leading and/or
        /// trailing whitespace or other non-encoding values must be removed
        /// before calling this method.</param>
        /// <returns>The Int32 value represented by the encoded input string.
        /// </returns>
        public static int DecodeToInt32(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input");
            }

            if (input.Length == 0 || input.Length > 7)
            {
                throw new ArgumentOutOfRangeException("input", input,
                                                      "Must be string of between 1 and 7 characters inclusive.");
            }

            return input.DecodeToQuintets().UnpackQuintets().PackToInt32s().First();
        }
    }
}