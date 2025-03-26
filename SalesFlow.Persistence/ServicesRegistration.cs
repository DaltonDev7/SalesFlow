

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories;
using SalesFlow.Persistence.Repositories.Generic;

namespace SalesFlow.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationContext>(db => db.UseSqlServer(connectionString));

            // repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();
        }
    }
}
