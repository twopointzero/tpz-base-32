using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests.HelperTests
{
    [TestFixture]
    public class EncodeQuintets
    {
        [Test]
        public void GivenANumberOfLegalValuesAndAtLeastOneIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            var input = new byte[] { 19, 24, 12, 11, 22, 29, 234 };
            var actual = TpzBase32Converter.Helper.EncodeQuintets(input);
            Assert.Throws<ArgumentOutOfRangeException>(() => actual.ToArray());
        }

        [Test]
        public void GivenANumberOfLegalValuesShouldReturnTheCorrectEncodedValues()
        {
            var values = ZBase32Alphabet.Value.ToArray();
            var input = new byte[] { 19, 24, 12, 11, 22, 29 };
            var expected = input.Select(o => values[o]);
            var actual = TpzBase32Converter.Helper.EncodeQuintets(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenASingleIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            // Call ToArray() to force evaluation of the enumerable,
            // as the underlying implementation is lazy.
            Assert.Throws<ArgumentOutOfRangeException>(
                () => TpzBase32Converter.Helper.EncodeQuintets(new[] { (byte)42 }).ToArray());
        }

        [Test]
        public void GivenASingleLegalValueShouldReturnASingleEncodedValue()
        {
            var values = ZBase32Alphabet.Value.ToArray();
            for (byte i = 0; i < values.Length; i++)
            {
                var expected = new[] { values[i] };
                var actual = TpzBase32Converter.Helper.EncodeQuintets(new[] { i });
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void GivenAnEmptyEnumerableShouldReturnAnEmptyEnumerable()
        {
            CollectionAssert.IsEmpty(TpzBase32Converter.Helper.EncodeQuintets(Enumerable.Empty<byte>()));
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TpzBase32Converter.Helper.EncodeQuintets(null));
        }
    }
}