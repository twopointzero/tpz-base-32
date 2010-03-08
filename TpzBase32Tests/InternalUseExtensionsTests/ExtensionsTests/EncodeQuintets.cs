using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class EncodeQuintets
    {
        [Test]
        public void GivenANumberOfLegalValuesAndAtLeastOneIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            var input = new byte[] { 19, 24, 12, 11, 22, 29, 234 };
            var actual = input.EncodeQuintets();
            Assert.Throws<ArgumentOutOfRangeException>(() => actual.ToArray());
        }

        [Test]
        public void GivenANumberOfLegalValuesShouldReturnTheCorrectEncodedValues()
        {
            var values = Constants.EncodingAlphabet.ToArray();
            var input = new byte[] { 19, 24, 12, 11, 22, 29 };
            var expected = input.Select(o => values[o]);
            var actual = input.EncodeQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenASingleIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            var input = new[] { (byte)42 };

            // Call ToArray() to force evaluation of the enumerable,
            // as the underlying implementation is lazy.
            Assert.Throws<ArgumentOutOfRangeException>(() => input.EncodeQuintets().ToArray());
        }

        [Test]
        public void GivenASingleLegalValueShouldReturnASingleEncodedValue()
        {
            var values = Constants.EncodingAlphabet.ToArray();
            for (byte i = 0; i < values.Length; i++)
            {
                var input = new[] { i };
                var expected = new[] { values[i] };
                var actual = input.EncodeQuintets();
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void GivenAnEmptyEnumerableShouldReturnAnEmptyEnumerable()
        {
            CollectionAssert.IsEmpty(Enumerable.Empty<byte>().EncodeQuintets());
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            IEnumerable<byte> input = null;
            Assert.Throws<ArgumentNullException>(() => input.EncodeQuintets());
        }
    }
}