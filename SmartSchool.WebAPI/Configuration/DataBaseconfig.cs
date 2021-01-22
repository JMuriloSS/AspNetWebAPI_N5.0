using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSchool.WebAPI.Data.Context;

namespace SmartSchool.WebAPI.Configuration
{
    public static class DataBaseconfig
    {
        public static void AddDatabaseConfiguration(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddDbContext<SmartContext>(context => 
                context.UseSqlite(configuration.GetConnectionString("Default")));
        }
    }
}