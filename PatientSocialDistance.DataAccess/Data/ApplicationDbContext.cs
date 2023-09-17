using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.Persistence.Models;

namespace PatientSocialDistance.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Vist> Vists { get; set; }
        public DbSet<VistApprovalStatus> VistApprovalStatuses { get; set; }
        public DbSet<Block> Blocks { get; set; }

    }
}
