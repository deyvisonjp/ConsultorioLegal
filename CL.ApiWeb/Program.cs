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
    var configurationDatabase = builder.Configuration;

    #region Config Log
    //https://youtu.be/u9UREKdQD70?list=PLbq2QKd5ieAt0H551D_0E4bGIYRxbq5HL&t=2770
    string ambiente = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"); // Vem do arquivo Properties/launchSettings.json
    var configurationLog = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{ambiente}.json")
        .Build();

    builder.Host.UseSerilog((ctx, lc) => lc
        .ReadFrom.Configuration(configurationLog)
        .ReadFrom.Configuration(ctx.Configuration));
    #endregion

    builder.Services.AddControllers();
    builder.Services.AddFluentValidationConfiguration();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerConfiguration();

    builder.Services.AddFluentValidationRulesToSwagger();

    builder.Services.AddDatabaseConfiguration(configurationDatabase);

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


