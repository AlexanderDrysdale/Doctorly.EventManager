using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.EventManager.Api.Infastructure.Persistence.Repository.Implementations
{
    public class AttendeeRepository : Repository<Attendee>, IAttendeeRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Attendee> _dbSet;

        public AttendeeRepository(AppDbContext context)
            : base(context)
        {
            _context = context;
            _dbSet = _context.Set<Attendee>();
        }
    }
}
