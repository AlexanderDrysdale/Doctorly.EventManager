using Doctorly.EventManager.Api.Domain.Entities;

namespace Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces
{
    public interface IEventRepository : IRepository<Event>
    {
        IEnumerable<Event> SearchByName(string name);
    }
}
