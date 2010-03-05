﻿using NUnit.Framework;

namespace twopointzero.TpzBase32Tests
{
    [TestFixture]
    public class ZBase32Alphabet
    {
        public const string Value = "ybndrfg8ejkmcpqxot1uwisza345h769";

        [Test]
        public void ConfirmThatTheTestProjectCodeHasTheCorrectAlphabet()
        {
            // A seemingly silly test, but I've seen typos creep into codebases
            // on enough occasions to know that silly can still be valuable.
            Assert.AreEqual("ybndrfg8ejkmcpqxot1uwisza345h769", Value);
        }
    }
}