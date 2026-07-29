using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Config;
using Doctorly.EventManager.Api.Infastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Doctorly.EventManager.Api.Infastructure.Services.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public void SendEmail(string to, string subject, string body, List<EmailAttachment>? attachments = null)
        {
            using var client = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = true
            };

            var mail = new MailMessage(_settings.FromAddress, to, subject, body);
            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    var stream = new MemoryStream(attachment.Content);
                    mail.Attachments.Add(new Attachment(stream, attachment.FileName, attachment.ContentType));
                }
            }

            client.Send(mail);
        }
    }
}
