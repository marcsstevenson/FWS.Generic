using System;
using FWS.Generic.Framework.Helpers.String;
using FWS.Generic.Framework.Helpers.String.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.String
{
    [TestClass]
    public class StringHelperTests
    {
        [TestMethod]
        public void IsNumericOnlyShallReturnTrueForStringContainingOnlyNumericWithRemoveWhitespaceFalse()
        {
            Assert.IsTrue("123".IsNumericOnly(false));
        }

        [TestMethod]
        public void IsNumericOnlyShallReturnTrueForStringContainingOnlyNumericWithRemoveWhitespaceTrue()
        {
            Assert.IsTrue("123".IsNumericOnly(true));
        }

        [TestMethod]
        public void IsNumericOnlyShallReturnFalseForStringContainingWihtespaceWithRemoveWhitespaceFalse()
        {
            Assert.IsFalse("1 23".IsNumericOnly(false));
        }

        [TestMethod]
        public void IsNumericOnlyShallReturnTrueForStringContainingOnlyNumericAndWihtespaceWithRemoveWhitespaceTrue()
        {
            Assert.IsTrue("1 23".IsNumericOnly(true));
        }

        [TestMethod]
        public void IsNumericOnlyShallReturnTrueForStringContainingOnlyNumericAndWihtespaceAndNewLineWithRemoveWhitespaceTrue()
        {
            Assert.IsTrue("1 2\r\n3".IsNumericOnly(true));
        }

        [TestMethod]
        public void RemoveSpecialCharactersShallRemoveComma()
        {
            //Setup
            var input = "some,thing";
            var expectedOutput = "something";

            //Exercise
            var output = StringHelper.RemoveSpecialCharacters(input);

            //Verify
            Assert.AreEqual(0, string.CompareOrdinal(expectedOutput, output));
        }

        [TestMethod]
        public void RemoveSpecialCharactersShallRemoveUber()
        {
            //Setup
            var input = "Über";
            var expectedOutput = "ber";

            //Exercise
            var output = StringHelper.RemoveSpecialCharacters(input);

            //Verify
            Assert.AreEqual(0, string.CompareOrdinal(expectedOutput, output));
        }

        [TestMethod]
        public void RemoveSpecialCharactersShallRemoveQuote()
        {
            //Setup
            var input = "O'Gorman";
            var expectedOutput = "OGorman";

            //Exercise
            var output = StringHelper.RemoveSpecialCharacters(input);

            //Verify
            Assert.AreEqual(0, string.CompareOrdinal(expectedOutput, output));
        }

        [TestMethod]
        public void RemoveSpecialCharactersKeepSpaceShallNotRemoveSpace()
        {
            //Setup
            var input = "Some thing";
            var expectedOutput = input;

            //Exercise
            var output = StringHelper.RemoveSpecialCharactersKeepSpace(input);

            //Verify
            Assert.AreEqual(0, string.CompareOrdinal(expectedOutput, output));
        }
    }
}
