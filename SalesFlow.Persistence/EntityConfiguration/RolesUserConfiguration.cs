

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;


namespace SalesFlow.Persistence.EntityConfiguration
{
    public class RolesUserConfiguration : IEntityTypeConfiguration<ApplicationUserRoles>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoles> builder)
        {
            builder.ToTable("UserRoles");
            //builder.HasKey(c => new { c.UserId, c.RoleId });

            //builder.HasOne(ur => ur.Role)
            //   .WithMany(r => r.UserRoles) // Esto es importante para mantener la relación bidireccional
            //   .HasForeignKey(ur => ur.RoleId)
            //   .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(ur => ur.User)
            //    .WithMany(u => u.UserRoles) // Mantén la relación en el otro lado también
            //    .HasForeignKey(ur => ur.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
