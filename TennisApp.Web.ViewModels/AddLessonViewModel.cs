using System.ComponentModel.DataAnnotations;
using TennisApp.Data.Models;

namespace TennisApp.Models
{
    public class AddLessonViewModel
    {
        [Required]
        public Guid MemberId { get; set; }
        public Member? Member { get; set; }
        [Required]
        public Guid CourtId { get; set; }
        public Court? Court { get; set; }
        [Required]
        public Guid CoachId { get; set; }
        public Coach? Coach { get; set; }

    }
}
