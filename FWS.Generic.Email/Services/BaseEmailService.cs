using System;
using System.Net.Mail;

namespace FWS.Generic.Email.Services
{
    public abstract class BaseEmailService
    {
        public string ApiKey { get; set; }

        //public string SmtpUserName { get; set; }
        //public string SmtpPassword { get; set; }

        public string ToAddressOverride { get; set; }

        public bool HasToAddressOverride => this.ToAddressOverride != null;

        public void ValidateBeforeDelivery(MailMessage mailMessage)
        {
            if (string.IsNullOrEmpty(this.ApiKey))
                throw new ArgumentException("ApiKey must me set before delivering an email");
            
            if (mailMessage.From == null || string.IsNullOrEmpty(mailMessage.From.Address))
                throw new ArgumentException("mailMessage.From must me set before delivering an email");
        }
    }
}