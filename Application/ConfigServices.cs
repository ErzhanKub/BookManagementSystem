using Application.Books.Commands;
using Application.Pipelines;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    /// <summary>
    /// Extension class for IServiceCollection.
    /// Класс-расширение для IServiceCollection.
    /// </summary>
    public static class ConfigServices
    {
        /// <summary>
        /// Registers the MediatR service.
        /// Регистрирует сервис MediatR.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Коллекция сервисов.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(
                    Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(
               typeof(IPipelineBehavior<,>),
               typeof(ValidationPipeline<,>));

            return services;
        }
    }

}
