using FWS.Generic.Email.Interfaces;
using FWS.Generic.Email.Services.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.IO;
using System.Net.Mail;

namespace FWS.Generic.Email.Services
{
    public class SendGridRestEmailService : BaseEmailService, IEmailService
    {
        public string SmtpServerAddress { get; set; }

        public virtual DeliverResponse Deliver(MailMessage mailMessage)
        {
            ValidateBeforeDelivery(mailMessage);

            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress(mailMessage.From.Address)
            };
            
            //Override the to addresses if needed
            if (this.HasToAddressOverride)
            {
                var overrideMailAddresses = new[] { new MailAddress(this.ToAddressOverride) };
                
                foreach (var overrideMailAddress in overrideMailAddresses)
                    sendGridMessage.AddTo(overrideMailAddress.Address);
            }
            else
            {
                foreach (var to in mailMessage.To)
                    sendGridMessage.AddTo(to.Address);

                foreach (var cc in mailMessage.CC)
                    sendGridMessage.AddCc(cc.Address);

                foreach (var bcc in mailMessage.Bcc)
                    sendGridMessage.AddBcc(bcc.Address);
            }

            sendGridMessage.Subject = mailMessage.Subject;

            if (mailMessage.IsBodyHtml)
                sendGridMessage.HtmlContent = mailMessage.Body;
            else
                sendGridMessage.PlainTextContent = mailMessage.Body;

            //Add the attachments if any
            foreach (var attachment in mailMessage.Attachments)
            {
                Byte[] attachmentBytes = StreamToBytes(attachment.ContentStream);
                var attachmentContent = Convert.ToBase64String(attachmentBytes);

                if (attachment.ContentStream != null && attachment.Name != null)
                    sendGridMessage.AddAttachment(attachment.Name, attachmentContent);

                //, attachment.ContentDisposition.ToString()
            }

            var client = new SendGridClient(this.ApiKey);

            var response = client.SendEmailAsync(sendGridMessage);

            var result = response.Result;

            var statusCode = result.StatusCode.ToString();

            return new DeliverResponse
            {
                StatusCode = statusCode, Success = statusCode == "Accepted"
            };
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            if(stream == null) return new byte[0];

            if (stream is MemoryStream)
                return ((MemoryStream)stream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}