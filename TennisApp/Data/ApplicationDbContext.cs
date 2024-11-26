using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisApp.Common.Validations;
using TennisApp.Models;

namespace TennisApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>(entity =>
            {
                entity.HasOne(b => b.Member)
                      .WithMany(m => m.Bookings)
                      .HasForeignKey(b => b.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Court)
                      .WithMany(c => c.Bookings)
                      .HasForeignKey(b => b.CourtId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Coach>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.FullName)
                      .IsRequired()
                      .HasMaxLength(ValidationConstants.FullNameMaxLength);

                entity.Property(c => c.YearsExperience)
                      .IsRequired();

                entity.Property(c => c.IsDeleted)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.HasMany(c => c.Members)
                      .WithOne(m => m.Coach)
                      .HasForeignKey(m => m.CoachId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(c => c.Lessons)
                      .WithOne(l => l.Coach)
                      .HasForeignKey(l => l.CoachId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Court>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(ValidationConstants.CourtNameMaxLength);

                entity.Property(c => c.IsDeleted)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.HasMany(c => c.Bookings)
                      .WithOne(b => b.Court)
                      .HasForeignKey(b => b.CourtId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.Lessons)
                      .WithOne(l => l.Court)
                      .HasForeignKey(l => l.CourtId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Lesson>(entity =>
            {
                entity.HasKey(l => l.Id);

                entity.Property(l => l.IsDeleted)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.HasOne(l => l.Member)
                      .WithMany(m => m.Lessons)
                      .HasForeignKey(l => l.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(l => l.Coach)
                      .WithMany(c => c.Lessons)
                      .HasForeignKey(l => l.CoachId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(l => l.Court)
                      .WithMany(c => c.Lessons)
                      .HasForeignKey(l => l.CourtId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(l => l.CoachLessons)
                      .WithOne(cl => cl.Lesson)
                      .HasForeignKey(cl => cl.LessonId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.FullName)
                      .IsRequired()
                      .HasMaxLength(ValidationConstants.FullNameMaxLength);

                entity.Property(m => m.Level)
                      .IsRequired();

                entity.Property(m => m.Racket)
                      .IsRequired();

                entity.HasOne(m => m.Coach)
                      .WithMany(c => c.Members)
                      .HasForeignKey(m => m.CoachId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(m => m.Lessons)
                      .WithOne(l => l.Member)
                      .HasForeignKey(l => l.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(m => m.Bookings)
                      .WithOne(b => b.Member)
                      .HasForeignKey(b => b.MemberId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<CoachLesson>(entity =>
            {
                entity.HasKey(cl => new { cl.CoachId, cl.LessonId });

                entity.HasOne(cl => cl.Coach)
                      .WithMany(c => c.CoachLessons)
                      .HasForeignKey(cl => cl.CoachId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(cl => cl.Lesson)
                      .WithMany(l => l.CoachLessons)
                      .HasForeignKey(cl => cl.LessonId)
                      .OnDelete(DeleteBehavior.NoAction);
            });
            

            // ApplicationUserConfiguration

            var appUser = builder.Entity<ApplicationUser>();

            appUser
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            appUser
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
