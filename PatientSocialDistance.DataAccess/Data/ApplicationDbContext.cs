using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PatientSocialDistance.Persistence.Models;
using System.Reflection.Emit;

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
        public DbSet<Notification> Notifications { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Hospital");

            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");

            builder.Entity<IdentityRole>().HasData(new IdentityRole[]
            {

                new IdentityRole
                {
                    ConcurrencyStamp = "ff84e631-d455-44f6-b1d7-2395501f41ef",
                    Id = "3dea862e-bc8c-4cd6-a577-ac65509757e7",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
            new IdentityRole
            {
                ConcurrencyStamp = "9805afd8-c511-4285-bad4-9a87d5efd2d3",
                Id = "c5000e40-4357-4895-8689-35e2ffff0cae",
                Name = "User",
                NormalizedName = "USER"
            }
            });

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
                new VistApprovalStatus() { Id = 4, Name = "WaitForRequesterToApprove", IsDeleted = false },
            });

        }
    }
}
