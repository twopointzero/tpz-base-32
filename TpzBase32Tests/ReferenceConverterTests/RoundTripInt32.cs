using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.ReferenceConverterTests
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
                var encoded = ReferenceConverter.Encode(input);
                var decoded = ReferenceConverter.DecodeToInt32(encoded);
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
                var encoded = ReferenceConverter.EncodeWithPadding(input);
                var decoded = ReferenceConverter.DecodeToInt32(encoded);
                if (input != decoded)
                {
                    Assert.AreEqual(input, decoded);
                }
            }
        }
    }
}