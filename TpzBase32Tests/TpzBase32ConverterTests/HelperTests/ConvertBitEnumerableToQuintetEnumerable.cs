using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests.HelperTests
{
    [TestFixture]
    public class ConvertBitEnumerableToQuintetEnumerable
    {
        [Test]
        public void GivenElevenValuesShouldReturnAnEnumerableThatProducesTheThreeExpectedResults()
        {
            var expected = new byte[] { 26, 27, 0 };
            var input = new[] { false, true, false, true, true, true, true, false, true, true, false };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenEmptyShouldReturnAnEnumerableThatProducesNoResults()
        {
            CollectionAssert.IsEmpty(TpzBase32Converter.Helper.ConvertToQuintetEnumerable(Enumerable.Empty<bool>()));
        }

        [Test]
        public void GivenFalseAndTrueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOffSecondBitOn()
        {
            var expected = new byte[] { 2 };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(new[] { false, true });
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenFiveValuesShouldReturnAnEnumerableThatProducesTheOneExpectedResult()
        {
            var expected = new byte[] { 26 };
            var input = new[] { false, true, false, true, true };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenFourValuesShouldReturnAnEnumerableThatProducesTheOneExpectedResult()
        {
            var expected = new byte[] { 10 };
            var input = new[] { false, true, false, true, };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenNineValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var expected = new byte[] { 26, 11 };
            var input = new[] { false, true, false, true, true, true, true, false, true };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TpzBase32Converter.Helper.ConvertToQuintetEnumerable(null));
        }

        [Test]
        public void GivenOneFalseValueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOff()
        {
            var expected = new byte[] { 0 };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(new[] { false });
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenOneTrueValueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOn()
        {
            var expected = new byte[] { 1 };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(new[] { true });
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenSixValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var expected = new byte[] { 26, 1 };
            var input = new[] { false, true, false, true, true, true };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenTenValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var expected = new byte[] { 26, 27 };
            var input = new[] { false, true, false, true, true, true, true, false, true, true };
            var actual = TpzBase32Converter.Helper.ConvertToQuintetEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}