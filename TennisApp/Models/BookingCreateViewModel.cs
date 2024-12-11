using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TennisApp.Models
{
    public class BookingCreateViewModel
    {
        [Required]
        [DisplayFormat(DataFormatString = TennisApp.Common.Validations.ValidationConstants.BookingDateFormat, ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public Guid MemberId { get; set; }

        [Required]
        public int CourtId { get; set; }

        public IEnumerable<SelectListItem> Members { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Courts { get; set; } = new List<SelectListItem>();
    }
}
