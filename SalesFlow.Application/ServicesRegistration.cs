

using Microsoft.Extensions.DependencyInjection;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Mappings;
using SalesFlow.Application.Services;
using System.Reflection;

namespace SalesFlow.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
         
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddAutoMapper(typeof(CategoryProfile));

            services.AddTransient<IAuthenticationServices, AuthenticationServices>();
            services.AddTransient<IReporterServices, ReporterServices>();
            services.AddTransient<IRolesServices, RoleServices>();
            services.AddTransient<IPaymentServices, PaymentServices>();
           

        }
    }
}
