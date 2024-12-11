using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using TennisApp.Data;
using TennisApp.Services.Interfaces;

namespace TennisApp.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;

        public MemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetMembersForDropdown()
        {
            return await _context.Members
                .Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.FullName
                })
                .ToListAsync();
        }
    }
}
