using System.ComponentModel.DataAnnotations;

namespace Doctorly.EventManager.Api.Domain.Entities
{
    public class Attendee : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        public string Email { get; set; }

        [Required]
        public bool IsAttending { get; set; }
    }
}
