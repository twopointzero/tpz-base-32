using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests
{
    [TestFixture]
    public class EncodeInt32ToStringWithPadding
    {
        [Test]
        public void GivenMaxValueShouldEncodeTo_999999b()
        {
            Assert.AreEqual("999999b", TpzBase32Converter.EncodeWithPadding(int.MaxValue));
        }

        [Test]
        public void GivenMinValueShouldEncodeTo_yyyyyyn()
        {
            Assert.AreEqual("yyyyyyn", TpzBase32Converter.EncodeWithPadding(int.MinValue));
        }

        [Test]
        public void GivenNegativeOneShouldEncodeTo_byyyyyy()
        {
            Assert.AreEqual("999999d", TpzBase32Converter.EncodeWithPadding(-1));
        }

        [Test]
        public void GivenOneShouldEncodeTo_byyyyyy()
        {
            Assert.AreEqual("byyyyyy", TpzBase32Converter.EncodeWithPadding(1));
        }

        [Test]
        public void GivenThirtyTwoToTheFifthShouldEncodeTo_yyyyyby()
        {
            Assert.AreEqual("yyyyyby", TpzBase32Converter.EncodeWithPadding((int)Math.Pow(32, 5)));
        }

        [Test]
        public void GivenThirtyTwoToTheFirstShouldEncodeTo_ybyyyyy()
        {
            Assert.AreEqual("ybyyyyy", TpzBase32Converter.EncodeWithPadding(32));
        }

        [Test]
        public void GivenThirtyTwoToTheFourthShouldEncodeTo_yyyybyy()
        {
            Assert.AreEqual("yyyybyy", TpzBase32Converter.EncodeWithPadding((int)Math.Pow(32, 4)));
        }

        [Test]
        public void GivenThirtyTwoToTheSecondShouldEncodeTo_yybyyyy()
        {
            Assert.AreEqual("yybyyyy", TpzBase32Converter.EncodeWithPadding((int)Math.Pow(32, 2)));
        }

        [Test]
        public void GivenThirtyTwoToTheSixthShouldEncodeTo_yyyyyyb()
        {
            Assert.AreEqual("yyyyyyb", TpzBase32Converter.EncodeWithPadding((int)Math.Pow(32, 6)));
        }

        [Test]
        public void GivenThirtyTwoToTheThirdShouldEncodeTo_yyybyyy()
        {
            Assert.AreEqual("yyybyyy", TpzBase32Converter.EncodeWithPadding((int)Math.Pow(32, 3)));
        }

        [Test]
        public void GivenZeroShouldEncodeTo_yyyyyyy()
        {
            Assert.AreEqual("yyyyyyy", TpzBase32Converter.EncodeWithPadding(0));
        }
    }
}