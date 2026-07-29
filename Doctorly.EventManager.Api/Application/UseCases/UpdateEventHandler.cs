using Doctorly.EventManager.Api.Application.Dtos;
using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;

namespace Doctorly.EventManager.Api.Application.UseCases
{
    public class UpdateEventHandler
    {
        private readonly IEventRepository _repo;

        public UpdateEventHandler(IEventRepository repo) => _repo = repo;

        public bool Handle(int id, UpdateEventDto dto)
        {
            var ev = _repo.GetById(id);
            if (ev == null) return false;

            ev.Title = dto.Title;
            ev.Description = dto.Description;
            ev.StartTime = dto.StartTime;
            ev.EndTime = dto.EndTime;
            ev.UpdatedAt = DateTime.UtcNow;

            if (dto.Attendees != null)
            {
                ev.Attendees = dto.Attendees.Select(a => new Attendee
                {
                    Id = a.Id,
                    Name = a.Name,
                    Email = a.Email,
                    IsAttending = a.IsAttending,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = DateTime.UtcNow
                }).ToList();
            }

            _repo.Update(ev);
            return true;
        }
    }
}
