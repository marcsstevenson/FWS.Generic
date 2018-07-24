using System.Net.Mail;
using FWS.Generic.Email.Services.Models;

namespace FWS.Generic.Email.Interfaces
{
    public interface IEmailService
    {
        string ApiKey { get; set; }
        
        /// <summary>
        /// This setting will override all email sent by an email service to this email address (for testing purposes). Set to blank to remove
        /// </summary>
        string ToAddressOverride { get; set; }

        bool HasToAddressOverride { get; }

        DeliverResponse Deliver(MailMessage mailMessage);
    }
}