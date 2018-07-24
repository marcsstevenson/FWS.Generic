using System;
using FWS.Generic.Framework.Helpers.CreditCard;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Helpers.CreditCard
{
    [TestClass]
    public class CreditCardHelperTests
    {
        public const string ValidMasterCardNumber = "5555555555554444";
        public const string ValidViasCardNumber = "4111111111111111";

        [TestMethod]
        public void AValidMasterCardNumberShallTestAsValid()
        {
            //Exercise
            var isValid = CreditCardHelper.CheckIfCreditCardNumberIsValid(ValidMasterCardNumber);

            //Verify
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AnInvalidMasterCardNumberShallTestAsInValid()
        {
            //Setup
            const string creditCardNumber = "1235555555554444";

            //Exercise
            var isValid = CreditCardHelper.CheckIfCreditCardNumberIsValid(creditCardNumber);

            //Verify
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void AValidVisaNumberShallTestAsValid()
        {
            //Exercise
            var isValid = CreditCardHelper.CheckIfCreditCardNumberIsValid(ValidViasCardNumber);

            //Verify
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void AnInvalidVisaNumberShallTestAsInValid()
        {
            //Setup
            const string creditCardNumber = "9991111111111111";

            //Exercise
            var isValid = CreditCardHelper.CheckIfCreditCardNumberIsValid(creditCardNumber);

            //Verify
            Assert.IsFalse(isValid);
        }
    }
}
