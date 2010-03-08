using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class EncodeQuintet
    {
        [Test]
        public void GivenAnyIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            for (byte i = 32; i < 255; i++)
            {
                byte input = i;
                Assert.Throws<ArgumentOutOfRangeException>(() => input.EncodeQuintet());
            }
        }

        [Test]
        public void GivenAnyLegalValueShouldNotThrow()
        {
            for (byte i = 0; i < 31; i++)
            {
                i.EncodeQuintet();
            }
        }

        [Test]
        public void GivenAnyLegalValueShouldProduceTheExpectedResult()
        {
            var values = Constants.EncodingAlphabet.ToArray();
            for (byte i = 0; i < values.Length; i++)
            {
                byte input = i;
                char expected = values[i];
                Assert.AreEqual(expected, input.EncodeQuintet());
            }
        }
    }
}