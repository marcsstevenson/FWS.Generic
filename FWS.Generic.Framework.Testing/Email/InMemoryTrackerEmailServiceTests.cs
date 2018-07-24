using System;
using System.Net.Mail;
using FWS.Generic.Email.MockService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FWS.Generic.Framework.Testing.Email
{
    [TestClass]
    public class InMemoryTrackerEmailServiceTests
    {
        [TestMethod]
        public void DeliveringShallTrackCorrectly()
        {
            //Setup
            var inMemoryTrackerEmailService = new InMemoryTrackerEmailService{ApiKey = "Blah"};
            const string trackingId = "imanemailaddress@email.com";
            var mailMessage = new MailMessage { From = new MailAddress(trackingId) };
            
            //Exercise
            inMemoryTrackerEmailService.Deliver(mailMessage);

            //Verify
            Assert.IsTrue(inMemoryTrackerEmailService.SentMailMessages.Count == 1, "InMemoryTrackerEmailService is tracking an incorrect number of email messages");
            Assert.IsTrue(inMemoryTrackerEmailService.SentMailMessages[0].From.Address == trackingId, "InMemoryTrackerEmailService is tracking the correct from address");
        }
    }
}
