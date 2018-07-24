using System.Collections.Generic;
using System.Net.Mail;
using FWS.Generic.Email.Interfaces;
using FWS.Generic.Email.Services;
using FWS.Generic.Email.Services.Models;

namespace FWS.Generic.Email.MockService 
{
    public class InMemoryTrackerEmailService : BaseEmailService, IEmailService
    {
        public List<MailMessage> SentMailMessages { get; set; }

        public DeliverResponse Deliver(MailMessage mailMessage)
        {
            ValidateBeforeDelivery(mailMessage);

            SentMailMessages = SentMailMessages ?? new List<MailMessage>();

            //Add this message to our tracking list
            this.SentMailMessages.Add(mailMessage);

            return new DeliverResponse
            {
                StatusCode = "InMemory",
                Success = true
            };
        }
    }
}