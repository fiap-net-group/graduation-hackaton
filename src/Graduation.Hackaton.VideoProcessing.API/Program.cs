using Graduation.Hackaton.VideoProcessing.API.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
               .SetBasePath(builder.Environment.ContentRootPath)
               .AddJsonFile("appsettings.json", true, true)
               .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
               .AddEnvironmentVariables();

builder.Services.AddApiDependencyInjection();

var app = builder.Build();

app.UseApiDependencyInjection();

app.MapControllers();

app.Run();
