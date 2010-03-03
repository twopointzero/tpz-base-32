using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests
{
    [TestFixture]
    public class ConvertInt32ToBitEnumerable
    {
        [Test]
        public void GivenFortyTwoShouldReturnTheExpectedResult()
        {
            var expected = new bool[32];
            expected[1] = true;
            expected[3] = true;
            expected[5] = true;
            var actual = TpzBase32Converter.ConvertToBitEnumerable(42);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenMinusFortyTwoShouldReturnTheExpectedResult()
        {
            // two's complement results in the full bitflip (including the sign bit of course)
            // of one less than the abs value, so in this case: full bitflip of 41
            var expected = Enumerable.Repeat(true, 32).ToArray();
            expected[0] = false;
            expected[3] = false;
            expected[5] = false;
            var actual = TpzBase32Converter.ConvertToBitEnumerable(-42);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenMinusOneShouldReturnTheExpectedResult()
        {
            // all bits high including the high sign bit,
            // on account of Int32's two's complement representation
            var expected = Enumerable.Repeat(true, 32);
            var actual = TpzBase32Converter.ConvertToBitEnumerable(-1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenMinusTwoShouldReturnTheExpectedResult()
        {
            // all but the first bit high (including the high sign bit),
            // on account of Int32's two's complement representation
            var expected = Enumerable.Repeat(false, 1).Concat(Enumerable.Repeat(true, 31));
            var actual = TpzBase32Converter.ConvertToBitEnumerable(-2);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenOneShouldReturnTheExpectedResult()
        {
            var expected = new bool[32];
            expected[0] = true;
            var actual = TpzBase32Converter.ConvertToBitEnumerable(1);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenTwoShouldReturnTheExpectedResult()
        {
            var expected = new bool[32];
            expected[1] = true;
            var actual = TpzBase32Converter.ConvertToBitEnumerable(2);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GivenZeroShouldReturnTheExpectedResult()
        {
            var expected = Enumerable.Repeat(false, 32);
            var actual = TpzBase32Converter.ConvertToBitEnumerable(0);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void ShouldReturn32Values()
        {
            var actual = TpzBase32Converter.ConvertToBitEnumerable(0);
            Assert.AreEqual(32, actual.Count());
        }

        [Test]
        public void ShouldReturnANonNullResult()
        {
            var actual = TpzBase32Converter.ConvertToBitEnumerable(0);
            Assert.IsNotNull(actual);
        }
    }
}