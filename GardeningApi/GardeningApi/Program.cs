using Asp.Versioning;
using Gardening.Infrastructure.Data;
using Gardening.Infrastructure.Repositories;
using Gardening.Infrastructure.Repositories.Interfaces;
using Gardening.Services.Services;
using Gardening.Services.Services.Interfaces;
using GardeningApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PlantAppDbContext>(options =>
    options.UseInMemoryDatabase("PlantDb"));

builder.Services.AddScoped<IPlantService, PlantService>();
builder.Services.AddScoped<IPlantSpecieService, PlantSpecieService>();

builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<IPlantSpecieRepository, PlantSpecieRepository>();

builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new ApiVersion(1.0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
        opt.ApiVersionReader = new QueryStringApiVersionReader();
    })
    .AddMvc()
    .AddApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(opt => opt.OperationFilter<SwaggerDefaultValues>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            opt.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
}