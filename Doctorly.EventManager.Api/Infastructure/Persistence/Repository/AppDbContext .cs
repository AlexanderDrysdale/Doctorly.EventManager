using Doctorly.EventManager.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.EventManager.Api.Infastructure.Persistence.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
    }
}
