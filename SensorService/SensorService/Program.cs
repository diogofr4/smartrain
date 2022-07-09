using Domain.Interface;
using Infrastructure.Clients;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ISmartRainSensorService, SmartRainSensorService>();
builder.Services.AddScoped<ISmartRainSensorRepository, SmartRainSensorRepository>();
//builder.Services.AddScoped<IDapperClient, DapperClient>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SensorService", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SensorService v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
