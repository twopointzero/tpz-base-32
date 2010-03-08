using System;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class NormalizeStringToTpzBase32Alphabet
    {
        [Test]
        public void GivenAValueEntirelyOutsideTheCharacterSetShouldReturnIt()
        {
            const string input = "!@#$";
            const string expected = input;
            Assert.AreEqual(expected, input.NormalizeToTpzBase32Alphabet());
        }

        [Test]
        public void GivenAValueInTheEncodingAlphabetShouldReturnIt()
        {
            const string input = Constants.EncodingAlphabet;
            const string expected = input;
            Assert.AreEqual(expected, input.NormalizeToTpzBase32Alphabet());
        }

        [Test]
        public void GivenAValueThatShouldBeNormalizeToTpzBase32AlphabetdShouldReturnItAsExpected()
        {
            const string input = "0l2v";
            const string expected = "o1zu";
            Assert.AreEqual(expected, input.NormalizeToTpzBase32Alphabet());
        }

        [Test]
        public void GivenAnUppercasedValueInTheEncodingAlphabetShouldReturnItLowercased()
        {
            string input = Constants.EncodingAlphabet.ToUpper();
            const string expected = Constants.EncodingAlphabet;
            Assert.AreEqual(expected, input.NormalizeToTpzBase32Alphabet());
        }

        [Test]
        public void GivenAnUppercasedValueThatShouldBeNormalizeToTpzBase32AlphabetdShouldReturnItAsExpected()
        {
            const string input = "0L2V";
            const string expected = "o1zu";
            Assert.AreEqual(expected, input.NormalizeToTpzBase32Alphabet());
        }

        [Test]
        public void GivenEmptyShouldReturnEmpty()
        {
            const string input = "";
            const string expected = input;
            Assert.AreEqual(expected, input.NormalizeToTpzBase32Alphabet());
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            string input = null;
            Assert.Throws<ArgumentNullException>(() => input.NormalizeToTpzBase32Alphabet());
        }
    }
}