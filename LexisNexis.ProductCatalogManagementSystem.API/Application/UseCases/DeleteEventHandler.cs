using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;

namespace Doctorly.EventManager.Api.Application.UseCases
{
    public class DeleteEventHandler
    {
        private readonly IEventRepository _repo;

        public DeleteEventHandler(IEventRepository repo) => _repo = repo;

        public bool Handle(int id)
        {
            var ev = _repo.GetById(id);
            if (ev == null) return false;

            _repo.Delete(id);
            return true;
        }
    }
}
