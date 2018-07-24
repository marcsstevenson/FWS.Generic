using System;
using System.Net.Mail;
using FWS.Generic.Email.MockService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Email
{
    [TestClass]
    public class BaseEmailServiceTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeliveringWithoutSmtpUsernameShallThrowArgumentException()
        {
            //Setup
            var inMemoryTrackerEmailService = new InMemoryTrackerEmailService();
            var mailMessage = new MailMessage {From = new MailAddress("from@something.com")};

            //Exercise
            inMemoryTrackerEmailService.Deliver(mailMessage);

            //Verify
            //Assert - Expects exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeliveringWithoutSmtpPasswordShallThrowArgumentException()
        {
            //Setup
            var inMemoryTrackerEmailService = new InMemoryTrackerEmailService();
            var mailMessage = new MailMessage { From = new MailAddress("from@something.com") };

            //Exercise
            inMemoryTrackerEmailService.Deliver(mailMessage);

            //Verify
            //Assert - Expects exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DeliveringWithoutFromAddressShallThrowArgumentException()
        {
            //Setup
            var inMemoryTrackerEmailService = new InMemoryTrackerEmailService();
            var mailMessage = new MailMessage { From = new MailAddress("") };

            //Exercise
            inMemoryTrackerEmailService.Deliver(mailMessage);

            //Verify
            //Assert - Expects exception
        }
    }
}
