
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.ToTable("Ingredients");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(10,2)");

            // Relación uno a muchos con Recipe
            builder.HasMany(i => i.Recipes)
                   .WithOne(r => r.Ingredient)
                   .HasForeignKey(r => r.IdIngredient)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
