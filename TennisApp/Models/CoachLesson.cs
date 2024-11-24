using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TennisApp.Models
{
    public class CoachLesson
    {
        [Required]
        [Key]
        public Guid CoachId { get; set; }
        [ForeignKey(nameof(CoachId))]
        public Coach Coach { get; set; } = null!;

        [Key]
        [Required]
        public Guid LessonId { get; set; }
        [ForeignKey(nameof(LessonId))]
        public Lesson Lesson { get; set; } = null!;
    }
}
