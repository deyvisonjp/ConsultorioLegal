using CL.Data.Context;
using CL.Data.Repository;
using CL.Manager.Implementation;
using CL.Manager.Interfaces;
using CL.Manager.Mappings;
using CL.Manager.Validator;
using CL.WebApi.Configuration;
using CL.WebApi.Controllers;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Serilog;

//Log.Logger = new LoggerConfiguration()
//    .Enrich.FromLogContext()
//    .WriteTo.File("logs/log.txt", fileSizeLimitBytes: 500000, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Day) // após a instalação do Sinks
//    .WriteTo.Console()
//    .CreateLogger();
//    // https://github.com/serilog/serilog/wiki/Configuration-Basics

//Log.Information("Iniciando o WebApi");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .Enrich.FromLogContext()
        .MinimumLevel.Debug()
        .WriteTo.Async(p => p.Console())
        .WriteTo.Async(p => p.File("logs/log.txt", fileSizeLimitBytes: 500000, rollOnFileSizeLimit: true, rollingInterval: RollingInterval.Day))
        .ReadFrom.Configuration(ctx.Configuration));

    var Configuration = builder.Configuration;

    builder.Services.AddControllers();
    builder.Services.AddFluentValidationConfiguration();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerConfiguration();

    builder.Services.AddFluentValidationRulesToSwagger();

    builder.Services.AddDatabaseConfiguration(Configuration);

    builder.Services.AddDependencyInjectionConfiguration();

    builder.Services.AddAutoMapperConfiguration();

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseSwaggerConfiguration();

    app.UseDatabaseConfiguration();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Erro catatrófico");
    throw;
}
finally
{
    Log.Information("Finalizando o WebApi");
    Log.CloseAndFlush();
}


