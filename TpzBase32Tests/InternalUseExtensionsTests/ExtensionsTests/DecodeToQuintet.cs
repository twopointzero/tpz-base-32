using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class DecodeToQuintet
    {
        [Test]
        public void GivenACharacterInTheEncodingShouldReturnTheExpectedValue()
        {
            var values = Constants.EncodingAlphabet.ToArray();
            for (byte i = 0; i < values.Length; i++)
            {
                char input = values[i];
                byte expected = i;
                Assert.AreEqual(expected, input.DecodeToQuintet());
            }
        }

        [Test]
        public void GivenACharacterNotInTheEncodingShouldThrowArgumentOutOfRangeException()
        {
            const char input = '!';
            Assert.Throws<ArgumentOutOfRangeException>(() => input.DecodeToQuintet());
        }
    }
}