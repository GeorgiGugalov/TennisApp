using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisApp.Common.Repositories;
using TennisApp.Data.Models;

namespace TennisApp.Web.Infrastructure.Repositories
{
    public interface IBookingRepository : IRepository<Booking>
    {
    }
}
