using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.Int32ConverterTests
{
    [TestFixture]
    public class RoundTripInt32
    {
        [Test, Explicit("Long-running")]
        public void RoundTrip()
        {
            var rnd = new Random(42);
            for (long i = 0; i <= 6000000; i++)
            {
                var input = rnd.Next(int.MinValue, int.MaxValue);
                var encoded = Int32Converter.Encode(input);
                var decoded = Int32Converter.DecodeToInt32(encoded);
                if (input != decoded)
                {
                    Assert.AreEqual(input, decoded);
                }
            }
        }

        [Test, Explicit("Long-running")]
        public void RoundTripWithPadding()
        {
            var rnd = new Random(42);
            for (long i = 0; i <= 6000000; i++)
            {
                var input = rnd.Next(int.MinValue, int.MaxValue);
                var encoded = Int32Converter.EncodeWithPadding(input);
                var decoded = Int32Converter.DecodeToInt32(encoded);
                if (input != decoded)
                {
                    Assert.AreEqual(input, decoded);
                }
            }
        }

        [Test, Explicit("Long-running")]
        public void RoundTripWithPaddingAndReferenceConverterComparison()
        {
            var rnd = new Random(42);
            for (long i = 0; i <= 1000000; i++)
            {
                var input = rnd.Next(int.MinValue, int.MaxValue);
                var encoded = Int32Converter.EncodeWithPadding(input);
                var referenceEncoded = ReferenceConverter.EncodeWithPadding(input);
                if (encoded != referenceEncoded)
                {
                    Assert.AreEqual(referenceEncoded, encoded, "Encoded differs from reference for input of " + input);
                }

                var decoded = Int32Converter.DecodeToInt32(encoded);
                var referenceDecoded = ReferenceConverter.DecodeToInt32(encoded);
                if (decoded != referenceDecoded)
                {
                    Assert.AreEqual(referenceDecoded, decoded, "Decoded differs from reference for input of " + input);
                }

                if (input != decoded)
                {
                    Assert.AreEqual(input, decoded, "Decoded differs from original input for input of " + input);
                }
            }
        }

        [Test, Explicit("Long-running")]
        public void RoundTripWithReferenceConverterComparison()
        {
            var rnd = new Random(42);
            for (long i = 0; i <= 1000000; i++)
            {
                var input = rnd.Next(int.MinValue, int.MaxValue);
                var encoded = Int32Converter.Encode(input);
                var referenceEncoded = ReferenceConverter.Encode(input);
                if (encoded != referenceEncoded)
                {
                    Assert.AreEqual(referenceEncoded, encoded, "Encoded differs from reference for input of " + input);
                }

                var decoded = Int32Converter.DecodeToInt32(encoded);
                var referenceDecoded = ReferenceConverter.DecodeToInt32(encoded);
                if (decoded != referenceDecoded)
                {
                    Assert.AreEqual(referenceDecoded, decoded, "Decoded differs from reference for input of " + input);
                }

                if (input != decoded)
                {
                    Assert.AreEqual(input, decoded, "Decoded differs from original input for input of " + input);
                }
            }
        }
    }
}