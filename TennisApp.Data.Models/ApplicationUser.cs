using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using TennisApp.Data.Common.Models;

namespace TennisApp.Data.Models
{
    public class ApplicationUser : IdentityUser, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }
        // Common properties for both coaches and members
        [Required]
        public string FullName { get; set; } = null!;

        public string ProfileImageUrl { get; set; } = string.Empty;

        // Foreign key for coach
        public string? CoachId { get; set; }
        public ApplicationUser? Coach { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
