using Doctorly.EventManager.Api.Application.Dtos;
using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;

namespace Doctorly.EventManager.Api.Application.UseCases
{
    public class CreateEventHandler
    {
        private readonly IEventRepository _repo;

        public CreateEventHandler(IEventRepository repo) => _repo = repo;

        public EventDto Handle(CreateEventDto dto)
        {
            var ev = new Event
            {
                Title = dto.Title,
                Description = dto.Description,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Attendees = dto.Attendees?.Select(a => new Attendee
                {
                    Name = a.Name,
                    Email = a.Email,
                    IsAttending = a.IsAttending,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList() ?? new List<Attendee>()
            };

            _repo.Add(ev);
            return EventDto.FromEntity(ev);
        }
    }
}
