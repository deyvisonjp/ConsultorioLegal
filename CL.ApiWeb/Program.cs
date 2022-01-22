using CL.Data.Context;
using CL.Data.Repository;
using CL.Manager.Implementation;
using CL.Manager.Interfaces;
using CL.Manager.Mappings;
using CL.Manager.Validator;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Consultorio Legal", Version = "v1" }));

builder.Services.AddFluentValidation(p =>
    {
        p.RegisterValidatorsFromAssemblyContaining<NovoClienteValidator>(); //Adiciona a injeção de dependencias para validação
        p.RegisterValidatorsFromAssemblyContaining<AlteraClienteValidator>();
        p.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("pt-BR");
    });

var connectionString = builder.Configuration.GetConnectionString("ClConnection");
builder.Services.AddDbContext<ClContext>(options => options.UseSqlServer(connectionString)); //ClContext em 

builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteManager, ClienteManager>();

builder.Services.AddAutoMapper(typeof(NovoClienteMappingProfile), typeof(AlteraClienteMappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = string.Empty;
            c.SwaggerEndpoint("./swagger/v1/swagger.json", "CL V1");
        });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


