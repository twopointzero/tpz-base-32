using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests.HelperTests
{
    [TestFixture]
    public class EncodeQuintet
    {
        [Test]
        public void GivenAnyIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            for (byte i = 32; i < 255; i++)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => TpzBase32Converter.Helper.EncodeQuintet(i));
            }
        }

        [Test]
        public void GivenAnyLegalValueShouldNotThrow()
        {
            for (byte i = 0; i < 31; i++)
            {
                TpzBase32Converter.Helper.EncodeQuintet(i);
            }
        }

        [Test]
        public void GivenAnyLegalValueShouldProduceTheExpectedResult()
        {
            var values = ZBase32Alphabet.Value.ToArray();
            for (byte i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(values[i], TpzBase32Converter.Helper.EncodeQuintet(i));
            }
        }
    }
}