using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TennisApp.Common.Validations;
using TennisApp.Data.Common.Models;
using TennisApp.Data.Models;

namespace TennisApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly MethodInfo SetIsDeletedQueryFilterMethod =
            typeof(ApplicationDbContext).GetMethod(
                nameof(SetIsDeletedQueryFilter),
                BindingFlags.NonPublic | BindingFlags.Static);

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

        public override int SaveChanges() => this.SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
            this.SaveChangesAsync(true, cancellationToken);

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            this.ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            this.ConfigureUserIdentityRelations(builder);

            EntityIndexesConfiguration.Configure(builder);

            var entityTypes = builder.Model.GetEntityTypes().ToList();

            // Set global query filter for not deleted entities only
            var deletableEntityTypes = entityTypes
                .Where(et => et.ClrType != null && typeof(IDeletableEntity).IsAssignableFrom(et.ClrType));
            foreach (var deletableEntityType in deletableEntityTypes)
            {
                var method = SetIsDeletedQueryFilterMethod.MakeGenericMethod(deletableEntityType.ClrType);
                method.Invoke(null, new object[] { builder });
            }

            // Disable cascade delete
            var foreignKeys = entityTypes
                .SelectMany(e => e.GetForeignKeys().Where(f => f.DeleteBehavior == DeleteBehavior.Cascade));
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

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

        private static void SetIsDeletedQueryFilter<T>(ModelBuilder builder)
            where T : class, IDeletableEntity
        {
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }
        private void ConfigureUserIdentityRelations(ModelBuilder builder)
             => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
