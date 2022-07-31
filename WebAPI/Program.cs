using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Helper;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var policy = "_policy";

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policy, builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddCors();
builder.Services.AddDbContext<CarDealershipContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarDealershipConnection"), x => x.UseNetTopologySuite());
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IDealerShopRepository, DealerShopRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IPhotoRepository<PhotoForDealerShop>, PhotoRepository<PhotoForDealerShop>>();
builder.Services.AddScoped<IPhotoRepository<PhotoForCar>, PhotoRepository<PhotoForCar>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseCors(policy);

app.UseAuthorization();

app.MapControllers();

app.Run();
