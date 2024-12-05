using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TennisApp;
using TennisApp.Common.Validations;

namespace TennisApp.Data.Models
{
    public class Booking
    {
        [Key]
        public string Id { get; set; } = null!;
        [Required]
        [DisplayFormat(DataFormatString = TennisApp.Common.Validations.ValidationConstants.BookingDateFormat, ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public Guid MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public Member Member { get; set; } = null!;
        public int CourtId { get; set; }
        [Required]
        public Court Court { get; set; } = null!;
        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
