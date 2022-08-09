using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Repository;
using WebAPI.Extensions;
using WebAPI.Helper;
using WebAPI.Interfaces;
using WebAPI.Presentation.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddScoped<ValidationFilterAttribute>();

builder.Services.AddControllers()
    .AddApplicationPart(typeof(WebAPI.Presentation.AssemblyReference).Assembly);
builder.Services.ConfigureCors();


builder.Services.AddDbContext<CarDealershipContext>(opts => 
    opts.UseSqlServer(builder.Configuration.GetConnectionString("CarDealershipConnection"),
    x => x.MigrationsAssembly("WebAPI").UseNetTopologySuite()));

builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
// Configure the HTTP request pipeline.
app.ConfigureExceptionHandler(logger);
app.UseHttpsRedirection();

app.UseCors("_policy");

app.UseAuthorization();

app.MapControllers();

app.Run();
