using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.HasDefaultSchema("Hospital");
            //builder.Entity<IdentityUser>().ToTable("Users", "Security");
            //builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            //builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            //builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            //builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            //builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            //builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");


            builder.Entity<Interaction>().HasQueryFilter(i => !i.IsDeleted);
            builder.Entity<Vist>().HasQueryFilter(i => !i.IsDeleted);

            builder.Entity<UserType>().HasData( new UserType[] {
                new UserType() { Id = 1, IsDeleted = false, Name = "Doctor" },
                new UserType() { Id = 2, IsDeleted = false, Name = "Patient" },
                });

            builder.Entity<VistApprovalStatus>().HasData(new VistApprovalStatus[]
            {
                new VistApprovalStatus() { Id = 1, Name = "Requested", IsDeleted = false },
                new VistApprovalStatus() { Id = 2, Name = "Accepted", IsDeleted = false },
                new VistApprovalStatus() { Id = 3, Name = "Reject", IsDeleted = false },
            });

            //builder.Entity<Block>().Ignore(i=> i.UserBlockedId);

        }
    }
}
