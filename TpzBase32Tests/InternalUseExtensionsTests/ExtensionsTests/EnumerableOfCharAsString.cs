using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class EnumerableOfCharAsString
    {
        [Test]
        public void GivenAnEnumerableOfCharactersShouldReturnTheExpectedResult()
        {
            var values = Constants.EncodingAlphabet.ToArray();
            for (int i = 1; i < values.Length; i++)
            {
                var input = values.Take(i);
                string expected = Constants.EncodingAlphabet.Substring(0, i);
                string actual = input.AsString();
                Assert.AreEqual(expected, actual);
            }
        }

        [Test]
        public void GivenEmptyShouldReturnEmpty()
        {
            var input = Enumerable.Empty<Char>();
            string expected = String.Empty;
            Assert.AreEqual(expected, input.AsString());
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            IEnumerable<char> input = null;
            Assert.Throws<ArgumentNullException>(() => input.AsString());
        }
    }
}