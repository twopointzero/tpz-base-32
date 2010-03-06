using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests
{
    [TestFixture]
    public class EncodeInt32ToString
    {
        [Test]
        public void GivenMaxValueShouldEncodeTo_999999b()
        {
            Assert.AreEqual("999999b", TpzBase32Converter.Encode(int.MaxValue));
        }

        [Test]
        public void GivenMinValueShouldEncodeTo_yyyyyyn()
        {
            Assert.AreEqual("yyyyyyn", TpzBase32Converter.Encode(int.MinValue));
        }

        [Test]
        public void GivenNegativeOneShouldEncodeTo_999999d()
        {
            Assert.AreEqual("999999d", TpzBase32Converter.Encode(-1));
        }

        [Test]
        public void GivenOneShouldEncodeTo_b()
        {
            Assert.AreEqual("b", TpzBase32Converter.Encode(1));
        }

        [Test]
        public void GivenThirtyTwoToTheFifthShouldEncodeTo_yyyyyb()
        {
            Assert.AreEqual("yyyyyb", TpzBase32Converter.Encode((int)Math.Pow(32, 5)));
        }

        [Test]
        public void GivenThirtyTwoToTheFirstShouldEncodeTo_yb()
        {
            Assert.AreEqual("yb", TpzBase32Converter.Encode(32));
        }

        [Test]
        public void GivenThirtyTwoToTheFourthShouldEncodeTo_yyyyb()
        {
            Assert.AreEqual("yyyyb", TpzBase32Converter.Encode((int)Math.Pow(32, 4)));
        }

        [Test]
        public void GivenThirtyTwoToTheSecondShouldEncodeTo_yyb()
        {
            Assert.AreEqual("yyb", TpzBase32Converter.Encode((int)Math.Pow(32, 2)));
        }

        [Test]
        public void GivenThirtyTwoToTheSixthShouldEncodeTo_yyyyyyb()
        {
            Assert.AreEqual("yyyyyyb", TpzBase32Converter.Encode((int)Math.Pow(32, 6)));
        }

        [Test]
        public void GivenThirtyTwoToTheThirdShouldEncodeTo_yyyb()
        {
            Assert.AreEqual("yyyb", TpzBase32Converter.Encode((int)Math.Pow(32, 3)));
        }

        [Test]
        public void GivenZeroShouldEncodeTo_y()
        {
            Assert.AreEqual("y", TpzBase32Converter.Encode(0));
        }
    }
}