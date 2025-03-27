
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");
            builder.HasKey(x => x.Id);


            // Relación con el producto final
            builder.HasOne(r => r.Product)
                   .WithMany(p => p.Recipes)
                   .HasForeignKey(r => r.IdProduct)
                    .OnDelete(DeleteBehavior.NoAction);

            // Relación con el ingrediente (que también es un producto)
            builder.HasOne(r => r.Ingredient)
                   .WithMany()
                   .HasForeignKey(r => r.IdIngredient)
                    .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
