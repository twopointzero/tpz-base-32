using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests.HelperTests
{
    [TestFixture]
    public class ConvertBitEnumerableToIntEnumerable
    {
        [Test]
        public void GivenEmptyShouldReturnAnEnumerableThatProducesNoResults()
        {
            CollectionAssert.IsEmpty(TpzBase32Converter.Helper.ConvertToIntEnumerable(Enumerable.Empty<bool>()));
        }

        [Test]
        public void GivenFalseAndTrueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOffSecondBitOn()
        {
            var expected = new[] { 2 };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(new[] { false, true });
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => TpzBase32Converter.Helper.ConvertToIntEnumerable(null));
        }

        [Test]
        public void GivenOneFalseValueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOff()
        {
            var expected = new[] { 0 };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(new[] { false });
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenOneTrueValueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOn()
        {
            var expected = new[] { 1 };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(new[] { true });
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenSixtyFiveValuesShouldReturnAnEnumerableThatProducesTheThreeExpectedResults()
        {
            var expected = new[] { 1431655765, 1431655764, 1 };
            var input = new[]
                            {
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                false, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true
                            };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenSixtyFourValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var expected = new[] { 1431655765, 1431655764 };
            var input = new[]
                            {
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                false, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false
                            };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenSixtyThreeValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var expected = new[] { -1431655765, 1431655765 };
            var input = new[]
                            {
                                true, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true
                            };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThirtyOneValuesShouldReturnAnEnumerableThatProducesTheOneExpectedPositiveResult()
        {
            var expected = new[] { 1431655765 };
            var input = new[]
                            {
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true
                            };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThirtyThreeValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var expected = new[] { 1431655765, 1 };
            var input = new[]
                            {
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true
                            };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThirtyTwoValuesShouldReturnAnEnumerableThatProducesTheOneExpectedNegativeResult()
        {
            var expected = new[] { -1431655765 };
            var input = new[]
                            {
                                true, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true
                            };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(input).ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThirtyTwoValuesShouldReturnAnEnumerableThatProducesTheOneExpectedPositiveResult()
        {
            var expected = new[] { 1431655765 };
            var input = new[]
                            {
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false
                            };
            var actual = TpzBase32Converter.Helper.ConvertToIntEnumerable(input);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}