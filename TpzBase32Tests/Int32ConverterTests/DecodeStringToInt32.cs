using System;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.Int32ConverterTests
{
    [TestFixture]
    public class DecodeStringToInt32
    {
        private static void Decode(string input, int expected)
        {
            Assert.AreEqual(expected, Int32Converter.DecodeToInt32(input));
        }

        [Test]
        public void GivenEmptyShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Int32Converter.DecodeToInt32(string.Empty));
        }

        [Test]
        public void GivenNullShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Int32Converter.DecodeToInt32(null));
        }

        [Test]
        public void GivenOverlongButOtherwiseValidShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Int32Converter.DecodeToInt32("yyyyyyyyyyy"));
        }

        [Test]
        public void GivenSupportedLengthButOtherwiseInvalidShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Int32Converter.DecodeToInt32("yy!yyyy"));
        }

        [Test]
        public void Given_0L2V_ShouldDecodeTo_646736()
        {
            Decode("0L2V", 646736);
        }

        [Test]
        public void Given_999999b_ShouldDecodeTo_MaxValue()
        {
            Decode("999999b", int.MaxValue);
        }

        [Test]
        public void Given_999999d_ShouldDecodeTo_NegativeOne()
        {
            Decode("999999d", -1);
        }

        [Test]
        public void Given_b_ShouldDecodeTo_1()
        {
            Decode("b", 1);
        }

        [Test]
        public void Given_byyyyyy_ShouldDecodeTo_1()
        {
            Decode("byyyyyy", 1);
        }

        [Test]
        public void Given_y_ZeroShouldDecodeTo_0()
        {
            Decode("y", 0);
        }

        [Test]
        public void Given_yb_ShouldDecodeTo_ThirtyTwoToTheFirst()
        {
            Decode("yb", 32);
        }

        [Test]
        public void Given_ybyyyyy_ShouldDecodeTo_ThirtyTwoToTheFirst()
        {
            Decode("ybyyyyy", 32);
        }

        [Test]
        public void Given_yyb_ShouldDecodeTo_ThirtyTwoToTheSecond()
        {
            Decode("yyb", (int)Math.Pow(32, 2));
        }

        [Test]
        public void Given_yybyyyy_ShouldDecodeTo_ThirtyTwoToTheSecond()
        {
            Decode("yybyyyy", (int)Math.Pow(32, 2));
        }

        [Test]
        public void Given_yyyb_ShouldDecodeTo_ThirtyTwoToTheThird()
        {
            Decode("yyyb", (int)Math.Pow(32, 3));
        }

        [Test]
        public void Given_yyybyyy_ShouldDecodeTo_ThirtyTwoToTheThird()
        {
            Decode("yyybyyy", (int)Math.Pow(32, 3));
        }

        [Test]
        public void Given_yyyybyy_ShouldDecodeTo_ThirtyTwoToTheFourth()
        {
            Decode("yyyybyy", (int)Math.Pow(32, 4));
        }

        [Test]
        public void Given_yyyyyb_ShouldDecodeTo_ThirtyTwoToTheFifth()
        {
            Decode("yyyyyb", (int)Math.Pow(32, 5));
        }

        [Test]
        public void Given_yyyyyby_ShouldDecodeTo_ThirtyTwoToTheFifth()
        {
            Decode("yyyyyby", (int)Math.Pow(32, 5));
        }

        [Test]
        public void Given_yyyyyyb_ShouldDecodeTo_ThirtyTwoToTheSixth()
        {
            Decode("yyyyyyb", (int)Math.Pow(32, 6));
        }

        [Test]
        public void Given_yyyyyyn_ShouldDecodeTo_MinValue()
        {
            Decode("yyyyyyn", int.MinValue);
        }

        [Test]
        public void Given_yyyyyyy_ZeroShouldDecodeTo_0()
        {
            Decode("yyyyyyy", 0);
        }
    }
}