using Contracts;
using Repository;
using Service;
using Service.Contracts;

namespace WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("_policy", builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerService.LoggerManager>();
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
                services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();
    }
}
