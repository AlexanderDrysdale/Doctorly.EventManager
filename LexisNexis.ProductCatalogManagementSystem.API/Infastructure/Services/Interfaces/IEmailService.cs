using Doctorly.EventManager.Api.Domain.Entities;

namespace Doctorly.EventManager.Api.Infastructure.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body, List<EmailAttachment>? attachments = null);
    }
}
