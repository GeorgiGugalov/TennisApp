using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisApp.Services.Interfaces;
using TennisApp.Web.Infrastructure.Repositories;
using TennisApp.Web.ViewModels;

namespace TennisApp.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            this.bookingRepository = bookingRepository;
        }
        public IQueryable<BookingIndexViewModel> GetAllBookingsOrderedByDateDescending()
        {
            IQueryable<BookingIndexViewModel> all = bookingRepository.All()
                .Include(b => b.Member)
                .Include(b => b.Court)
                .Select(b => new BookingIndexViewModel
                {
                    Date = b.Date,
                    MemberName = b.Member.FullName,
                    CourtName = b.Court.Name
                })
                .OrderByDescending(x => x.Date);

            return all;
        }
    }
}
