

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesFlow.Identity.Entities;
using SalesFlow.Identity.EntityConfiguration;

namespace SalesFlow.Identity.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationUserRol, int,
    IdentityUserClaim<int>, ApplicationUserRoles, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            //modelBuilder.ApplyConfiguration(new RolConfiguration());
            modelBuilder.ApplyConfiguration(new RolesUserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
