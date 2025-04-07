using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;


namespace SalesFlow.Persistence.EntityConfiguration
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
