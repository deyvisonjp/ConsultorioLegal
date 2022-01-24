using CL.Data.Context;
using CL.Data.Repository;
using CL.Manager.Implementation;
using CL.Manager.Interfaces;
using CL.Manager.Mappings;
using CL.Manager.Validator;
using CL.WebApi.Configuration;
using CL.WebApi.Controllers;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var connectionString = builder.Configuration.GetConnectionString("ClConnection");
builder.Services.AddDbContext<ClContext>(options => options.UseSqlServer(connectionString)); //ClContext em 

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddAutoMapperConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   app.UseDeveloperExceptionPage();
}

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


