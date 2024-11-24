using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisApp.Models;

namespace TennisApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<CoachLesson> CoachLessons { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Setting> Settings { get; set; }
    }
}
