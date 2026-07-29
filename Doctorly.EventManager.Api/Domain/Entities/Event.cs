using System.ComponentModel.DataAnnotations;

namespace Doctorly.EventManager.Api.Domain.Entities
{
    public class Event : BaseEntity
    {
        [Required]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
        public string Title { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();

    }
}
