using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisApp.Web.ViewModels
{
    public class BookingIndexViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = TennisApp.Common.Validations.ValidationConstants.BookingDateFormat, ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string MemberName { get; set; }
        public string CourtName { get; set; }

    }
}
