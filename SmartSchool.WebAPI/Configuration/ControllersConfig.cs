using Microsoft.Extensions.DependencyInjection;

namespace SmartSchool.WebAPI.Configuration
{
    public static class ControllersConfig
    {
        public static void AddControllersConfiguration(this IServiceCollection services)
        {
            services.AddControllers()
                    .AddNewtonsoftJson(
                        options => options.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );
        }
    }
}