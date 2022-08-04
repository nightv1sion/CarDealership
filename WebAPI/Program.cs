using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repository;
using WebAPI.Extensions;
using WebAPI.Helper;
using WebAPI.Interfaces;
using WebAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(WebAPI.Presentation.AssemblyReference).Assembly);
builder.Services.ConfigureCors();


builder.Services.AddDbContext<CarDealershipContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarDealershipConnection"), x => x.UseNetTopologySuite());
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IPhotoRepository<PhotoForDealerShop>, PhotoRepository<PhotoForDealerShop>>();
builder.Services.AddScoped<IPhotoRepository<PhotoForCar>, PhotoRepository<PhotoForCar>>();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
// Configure the HTTP request pipeline.
app.ConfigureExceptionHandler(logger);
app.UseHttpsRedirection();

app.UseCors("_policy");

app.UseAuthorization();

app.MapControllers();

app.Run();
