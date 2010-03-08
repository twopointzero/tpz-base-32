using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class PackToQuintets
    {
        [Test]
        public void GivenElevenValuesShouldReturnAnEnumerableThatProducesTheThreeExpectedResults()
        {
            var input = new[]
                            {
                                false, true, false, true, true,
                                true, true, false, true, true,
                                false
                            };
            var expected = new byte[] { 26, 27, 0 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenEmptyShouldReturnAnEnumerableThatProducesNoResults()
        {
            CollectionAssert.IsEmpty(Enumerable.Empty<bool>().PackToQuintets());
        }

        [Test]
        public void GivenFalseAndTrueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOffSecondBitOn()
        {
            var input = new[] { false, true };
            var expected = new byte[] { 2 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenFiveValuesShouldReturnAnEnumerableThatProducesTheOneExpectedResult()
        {
            var input = new[] { false, true, false, true, true };
            var expected = new byte[] { 26 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenFourValuesShouldReturnAnEnumerableThatProducesTheOneExpectedResult()
        {
            var input = new[] { false, true, false, true, };
            var expected = new byte[] { 10 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenNineValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var input = new[]
                            {
                                false, true, false, true, true,
                                true, true, false, true
                            };
            var expected = new byte[] { 26, 11 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            IEnumerable<bool> input = null;
            Assert.Throws<ArgumentNullException>(() => input.PackToQuintets());
        }

        [Test]
        public void GivenOneFalseValueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOff()
        {
            var input = new[] { false };
            var expected = new byte[] { 0 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenOneTrueValueShouldReturnAnEnumerableThatProducesOneResultWithTheLowBitOn()
        {
            var input = new[] { true };
            var expected = new byte[] { 1 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenSixValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var input = new[]
                            {
                                false, true, false, true, true,
                                true
                            };
            var expected = new byte[] { 26, 1 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenTenValuesShouldReturnAnEnumerableThatProducesTheTwoExpectedResults()
        {
            var input = new[]
                            {
                                false, true, false, true, true,
                                true, true, false, true, true
                            };
            var expected = new byte[] { 26, 27 };
            var actual = input.PackToQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}