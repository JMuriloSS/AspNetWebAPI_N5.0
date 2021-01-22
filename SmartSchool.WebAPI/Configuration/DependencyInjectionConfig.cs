using Microsoft.Extensions.DependencyInjection;
using SmartSchool.WebAPI.Data;

namespace SmartSchool.WebAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
        }
    }
}