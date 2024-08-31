using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories;
using Gardening.Infrastructure.Repositories.Interfaces;
using Gardening.Services.Services;
using Gardening.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PlantAppDbContext>(options =>
    options.UseInMemoryDatabase("PlantDb"));

builder.Services.AddScoped<IPlantService, PlantService>();
builder.Services.AddScoped<IPlantSpecieService, PlantSpecieService>();

builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<IPlantSpecieRepository, PlantSpecieRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program{

}