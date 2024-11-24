using System.ComponentModel.DataAnnotations;
using TennisApp.Models.Enums;

namespace TennisApp.Models
{
    public class Court
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(TennisApp.Common.Validations.ValidationConstants.CourtNameMinLength,
            TennisApp.Common.Validations.ValidationConstants.CourtNameMaxLength)]
        public string Name { get; set; } = null!;
        [Required]
        public CourtType CourtType { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
