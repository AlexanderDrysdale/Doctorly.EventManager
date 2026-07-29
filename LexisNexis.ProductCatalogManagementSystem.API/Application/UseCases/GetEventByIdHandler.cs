using Doctorly.EventManager.Api.Application.Dtos;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;

namespace Doctorly.EventManager.Api.Application.UseCases
{
    public class GetEventByIdHandler
    {
        private readonly IEventRepository _repo;

        public GetEventByIdHandler(IEventRepository repo) => _repo = repo;

        public EventDto? Handle(int id)
        {
            var ev = _repo.GetById(id);
            return ev == null ? null : EventDto.FromEntity(ev);
        }
    }
}
