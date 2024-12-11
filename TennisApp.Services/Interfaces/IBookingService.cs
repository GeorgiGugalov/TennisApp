using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisApp.Web.ViewModels;

namespace TennisApp.Services.Interfaces
{
    public interface IBookingService
    {
        IQueryable<BookingIndexViewModel> GetAllBookingsOrderedByDateDescending();
    }
}
