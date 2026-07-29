using System.ComponentModel.DataAnnotations;

namespace Doctorly.EventManager.Api.Application.Dtos
{
    // Used in POST /api/categories
    public record CreateAttendeeDto(
        [property: Required]
        [property: StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        string Name,

        [property: Required]
        [property: EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [property: StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        string Email,

        [property: Required]
        bool IsAttending
    );
}
