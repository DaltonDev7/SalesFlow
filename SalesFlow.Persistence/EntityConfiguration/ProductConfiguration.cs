
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
             .HasColumnType("decimal(10,2)");


            builder.HasOne(c => c.Category)
                   .WithMany(p => p.Products)
                   .HasForeignKey(c => c.IdCategory)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(p => p.Inventory)  // Relación 1:1
              .WithOne(i => i.Product)
              .HasForeignKey<Inventory>(i => i.IdProduct)
              .OnDelete(DeleteBehavior.Cascade); // Si se elimina un producto, su inventario también

            // Relación uno a muchos con Recipe
            builder.HasMany(p => p.Recipes)
                   .WithOne(r => r.Product)
                   .HasForeignKey(r => r.IdProduct)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category)
              .WithMany(c => c.Products) // Una categoría tiene muchos productos
              .HasForeignKey(p => p.IdCategory) // Clave foránea en Product
              .OnDelete(DeleteBehavior.Restrict); // No permite borrar una categoría si tiene productos
        }
    }
}
