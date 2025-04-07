using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Identity.Entities;

namespace SalesFlow.Identity.EntityConfiguration
{
    internal class RolConfiguration : IEntityTypeConfiguration<ApplicationUserRol>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRol> builder)
        {
            builder.ToTable("Roles");
            //builder.HasKey(x => x.Id);
        }
    }
}
