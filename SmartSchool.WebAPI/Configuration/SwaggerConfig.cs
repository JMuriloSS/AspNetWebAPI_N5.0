using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace SmartSchool.WebAPI.Configuration
{
    public static class SwaggerConfig
    {
        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var apiProviderDescription = services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        new OpenApiInfo
                        {
                            Title = "SmartSchool.WebAPI",
                            Version = description.ApiVersion.ToString(),
                            TermsOfService = new Uri("https://github.com/JMuriloSS"),
                            Description = "WebAPI do SmartSchool para estudos.",
                            License = new Microsoft.OpenApi.Models.OpenApiLicense
                            {
                                Name = "SamrtSchool License",
                                Url = new Uri("https://github.com/JMuriloSS")
                            },
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact
                            {
                                Name = "José Murilo",
                                Email = "jmprg07@gmail.com",
                                Url = new Uri("https://github.com/JMuriloSS")
                            }
                        }
                    );
                }

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }


        public static void UseSwaggerConfiguration(this IApplicationBuilder app, IApiVersionDescriptionProvider apiProviderDescription)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = string.Empty;
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}