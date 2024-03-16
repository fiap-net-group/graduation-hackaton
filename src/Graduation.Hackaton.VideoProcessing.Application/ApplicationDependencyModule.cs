using FluentValidation;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.ProcessVideo.Definitions;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.SendVideoToProcess;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.SendVideoToProcess.Boundaries;
using Graduation.Hackaton.VideoProcessing.Application.VideoProcessing.SendVideoToProcess.Definitions;
using Microsoft.Extensions.DependencyInjection;

namespace Graduation.Hackaton.VideoProcessing.Application
{
    public static class ApplicationDependencyModule
    {
        public static IServiceCollection AddApiApplicationConfiguration(this IServiceCollection services)
        {
            return services.AddApiUseCases()
                           .AddValidators()
                           .AddMappers();
        }

        public static IServiceCollection AddWorkerApplicationConfiguration(this IServiceCollection services)
        {
            return services.AddWorkerUseCases()
                           .AddValidators()
                           .AddMappers();
        }

        private static IServiceCollection AddApiUseCases(this IServiceCollection services)
        {
            services.AddScoped<ISendVideoToProcessUseCase, SendVideoToProcessInteractor>();

            return services;
        }

        private static IServiceCollection AddWorkerUseCases(this IServiceCollection services)
        {
            services.AddScoped<IProcessVideoUseCase, ProcessVideoInteractor>();

            return services;
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProcessVideoMapper));

            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<ProcessVideoValidator>();

            return services;
        }
    }
}
