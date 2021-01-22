using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSchool.WebAPI.Configuration
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}