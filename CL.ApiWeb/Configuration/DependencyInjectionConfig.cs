using CL.Data.Repository;
using CL.Manager.Implementation;
using CL.Manager.Interfaces;

namespace CL.WebApi.Controllers
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IClienteManager, ClienteManager>();
        }
    }
}
