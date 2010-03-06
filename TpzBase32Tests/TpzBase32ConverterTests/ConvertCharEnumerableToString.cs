using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests
{
    [TestFixture]
    public class ConvertCharEnumerableToString
    {
        [Test]
        public void GivenAnEnumerableOfCharactersShouldReturnTheExpectedResult()
        {
            var values = ZBase32Alphabet.Value.ToArray();
            for (int i = 1; i < values.Length; i++)
            {
                string expected = ZBase32Alphabet.Value.Substring(0, i);
                string actual = TpzBase32Converter.ConvertToString(values.Take(i));
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void GivenEmptyShouldReturnEmpty()
        {
            Assert.AreEqual(String.Empty, TpzBase32Converter.ConvertToString(Enumerable.Empty<Char>()));
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TpzBase32Converter.ConvertToString(null));
        }
    }
}