using System;
using System.Linq;

namespace twopointzero.TpzBase32
{
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
            var bits = TpzBase32ConverterHelper.ConvertToBitEnumerable(input);
            var quintets = TpzBase32ConverterHelper.ConvertToQuintetEnumerable(bits);
            var chars = TpzBase32ConverterHelper.EncodeQuintets(quintets);
            var result = TpzBase32ConverterHelper.ConvertToString(chars);
            return result;
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
            char zero = Constants.ZBase32Alphabet[0];

            if (input == 0)
            {
                return zero.ToString();
            }

            var bits = TpzBase32ConverterHelper.ConvertToBitEnumerable(input);
            var quintets = TpzBase32ConverterHelper.ConvertToQuintetEnumerable(bits);
            var chars = TpzBase32ConverterHelper.EncodeQuintets(quintets);
            var result = TpzBase32ConverterHelper.ConvertToString(chars);
            return result.TrimEnd(zero);
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

            var quintets = TpzBase32ConverterHelper.DecodeQuintets(input);
            var bits = TpzBase32ConverterHelper.ConvertQuintetsToBitEnumerable(quintets);
            var ints = TpzBase32ConverterHelper.ConvertToIntEnumerable(bits);
            var result = ints.First();
            return result;
        }


        #region Nested type: Helper

        #endregion
    }
}