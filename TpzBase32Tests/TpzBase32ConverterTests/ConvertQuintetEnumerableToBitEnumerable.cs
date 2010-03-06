using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests
{
    [TestFixture]
    public class ConvertQuintetEnumerableToBitEnumerable
    {
        [Test]
        public void GivenAsEmptyEnumerableShouldReturnEmptyEnumerable()
        {
            CollectionAssert.IsEmpty(TpzBase32Converter.ConvertQuintetsToBitEnumerable(Enumerable.Empty<byte>()));
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TpzBase32Converter.ConvertQuintetsToBitEnumerable(null));
        }

        [Test]
        public void GivenOneQuintetShouldReturnTheFiveExpectedBooleanValues()
        {
            var input = new byte[] { 2 };
            var expected = new[] { false, true, false, false, false };
            var actual = TpzBase32Converter.ConvertQuintetsToBitEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThreeQuintetsShouldReturnTheFifteenExpectedBooleanValues()
        {
            var input = new byte[] { 26, 11, 31 };
            var expected = new[]
                               {
                                   false, true, false, true, true, true, true, false, true, false, true, true, true, true,
                                   true
                               };
            var actual = TpzBase32Converter.ConvertQuintetsToBitEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenTwoQuintetsShouldReturnTheTenExpectedBooleanValues()
        {
            var input = new byte[] { 26, 11 };
            var expected = new[] { false, true, false, true, true, true, true, false, true, false };
            var actual = TpzBase32Converter.ConvertQuintetsToBitEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}