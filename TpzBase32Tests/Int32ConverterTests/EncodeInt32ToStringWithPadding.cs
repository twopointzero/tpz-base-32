using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.Int32ConverterTests
{
    [TestFixture]
    public class EncodeInt32ToStringWithPadding
    {
        private static void Encode(int input, string expected)
        {
            Assert.AreEqual(expected, Int32Converter.EncodeWithPadding(input));
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
        public void GivenOneShouldEncodeTo_byyyyyy()
        {
            Encode(1, "byyyyyy");
        }

        [Test]
        public void GivenThirtyTwoToTheFifthShouldEncodeTo_yyyyyby()
        {
            Encode((int)Math.Pow(32, 5), "yyyyyby");
        }

        [Test]
        public void GivenThirtyTwoToTheFirstShouldEncodeTo_ybyyyyy()
        {
            Encode(32, "ybyyyyy");
        }

        [Test]
        public void GivenThirtyTwoToTheFourthShouldEncodeTo_yyyybyy()
        {
            Encode((int)Math.Pow(32, 4), "yyyybyy");
        }

        [Test]
        public void GivenThirtyTwoToTheSecondShouldEncodeTo_yybyyyy()
        {
            Encode((int)Math.Pow(32, 2), "yybyyyy");
        }

        [Test]
        public void GivenThirtyTwoToTheSixthShouldEncodeTo_yyyyyyb()
        {
            Encode((int)Math.Pow(32, 6), "yyyyyyb");
        }

        [Test]
        public void GivenThirtyTwoToTheThirdShouldEncodeTo_yyybyyy()
        {
            Encode((int)Math.Pow(32, 3), "yyybyyy");
        }

        [Test]
        public void GivenZeroShouldEncodeTo_yyyyyyy()
        {
            Encode(0, "yyyyyyy");
        }

        [Test]
        public void Given_853561428_ShouldEncodeTo_wnwyq3y()
        {
            Encode(853561428, "wnwyq3y");
        }
    }
}