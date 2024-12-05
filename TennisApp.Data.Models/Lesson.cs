using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TennisApp.Data.Models
{
    public class Lesson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid MemberId { get; set; }

        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; } = null!;
        public Guid CoachId { get; set; }

        [ForeignKey(nameof(CoachId))]
        public Coach Coach { get; set; } = null!;

        [Required]
        public int CourtId { get; set; }

        [ForeignKey(nameof(CourtId))]
        public Court Court { get; set; } = null!;
        public List<CoachLesson> CoachLessons { get; set; } = new List<CoachLesson>();
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
