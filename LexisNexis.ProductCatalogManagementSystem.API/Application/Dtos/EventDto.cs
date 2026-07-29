using Doctorly.EventManager.Api.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Doctorly.EventManager.Api.Application.Dtos
{
    // Returned in GET /api/categories (flat list)
    public record EventDto(
        int Id,

        [property: Required]
        [property: StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        string Title,

        [property: Required]
        [property: StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        string Description,

        [property: Required]
        DateTime StartTime,

        [property: Required]
        DateTime EndTime,

        List<AttendeeDto> Attendees,

        DateTime CreatedAt,
        DateTime UpdatedAt
    )
    {
        public static EventDto FromEntity(Event ev) =>
            new EventDto(
                ev.Id,
                ev.Title,
                ev.Description,
                ev.StartTime,
                ev.EndTime,
                ev.Attendees.Select(a => AttendeeDto.FromEntity(a)).ToList(),
                ev.CreatedAt,
                ev.UpdatedAt
            );
    };
}
