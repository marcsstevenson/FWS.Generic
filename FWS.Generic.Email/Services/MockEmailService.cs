using System.Collections.Generic;
using System.Net.Mail;
using FWS.Generic.Email.Interfaces;
using FWS.Generic.Email.Services.Models;

namespace FWS.Generic.Email.Services
{
    public class MockEmailService : BaseEmailService, IEmailService
    {
        public MockEmailService()
        {
            this.ApiKey = "MockKey";
        }

        public string SmtpServerAddress { get; set; }

        private List<MailMessage> _mailMessagesSent;

        public List<MailMessage> MailMessagesSent
        {
            get
            {
                return this._mailMessagesSent ?? (_mailMessagesSent = new List<MailMessage>());
            }
        }

        public DeliverResponse Deliver(MailMessage mailMessage)
        {
            ValidateBeforeDelivery(mailMessage);

            this.MailMessagesSent.Add(mailMessage);

            return new DeliverResponse
            {
                StatusCode = "Mocked", Success = true
            };
        }
    }
}