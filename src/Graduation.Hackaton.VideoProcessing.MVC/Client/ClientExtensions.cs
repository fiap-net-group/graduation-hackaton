using Polly;
using Polly.Extensions.Http;
using System.Text.Json;

namespace Graduation.Hackaton.VideoProcessing.MVC.Client
{
    public static class ClientExtensions
    {
        public const string VideoClient = nameof(VideoClient);

        public static IServiceCollection AddClientConfiguration(this IServiceCollection services)
        {
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            services.AddSingleton(HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }, (outcome, timespan, retryCount, context) =>
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Attempt: {retryCount}!");
                    Console.ForegroundColor = ConsoleColor.White;
                }));

            return services;
        }
    }
}
