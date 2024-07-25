using System.Net.Http;
using System;
using Klueber.Em.Brokers.Brokers.Apis;
using Klueber.Em.Brokers.Brokers.Loggings;
using Klueber.Em.Brokers.Clients;
using Klueber.Em.Brokers.Services.Luca;
using Klueber.Em.Brokers.Services.Product;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Klueber.Em.Brokers
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEmBrokers(this IServiceCollection services,
            Action<HttpClient> configureClient,
            Action<IHttpClientBuilder> configureHttpMessageHandler = null)
        {
            services.AddScoped<IRestApiClient, RestApiClient>();
            var httpClientBuilder = services.AddHttpClient<IApiBroker, ApiBroker>(client =>
             {
                 configureClient(client);
             });
            configureHttpMessageHandler?.Invoke(httpClientBuilder);

            services.AddEmBrokerServices();
            
            return services;
        }

        private static IServiceCollection AddEmBrokerServices(this IServiceCollection services)
        {
            services.AddScoped<ILucaService, LucaService>();
            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<ILogger, Logger<LoggingBroker>>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();

            return services;
        }
    }
}
