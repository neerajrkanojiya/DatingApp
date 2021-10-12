using API.Data;
using API.Interface;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
             //following ITokenService,TokenServices are requierd only for testing.
            services.AddScoped<ITokenService,TokenServices>();
            services.AddDbContext<DataContext>(options =>
            {
                object p = options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}