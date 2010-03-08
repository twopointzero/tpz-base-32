using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class UnpackQuintets
    {
        [Test]
        public void GivenAsEmptyEnumerableShouldReturnEmptyEnumerable()
        {
            var input = Enumerable.Empty<byte>();
            CollectionAssert.IsEmpty(input.UnpackQuintets());
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            IEnumerable<byte> input = null;
            Assert.Throws<ArgumentNullException>(() => input.UnpackQuintets());
        }

        [Test]
        public void GivenOneQuintetShouldReturnTheFiveExpectedBooleanValues()
        {
            var input = new byte[] { 2 };
            var expected = new[] { false, true, false, false, false };
            var actual = input.UnpackQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenThreeQuintetsShouldReturnTheFifteenExpectedBooleanValues()
        {
            var input = new byte[] { 26, 11, 31 };
            var expected = new[]
                               {
                                   false, true, false, true, true,
                                   true, true, false, true, false,
                                   true, true, true, true, true
                               };
            var actual = input.UnpackQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenTwoQuintetsShouldReturnTheTenExpectedBooleanValues()
        {
            var input = new byte[] { 26, 11 };
            var expected = new[]
                               {
                                   false, true, false, true, true,
                                   true, true, false, true, false
                               };
            var actual = input.UnpackQuintets();
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}