using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests
{
    [TestFixture]
    public class RoundTripInt32
    {
        [Test, Explicit("Long-running")]
        public void RoundTrip()
        {
            var rnd = new Random(42);
            for (long i = 0; i <= 1000000; i++)
            {
                var input = rnd.Next(int.MinValue, int.MaxValue);
                var encoded = TpzBase32Converter.Encode(input);
                var decoded = TpzBase32Converter.DecodeToInt32(encoded);
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
            for (long i = 0; i <= 1000000; i++)
            {
                var input = rnd.Next(int.MinValue, int.MaxValue);
                var encoded = TpzBase32Converter.EncodeWithPadding(input);
                var decoded = TpzBase32Converter.DecodeToInt32(encoded);
                if (input != decoded)
                {
                    Assert.AreEqual(input, decoded);
                }
            }
        }
    }
}