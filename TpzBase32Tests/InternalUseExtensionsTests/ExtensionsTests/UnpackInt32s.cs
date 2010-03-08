using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32.InternalUseExtensions;

namespace twopointzero.TpzBase32Tests.InternalUseExtensionsTests.ExtensionsTests
{
    [TestFixture]
    public class UnpackInt32s
    {
        [Test]
        public void GivenFortyTwoShouldReturnTheExpectedResult()
        {
            const int input = 42;
            var expected = new bool[32];
            expected[1] = true;
            expected[3] = true;
            expected[5] = true;
            var actual = input.Unpack();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenMinusFortyTwoShouldReturnTheExpectedResult()
        {
            const int input = -42;

            // two's complement results in the full bitflip (including the sign bit of course)
            // of one less than the abs value, so in this case: full bitflip of 41
            var expected = Enumerable.Repeat(true, 32).ToArray();
            expected[0] = false;
            expected[3] = false;
            expected[5] = false;
            var actual = input.Unpack();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenMinusOneShouldReturnTheExpectedResult()
        {
            const int input = -1;

            // all bits high including the high sign bit,
            // on account of Int32's two's complement representation
            var expected = Enumerable.Repeat(true, 32);
            var actual = input.Unpack();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenMinusTwoShouldReturnTheExpectedResult()
        {
            const int input = -2;

            // all but the first bit high (including the high sign bit),
            // on account of Int32's two's complement representation
            var expected = Enumerable.Repeat(false, 1).Concat(Enumerable.Repeat(true, 31));
            var actual = input.Unpack();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenOneShouldReturnTheExpectedResult()
        {
            const int input = 1;
            var expected = new bool[32];
            expected[0] = true;
            var actual = input.Unpack();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenTwoShouldReturnTheExpectedResult()
        {
            const int input = 2;
            var expected = new bool[32];
            expected[1] = true;
            var actual = input.Unpack();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenZeroShouldReturnTheExpectedResult()
        {
            const int input = 0;
            var expected = Enumerable.Repeat(false, 32);
            var actual = input.Unpack();
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldReturn32Values()
        {
            const int input = 0;
            const int expected = 32;
            var actual = input.Unpack();
            Assert.AreEqual(expected, actual.Count());
        }

        [Test]
        public void ShouldReturnANonNullResult()
        {
            const int input = 0;
            var actual = input.Unpack();
            Assert.IsNotNull(actual);
        }
    }
}