using System.ComponentModel.DataAnnotations;

namespace Doctorly.EventManager.Api.Application.Dtos
{
    // Used in POST /api/products
    public record CreateEventDto(
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

        List<CreateAttendeeDto> Attendees
    );
}
