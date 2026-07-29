using Doctorly.EventManager.Api.Domain.Entities;
using Doctorly.EventManager.Api.Infastructure.Persistence.Repository;

namespace Doctorly.EventManager.Api.Infastructure.Persistence
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Attendees.Any())
            {
                context.Attendees.AddRange(GetDummyAttendees());
            }

            if (!context.Events.Any())
            {
                context.Events.AddRange(GetDummyEvents());
            }

            context.SaveChanges();
        }

        private static List<Attendee> GetDummyAttendees()
        {
            return new List<Attendee>
            {
                new Attendee { Name = "Alice Johnson", Email = "alice@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Attendee { Name = "Bob Smith", Email = "bob@example.com", IsAttending = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Attendee { Name = "Charlie Brown", Email = "charlie@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Attendee { Name = "Diana Prince", Email = "diana@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Attendee { Name = "Ethan Hunt", Email = "ethan@example.com", IsAttending = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Attendee { Name = "Fiona Gallagher", Email = "fiona@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            };
        }

        private static List<Event> GetDummyEvents()
        {
            return new List<Event>
            {
                new Event
                {
                    Title = "Tech Conference 2026",
                    Description = "Annual technology conference covering AI, cloud, and web development.",
                    StartTime = DateTime.UtcNow.AddDays(10),
                    EndTime = DateTime.UtcNow.AddDays(10).AddHours(8),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Attendees = new List<Attendee>
                    {
                        new Attendee { Name = "Alice Johnson", Email = "alice@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                        new Attendee { Name = "Bob Smith", Email = "bob@example.com", IsAttending = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                    }
                },
                new Event
                {
                    Title = "Company Retreat",
                    Description = "Team-building retreat in the mountains.",
                    StartTime = DateTime.UtcNow.AddDays(30),
                    EndTime = DateTime.UtcNow.AddDays(32),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Attendees = new List<Attendee>
                    {
                        new Attendee { Name = "Charlie Brown", Email = "charlie@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                        new Attendee { Name = "Diana Prince", Email = "diana@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                    }
                },
                new Event
                {
                    Title = "Hackathon Weekend",
                    Description = "48-hour coding marathon with prizes.",
                    StartTime = DateTime.UtcNow.AddDays(45),
                    EndTime = DateTime.UtcNow.AddDays(46),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Attendees = new List<Attendee>
                    {
                        new Attendee { Name = "Ethan Hunt", Email = "ethan@example.com", IsAttending = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                        new Attendee { Name = "Fiona Gallagher", Email = "fiona@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                    }
                },
                new Event
                {
                    Title = "Product Launch Gala",
                    Description = "Celebration of our new product line.",
                    StartTime = DateTime.UtcNow.AddDays(60),
                    EndTime = DateTime.UtcNow.AddDays(60).AddHours(4),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Attendees = new List<Attendee>
                    {
                        new Attendee { Name = "Alice Johnson", Email = "alice@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                        new Attendee { Name = "Charlie Brown", Email = "charlie@example.com", IsAttending = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                    }
                }
            };
        }
    }
}
