using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class NormalizeCharToTpzBase32Alphabet
    {
        private static void NormalizeToTpzBase32Alphabet(string values, string expecteds)
        {
            for (int i = 0; i < values.Length; i++)
            {
                char input = values[i];
                char expected = expecteds[i];
                char actual = input.NormalizeToTpzBase32Alphabet();
                string message = string.Format("Expected {0} but actual was {1}. Input was {2}", expected, actual, input);
                Assert.AreEqual(expected, actual, message);
            }
        }

        [Test]
        public void GivenAValueEntirelyOutsideTheCharacterSetShouldReturnIt()
        {
            const string values = "!@#$";
            NormalizeToTpzBase32Alphabet(values, values);
        }

        [Test]
        public void GivenAValueInTheEncodingAlphabetShouldReturnIt()
        {
            const string values = Constants.EncodingAlphabet;
            NormalizeToTpzBase32Alphabet(values, values);
        }

        [Test]
        public void GivenAValueThatShouldBeNormalizeToTpzBase32AlphabetdShouldReturnItAsExpected()
        {
            NormalizeToTpzBase32Alphabet("0l2v", "o1zu");
        }

        [Test]
        public void GivenAnUppercasedValueInTheEncodingAlphabetShouldReturnItLowercased()
        {
            string values = Constants.EncodingAlphabet.ToUpper();
            const string expecteds = Constants.EncodingAlphabet;
            NormalizeToTpzBase32Alphabet(values, expecteds);
        }

        [Test]
        public void GivenAnUppercasedValueThatShouldBeNormalizeToTpzBase32AlphabetdShouldReturnItAsExpected()
        {
            NormalizeToTpzBase32Alphabet("0L2V", "o1zu");
        }
    }
}