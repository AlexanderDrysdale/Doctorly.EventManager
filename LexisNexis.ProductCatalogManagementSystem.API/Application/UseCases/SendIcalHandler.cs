using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;
using Doctorly.EventManager.Api.Infastructure.Services.Interfaces;
using System.Text;

namespace Doctorly.EventManager.Api.Application.UseCases
{
    public class SendIcalHandler
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEmailService _emailService;

        public SendIcalHandler(IEventRepository eventRepository, IEmailService emailService)
        {
            _eventRepository = eventRepository;
            _emailService = emailService;
        }

        public void Handle(int eventId)
        {
            var ev = _eventRepository.GetById(eventId);
            if (ev == null) throw new ArgumentException("Event not found.");

            var icalContent = BuildIcal(ev);
            var attachmentBytes = Encoding.UTF8.GetBytes(icalContent);

            foreach (var attendee in ev.Attendees)
            {
                _emailService.SendEmail(
                    to: attendee.Email,
                    subject: $"Invitation: {ev.Title}",
                    body: $"Dear {attendee.Name},\n\nYou are invited to {ev.Title}.\n\nRegards,\nEvent Manager",
                    attachments: new List<EmailAttachment>
                    {
                        new EmailAttachment("invite.ics", "text/calendar", attachmentBytes)
                    }
                );
            }
        }

        private string BuildIcal(Event ev)
        {
            var sb = new StringBuilder();
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID:-//Doctorly EventManager//EN");
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine($"UID:{Guid.NewGuid()}");
            sb.AppendLine($"DTSTAMP:{DateTime.UtcNow:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"DTSTART:{ev.StartTime:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"DTEND:{ev.EndTime:yyyyMMddTHHmmssZ}");
            sb.AppendLine($"SUMMARY:{ev.Title}");
            sb.AppendLine($"DESCRIPTION:{ev.Description}");
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");
            return sb.ToString();
        }
    }
}
