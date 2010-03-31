using twopointzero.TpzBase32.InternalUseExtensions;
using twopointzero.Validation;

namespace twopointzero.TpzBase32
{
    /// <summary>
    /// The Int32 class provides a tpz-base-32 encoding and
    /// decoding implementation designed for high performance.
    /// (For a reference implementation that more explicitly decomposes
    /// the encoding and decoding steps of the tpz-base-32 spec,
    /// refer to the ReferenceConverter class.)
    /// This class provides methods for encoding Int32 values into the
    /// tpz-base-32 encoding and decoding strings back to Int32 values.
    /// </summary>
    public static class Int32Converter
    {
        private const uint QuintetMask = 31;

        public static int DecodeToInt32(string input)
        {
            Validator.HasLengthInclusive(input, "input", 1, 7);

            int result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                byte quintet = input[i].DecodeToQuintet();
                result |= quintet << (5 * i);
            }
            return result;
        }

        public static string Encode(int input)
        {
            return Encode((uint)input, false);
        }

        public static string EncodeWithPadding(int input)
        {
            return Encode((uint)input, true);
        }

        private static string Encode(uint input, bool includePadding)
        {
            if (input == 0)
            {
                char zero = Constants.EncodingAlphabet[0];
                return new string(zero, includePadding ? 7 : 1);
            }

            var result = new char[7];
            for (int i = 0; i < 7; i++)
            {
                var quintet = (byte)((input >> (5 * i)) & QuintetMask);
                result[i] = quintet.EncodeQuintet();
                if (!includePadding && input < (ulong)(1 << (5 * (i + 1))))
                {
                    return new string(result, 0, i + 1);
                }
            }

            return new string(result);
        }
    }
}