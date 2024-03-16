﻿using Graduation.Hackaton.VideoProcessing.API.DependencyInjection.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Hackaton.VideoProcessing.API.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiDependencyInjection(this IServiceCollection services)
        {
            return services.AddApiConfiguration()
                .AddSwaggerConfiguration();
        }

        private static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(o => o.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));
            
            services.AddControllers();

            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });

            return services;

        }
    }
}
