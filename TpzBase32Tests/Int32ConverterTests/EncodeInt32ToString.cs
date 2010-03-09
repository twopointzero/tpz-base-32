using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.Int32ConverterTests
{
    [TestFixture]
    public class EncodeInt32ToString
    {
        private static void Encode(int input, string expected)
        {
            Assert.AreEqual(expected, Int32Converter.Encode(input));
        }

        [Test]
        public void GivenMaxValueShouldEncodeTo_999999b()
        {
            Encode(int.MaxValue, "999999b");
        }

        [Test]
        public void GivenMinValueShouldEncodeTo_yyyyyyn()
        {
            Encode(int.MinValue, "yyyyyyn");
        }

        [Test]
        public void GivenNegativeOneShouldEncodeTo_999999d()
        {
            Encode(-1, "999999d");
        }

        [Test]
        public void GivenOneShouldEncodeTo_b()
        {
            Encode(1, "b");
        }

        [Test]
        public void GivenThirtyTwoToTheFifthShouldEncodeTo_yyyyyb()
        {
            Encode((int)Math.Pow(32, 5), "yyyyyb");
        }

        [Test]
        public void GivenThirtyTwoToTheFirstShouldEncodeTo_yb()
        {
            Encode(32, "yb");
        }

        [Test]
        public void GivenThirtyTwoToTheFourthShouldEncodeTo_yyyyb()
        {
            Encode((int)Math.Pow(32, 4), "yyyyb");
        }

        [Test]
        public void GivenThirtyTwoToTheSecondShouldEncodeTo_yyb()
        {
            Encode((int)Math.Pow(32, 2), "yyb");
        }

        [Test]
        public void GivenThirtyTwoToTheSixthShouldEncodeTo_yyyyyyb()
        {
            Encode((int)Math.Pow(32, 6), "yyyyyyb");
        }

        [Test]
        public void GivenThirtyTwoToTheThirdShouldEncodeTo_yyyb()
        {
            Encode((int)Math.Pow(32, 3), "yyyb");
        }

        [Test]
        public void GivenZeroShouldEncodeTo_y()
        {
            Encode(0, "y");
        }

        [Test]
        public void Given_853561428_ShouldEncodeTo_wnwyq3()
        {
            Encode(853561428, "wnwyq3");
        }
    }
}