using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TennisApp.Common.Validations;
using TennisApp.Data.Models.Enums;

namespace TennisApp.Data.Models
{
    public class Member
    {
        public Member()
        {
            this.Id = Guid.NewGuid();
            this.Lessons = new List<Lesson>();
            this.Bookings = new List<Booking>();
            this.IsDeleted = false;
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Range(ValidationConstants.FullNameMinLength, ValidationConstants.FullNameMaxLength)]
        public string FullName { get; set; } = null!;
        public string? ImageURL { get; set; }
        [Required]
        public Level Level { get; set; }
        [Required]
        public string Racket { get; set; } = null!;
        [Required]
        public Guid CoachId { get; set; }
        [ForeignKey(nameof(CoachId))]
        public Coach Coach { get; set; } = null!;
        public List<Lesson> Lessons { get; set; }
        public List<Booking> Bookings { get; set; }
        public bool IsDeleted { get; set; }
    }
}
