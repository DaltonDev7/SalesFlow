

using Microsoft.Extensions.DependencyInjection;
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

      
        }
    }
}
