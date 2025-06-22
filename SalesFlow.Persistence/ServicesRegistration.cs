

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SalesFlow.Application.Interfaces.Common;
using SalesFlow.Application.Interfaces.Repositories;
using SalesFlow.Application.Models;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using SalesFlow.Persistence.Context;
using SalesFlow.Persistence.Repositories;
using SalesFlow.Persistence.Repositories.Generic;
using System.Text;


namespace SalesFlow.Persistence
{
    public static class ServicesRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationContext>(db => db.UseSqlServer(connectionString));


            ContextConfiguration(services, configuration);

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = async context =>
                    {
                        context.NoResult();
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        await context.Response.WriteAsync(context.Exception.ToString());
                    },

                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new ApiResponse<string>("You are not Authorized"));
                        await context.Response.WriteAsync(result);
                    },

                    OnForbidden = async context =>
                    {
                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new ApiResponse<string>("You are not Authorized to access this resource"));
                        await context.Response.WriteAsync(result);
                    }
                };
            });



            // repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IInventoryRepository, InventoryRepository>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IHistoryOrderRepository, HistoryOrderRepository>();
            services.AddTransient<IPaymentsRepository, PaymentsRepository>();
            services.AddTransient<ITableRepository, TableRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
        }

        private static void ContextConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName));
            });


            services.AddIdentity<ApplicationUser, ApplicationUserRol>(options =>
            {
                options.User.RequireUniqueEmail = true; // Asegura correos únicos
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
            }).AddRoles<ApplicationUserRol>().AddRoleManager<RoleManager<ApplicationUserRol>>()
             .AddEntityFrameworkStores<ApplicationContext>()
             .AddDefaultTokenProviders();

        }
    }
}
