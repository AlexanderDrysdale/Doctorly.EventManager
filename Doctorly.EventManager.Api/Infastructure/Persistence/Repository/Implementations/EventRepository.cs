using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Implementations
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Event> _dbSet;

        public EventRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Event>();
        }

        public IEnumerable<Event> SearchByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Enumerable.Empty<Event>();

            return _dbSet
                .Where(p => EF.Functions.Like(p.Title, $"%{name}%"))
                .ToList();
        }
    }
}
