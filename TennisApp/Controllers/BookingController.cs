using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;
using TennisApp.Data;
using TennisApp.Data.Models;
using TennisApp.Models;
using TennisApp.Services;
using TennisApp.Services.Interfaces;
using TennisApp.Web.Infrastructure.Repositories;
using TennisApp.Web.ViewModels;

namespace TennisApp.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;

        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public BookingController(IBookingService bookingService, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            this._bookingService = bookingService;
            this.context = context;
            this.userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            IEnumerable<BookingIndexViewModel> bookings = _bookingService.GetAllBookingsOrderedByDateDescending();
            return View(bookings);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new BookingCreateViewModel
            {
                Members = await _memberService.GetMembersForDropdown(),
                Courts = await _courtService.GetCourtsForDropdown()
            };
            return View(model);
        }
    }
}
