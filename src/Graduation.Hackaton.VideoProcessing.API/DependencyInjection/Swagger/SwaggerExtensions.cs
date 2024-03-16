using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Graduation.Hackaton.VideoProcessing.API.DependencyInjection.Swagger;

internal static class SwaggerExtensions
{
    internal static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();

            options.OperationFilter<ApiKeyHeaderParameter>();

            options.IncludeCommentsToApiDocumentation();
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    private static void IncludeCommentsToApiDocumentation(this SwaggerGenOptions options)
    {
        try
        {
            options.TryIncludeCommentsToApiDocumentation();
        }
        catch (Exception ex)
        {            
            Console.WriteLine(ex.Message);
        }
    }

    private static void TryIncludeCommentsToApiDocumentation(this SwaggerGenOptions options)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        if (File.Exists(xmlPath))
        {
            options.IncludeXmlComments(xmlPath);
        }
    }

    internal static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var groupName in provider.ApiVersionDescriptions.Select(description => description.GroupName))
            {
                options.SwaggerEndpoint($"/swagger/{groupName}/swagger.json", groupName.ToUpperInvariant());
            }
        });

        return app;
    }
}
