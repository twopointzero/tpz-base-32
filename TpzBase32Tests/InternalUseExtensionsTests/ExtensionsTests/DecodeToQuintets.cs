using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class DecodeToQuintets
    {
        [Test]
        public void GivenANumberOfLegalValuesAndAtLeastOneIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            var input = new[] { '2', 'o', 'z', '!', 'o' };
            var actual = input.DecodeToQuintets();
            Assert.Throws<ArgumentOutOfRangeException>(() => actual.ToArray());
        }

        [Test]
        public void GivenANumberOfLegalValuesShouldReturnTheCorrectDecodedValues()
        {
            var values = Constants.EncodingAlphabet.ToArray();
            var expected = new byte[] { 19, 24, 12, 11, 22, 29 };
            var input = expected.Select(o => values[o]);
            var actual = input.DecodeToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenASingleIllegalValueShouldThrowArgumentOutOfRangeException()
        {
            var input = new[] { '!' };

            // Call ToArray() to force evaluation of the enumerable,
            // as the underlying implementation is lazy.
            Assert.Throws<ArgumentOutOfRangeException>(() => input.DecodeToQuintets().ToArray());
        }

        [Test]
        public void GivenASingleLegalValueShouldReturnASingleDecodedValue()
        {
            var values = Constants.EncodingAlphabet.ToArray();
            for (byte i = 0; i < values.Length; i++)
            {
                var input = new[] { values[i] };
                var expected = new[] { i };
                var actual = input.DecodeToQuintets();
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void GivenAnEmptyEnumerableShouldReturnAnEmptyEnumerable()
        {
            var input = Enumerable.Empty<char>();
            CollectionAssert.IsEmpty(input.DecodeToQuintets());
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            IEnumerable<char> input = null;
            Assert.Throws<ArgumentNullException>(() => input.DecodeToQuintets());
        }
    }
}