

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Models;
using SalesFlow.Application.Services;
using SalesFlow.Application.Wrappers;
using SalesFlow.Domain.Entities;
using SalesFlow.Identity.Context;
using System.Text;

namespace SalesFlow.Identity
{

    public static class ServiceRegistration
    {


        public static void AddIdentityInfrastructureForApi(this IServiceCollection services, IConfiguration configuration)
        {

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
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
                    },
                    //Por diablo entra aqui
                    OnChallenge = c =>
                    {
                        c.HandleResponse();
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new ApiResponse<string>("You are not Authrize"));
                        return c.Response.WriteAsync(result);
                    },
                    OnForbidden = c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new ApiResponse<string>("You are not Authrize to access this resource"));
                        return c.Response.WriteAsync(result);
                    },

                };
            });


            services.AddTransient<IRolesServices, RoleServices>();
            services.AddTransient<IAuthenticationServices, AuthenticationServices>();

        }

        private static void ContextConfiguration(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
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
             .AddEntityFrameworkStores<IdentityContext>()
             .AddDefaultTokenProviders();

        }
    }
}
