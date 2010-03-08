using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterHelperTests
{
    [TestFixture]
    public class NormalizeChar
    {
        [Test]
        public void GivenAValueEntirelyOutsideTheCharacterSetShouldReturnIt()
        {
            const string expected = "!@#$";
            Assert.AreEqual(expected, TpzBase32ConverterHelper.Normalize(expected));
        }

        [Test]
        public void GivenAValueInTheEncodingAlphabetShouldReturnIt()
        {
            const string expected = ZBase32Alphabet.Value;
            Assert.AreEqual(expected, TpzBase32ConverterHelper.Normalize(expected));
        }

        [Test]
        public void GivenAValueThatShouldBeNormalizedShouldReturnItAsExpected()
        {
            const string values = "0l2v";
            const string expect = "o1zu";
            Assert.AreEqual(expect, TpzBase32ConverterHelper.Normalize(values));
        }

        [Test]
        public void GivenAnUppercasedValueInTheEncodingAlphabetShouldReturnItLowercased()
        {
            const string expected = ZBase32Alphabet.Value;
            Assert.AreEqual(expected, TpzBase32ConverterHelper.Normalize(expected.ToUpper()));
        }

        [Test]
        public void GivenAnUppercasedValueThatShouldBeNormalizedShouldReturnItAsExpected()
        {
            const string values = "0L2V";
            const string expect = "o1zu";
            Assert.AreEqual(expect, TpzBase32ConverterHelper.Normalize(values));
        }

        [Test]
        public void GivenEmptyShouldReturnEmpty()
        {
            const string expected = "";
            Assert.AreEqual(expected, TpzBase32ConverterHelper.Normalize(expected));
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TpzBase32ConverterHelper.Normalize(null));
        }
    }
}