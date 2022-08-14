using AutoMapper;
using Domain.Interface;
using Infrastructure.Clients;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using SensorService.Mappers;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AutoMapping());
});
IMapper autoMapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(autoMapper);

builder.Services.AddScoped<ISmartRainSensorService, SmartRainSensorService>();
builder.Services.AddScoped<ISmartRainSensorRepository, SmartRainSensorRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

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
