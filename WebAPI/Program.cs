using Microsoft.EntityFrameworkCore;
using WebAPI;
using WebAPI.Models;

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarDealershipConnection"));
});
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseCors(policy);

app.UseAuthorization();

app.MapControllers();

app.Run();
