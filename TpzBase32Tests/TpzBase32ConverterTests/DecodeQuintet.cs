﻿using System;
using System.Linq;
using NUnit.Framework;
using twopointzero.TpzBase32;

namespace twopointzero.TpzBase32Tests.TpzBase32ConverterTests
{
    [TestFixture]
    public class DecodeQuintet
    {
        [Test]
        public void GivenACharacterInTheEncodingShouldReturnTheExpectedValue()
        {
            var values = ZBase32Alphabet.Value.ToArray();
            for (byte i = 0; i < values.Length; i++)
            {
                Assert.AreEqual(i, TpzBase32Converter.DecodeQuintet(values[i]));
            }
        }

        [Test]
        public void GivenACharacterNotInTheEncodingShouldThrowArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => TpzBase32Converter.DecodeQuintet('!'));
        }
    }
}