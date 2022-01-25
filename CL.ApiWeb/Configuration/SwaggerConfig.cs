using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CL.WebApi.Configuration
{
    public static class SwaggerConfig
    {
        [Obsolete]
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo 
                    { 
                        Title = "Consultorio Legal",
                        Version = "v1" ,
                        Description = "API da aplicação Consultorio Legal - by Francis Silveira",
                        Contact = new OpenApiContact 
                        {
                            Name = "Deyvison J Paula",
                            Email = "deyvisonjpaula@gmail.com",
                            Url = new Uri("https://github.com/deyvisonjp")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "OSD",
                            Url = new Uri("https://opensource.org/osd")
                        },
                        TermsOfService = new Uri("https://opensource.org/osd")
                    });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"; // Via assembly pega o nome da aplicação
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Repositório e concatenação com arquivo xml
                c.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(AppContext.BaseDirectory, "CL.Core.Shared.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "CL V1");
            });
        }

    }
}
