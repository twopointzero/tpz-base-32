using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class PackToInt32s
    {
        [Test]
        public void GivenEmptyShouldReturnAnEnumerableThatProducesNoResults()
        {
            var input = Enumerable.Empty<bool>();
            CollectionAssert.IsEmpty(input.PackToInt32s());
        }

        [Test]
        public void GivenFalseAndTrueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOffSecondBitOn()
        {
            var input = new[] { false, true };
            var expected = new[] { 2 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            IEnumerable<bool> input = null;
            Assert.Throws<ArgumentNullException>(() => input.PackToInt32s());
        }

        [Test]
        public void GivenOneFalseValueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOff()
        {
            var input = new[] { false };
            var expected = new[] { 0 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenOneTrueValueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOn()
        {
            var input = new[] { true };
            var expected = new[] { 1 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenSixtyFiveValuesShouldReturnAnEnumerableThatProducesTheThreeExpectedResults()
        {
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
            var expected = new[] { 1431655765, 1431655764, 1 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenSixtyFourValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
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
            var expected = new[] { 1431655765, 1431655764 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenSixtyThreeValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
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
            var expected = new[] { -1431655765, 1431655765 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThirtyOneValuesShouldReturnAnEnumerableThatProducesTheOneExpectedPositiveResult()
        {
            var input = new[]
                            {
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true
                            };
            var expected = new[] { 1431655765 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThirtyThreeValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var input = new[]
                            {
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true
                            };
            var expected = new[] { 1431655765, 1 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThirtyTwoValuesShouldReturnAnEnumerableThatProducesTheOneExpectedNegativeResult()
        {
            var input = new[]
                            {
                                true, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true,
                                false, true, false, true, false, true, false, true
                            };
            var expected = new[] { -1431655765 };
            var actual = input.PackToInt32s().ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThirtyTwoValuesShouldReturnAnEnumerableThatProducesTheOneExpectedPositiveResult()
        {
            var input = new[]
                            {
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false,
                                true, false, true, false, true, false, true, false
                            };
            var expected = new[] { 1431655765 };
            var actual = input.PackToInt32s();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}