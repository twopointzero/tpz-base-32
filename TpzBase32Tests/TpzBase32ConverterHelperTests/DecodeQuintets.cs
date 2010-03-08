using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterHelperTests
{
    [TestFixture]
    public class DecodeQuintets
    {
        [Test]
        public void GivenANumberOfLegalValuesAndAtLeastOneIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            var input = new[] { '2', 'o', 'z', '!', 'o' };
            var actual = TpzBase32ConverterHelper.DecodeQuintets(input);
            Assert.Throws<ArgumentOutOfRangeException>(() => actual.ToArray());
        }

        [Test]
        public void GivenANumberOfLegalValuesShouldReturnTheCorrectDecodedValues()
        {
            var values = ZBase32Alphabet.Value.ToArray();
            var expected = new byte[] { 19, 24, 12, 11, 22, 29 };
            var input = expected.Select(o => values[o]);
            var actual = TpzBase32ConverterHelper.DecodeQuintets(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenASingleIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            // Call ToArray() to force evaluation of the enumerable,
            // as the underlying implementation is lazy.
            Assert.Throws<ArgumentOutOfRangeException>(
                () => TpzBase32ConverterHelper.DecodeQuintets(new[] { '!' }).ToArray());
        }

        [Test]
        public void GivenASingleLegalValueShouldReturnASingleDecodedValue()
        {
            var values = ZBase32Alphabet.Value.ToArray();
            for (byte i = 0; i < values.Length; i++)
            {
                var expected = new[] { i };
                var actual = TpzBase32ConverterHelper.DecodeQuintets(new[] { values[i] });
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void GivenAnEmptyEnumerableShouldReturnAnEmptyEnumerable()
        {
            CollectionAssert.IsEmpty(TpzBase32ConverterHelper.DecodeQuintets(Enumerable.Empty<char>()));
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TpzBase32ConverterHelper.DecodeQuintets(null));
        }
    }
}