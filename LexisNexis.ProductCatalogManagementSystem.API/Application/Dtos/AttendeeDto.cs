using Doctorly.EventManager.Api.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Doctorly.EventManager.Api.Application.Dtos
{
    // Returned in GET endpoints
    public record AttendeeDto(
        int Id,

        [property: Required]
        [property: StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        string Name,

        [property: Required]
        [property: EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [property: StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        string Email,

        [property: Required]
        bool IsAttending,

        DateTime CreatedAt,
        DateTime UpdatedAt
    )
    {
        public static AttendeeDto FromEntity(Attendee a) =>
        new AttendeeDto(
            a.Id,
            a.Name,
            a.Email,
            a.IsAttending,
            a.CreatedAt,
            a.UpdatedAt
        );
    };
}
