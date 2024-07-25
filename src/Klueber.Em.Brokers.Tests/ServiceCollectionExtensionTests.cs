using FluentAssertions;
using Klueber.Em.Brokers.Brokers.Apis;
using Klueber.Em.Brokers.Services.Luca;
using Klueber.Em.Brokers.Services.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Klueber.Em.Brokers.Tests
{
    public class ServiceCollectionExtensionTests
    {
        [Fact]
        public void ApiBrokerNotNull_WithHttpClientConfigured_AfterAddEMBrokersCalled()
        {
            // Arrange
            var webHostBuilder = new WebHostBuilder()
                .Configure(app => { })
                .ConfigureServices(services =>
                {
                    services.AddEmBrokers(client =>
                    {
                        client.BaseAddress = new Uri("https://example.com/api/");
                    });
                });

            var testServer = new TestServer(webHostBuilder);

            // Act
            var serviceProvider = testServer.Host.Services;
            var apiBroker = serviceProvider.GetService<IApiBroker>();
            var lucaService = serviceProvider.GetService<ILucaService>();
            var productService = serviceProvider.GetService<IProductService>();

            // Assert
            apiBroker.Should().NotBeNull();
            lucaService.Should().NotBeNull();
            productService.Should().NotBeNull();
        }
    }
}
