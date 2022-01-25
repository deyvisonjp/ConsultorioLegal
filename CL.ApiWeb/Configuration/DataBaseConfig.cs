using CL.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CL.WebApi.Configuration
{
    public static class DataBaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ClConnection");
            services.AddDbContext<ClContext>(options => options.UseSqlServer(connectionString));
        }

        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetRequiredService<ClContext>();
            context.Database.Migrate(); // O mesmo que update-database no console <> Atuliza o migrations e banco de dados
            context.Database.EnsureCreated(); // Garantimos que a base de dados será criada
        }
    }
}
