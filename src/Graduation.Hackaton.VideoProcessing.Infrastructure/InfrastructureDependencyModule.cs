using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Event;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Logger;
using Graduation.Hackaton.VideoProcessing.Infrastructure.Database.Configuration;
using Graduation.Hackaton.VideoProcessing.Infrastructure.Database;
using Graduation.Hackaton.VideoProcessing.Infrastructure.Event;
using Graduation.Hackaton.VideoProcessing.Infrastructure.Logger;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.Repositories;
using Graduation.Hackaton.VideoProcessing.Infrastructure.Database.Repositories;
using Graduation.Hackaton.VideoProcessing.Domain.Gateways.VideoProcessingApi;
using Graduation.Hackaton.VideoProcessing.Infrastructure.Integrations;

namespace Graduation.Hackaton.VideoProcessing.Infrastructure
{
    public static class InfrastructureDependencyModule
    {
        public static IServiceCollection AddLoggingGateway(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerGateway, ConsoleLoggerGateway>();

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IVideoRepositoryGateway, MongoDbVideoRepositoryGateway>();

            services.AddSingleton<IMongoDbGateway, MongoDbGateway>();

            return services;
        }

        public static IServiceCollection AddVideoProcessingApiIntegration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IVideoProcessingApiGateway, VideoProcessingApiGateway>(client =>
            {
                client.BaseAddress = new Uri(configuration.GetValue("Gateways:Integrations:VideoProcessingApi:BaseAddress", ""));
                client.Timeout = new TimeSpan(0, 0, configuration.GetValue("Gateways:Integrations:VideoProcessingApi:TimeoutInSeconds", 30));
            });

            return services;
        }

        public static IServiceCollection AddApiEventGateway(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEventSenderGateway, MassTransitSenderGateway>();

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(configuration.GetValue<string>("Gateways:Event:ServiceBus:ConnectionString", ""), h => { });

                    cfg.AddMessagesTypes();
                });
            });

            return services;
        }

        public static IServiceCollection AddWorkerEventGateway<TWorker>(this IServiceCollection services, IConfiguration configuration, Type workerType, string queueName)
            where TWorker : class, IConsumer
        {
            services.AddScoped<IEventSenderGateway, MassTransitSenderGateway>();

            services.AddMassTransit(busConfiguration =>
            {
                busConfiguration.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(configuration.GetValue<string>("Gateways:Event:ServiceBus:ConnectionString", ""), h => { });

                    cfg.ReceiveEndpoint(queueName, e =>
                    {
                        e.Consumer<TWorker>(context);
                    });
                });

                busConfiguration.AddConsumer(workerType);
            });

            return services;
        }
        private static IServiceBusBusFactoryConfigurator AddMessagesTypes(this IServiceBusBusFactoryConfigurator cfg)
        {
            //Insert all messages types that are sent by the API
            cfg.Message<ProcessVideoEvent>(m => m.SetEntityName(nameof(ProcessVideoEvent).ToLower()));

            return cfg;
        }
    }
}
