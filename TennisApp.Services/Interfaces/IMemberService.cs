using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisApp.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<SelectListItem>> GetMembersForDropdown();
    }
}
