using System.ComponentModel.DataAnnotations;

namespace TennisApp.Data.Models
{
    public class Coach
    {
        public Coach()
        {
            this.Id = Guid.NewGuid();
            this.Members = new List<Member>();
            this.Lessons = new List<Lesson>();
            this.CoachLessons = new List<CoachLesson>();
            this.IsDeleted = false;
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Range(TennisApp.Common.Validations.ValidationConstants.FullNameMinLength,
               TennisApp.Common.Validations.ValidationConstants.FullNameMaxLength)]
        public string FullName { get; set; } = null!;
        public string? ImageURL { get; set; }
        [Required]
        public int YearsExperience { get; set; }
        public List<Member> Members { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<CoachLesson> CoachLessons { get; set; }
        public bool IsDeleted { get; set; }
    }
}
