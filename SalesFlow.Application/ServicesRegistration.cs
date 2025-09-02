

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesFlow.Application.Feature.Orders.Commands;
using SalesFlow.Application.Interfaces.Services;
using SalesFlow.Application.Mappings;
using SalesFlow.Application.Services;
using System.Reflection;
using Microsoft.Extensions.Configuration;   // para GetSection
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;         // para IOptions<T>


namespace SalesFlow.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(CategoryProfile));

            services.AddTransient<IAuthenticationServices, AuthenticationServices>();
            services.AddTransient<IReporterServices, ReporterServices>();
            services.AddTransient<IRolesServices, RoleServices>();
            services.AddTransient<IAlertService, AlertService>();
            services.AddTransient<IPaymentServices, PaymentServices>();
            services.AddTransient<IHistoryOrdersServices, HistoryOrdersServices>();
            services.AddTransient<IProductServices, ProductServices>();
            services.AddTransient<IReservationsServices, ReservationsServices>();
            services.AddTransient<ITablesServices, TablesServices>();

            services.AddTransient<IProductosFaltanteManager, ProductosFaltanteManager>();

            // ✅ ahora sí puedes usar configuration
            services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));
            services.AddTransient<ISmtpEmailSender, SmtpEmailSender>();

            services.Configure<DataProtectionTokenProviderOptions>(o =>
            {
                o.TokenLifespan = TimeSpan.FromHours(2);
            });

        }
    }
}
