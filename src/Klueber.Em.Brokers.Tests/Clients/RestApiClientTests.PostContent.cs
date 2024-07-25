using FluentAssertions;
using Klueber.Em.Brokers.Tests.Models;
using WireMock.ResponseBuilders;
using WireMock.RequestBuilders;

namespace Klueber.Em.Brokers.Tests.Clients
{
    public partial class RestApiClientTests
    {
        [Fact]
        private async Task ShouldPostContentWithStreamResponseAsync()
        {
            // Arrange
            string randomContent = CreateRandomContent();
            var cancellationToken = new CancellationToken();
            string mediaType = "text/json";
            bool ignoreDefaultValues = false;

            this.wiremockServer.Given(
                    Request.Create()
                        .WithPath(relativeUrl)
                        .UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", mediaType)
                        .WithBody(randomContent));

            // Act
            Stream actualStream =
                await this.restApiClient.PostContentWithStreamResponseAsync(
                    relativeUrl: relativeUrl,
                    content: randomContent,
                    cancellationToken: cancellationToken,
                    mediaType: mediaType,
                    ignoreDefaultValues: ignoreDefaultValues,
                    serializationFunction: SerializationContentFunction);

            string actualReadContent = await ReadStreamToEndAsync(actualStream);

            // Assert
            actualReadContent.Should().BeEquivalentTo(randomContent);
        }

        [Fact]
        private async Task ShouldPostContentWithTContentReturnsTResultAsync()
        {
            // Arrange
            TEntity randomTEntity = GetRandomTEntity();
            TEntity expectedTEntity = randomTEntity;
            string mediaType = "text/json";
            bool ignoreDefaultValues = false;

            string expectedBody =
                System.Text.Json.JsonSerializer.Serialize(randomTEntity);

            this.wiremockServer.Given(
                    Request.Create()
                        .WithPath(relativeUrl)
                        .UsingPost())
                .RespondWith(
                    Response.Create()
                        .WithStatusCode(200)
                        .WithHeader("Content-Type", mediaType)
                        .WithBody(expectedBody));

            // Act
            TEntity actualTEntity =
                await this.restApiClient.PostContentAsync<TEntity, TEntity>(
                    relativeUrl: relativeUrl,
                    content: randomTEntity,
                    mediaType: mediaType,
                    ignoreDefaultValues: ignoreDefaultValues,
                    serializationFunction: SerializationContentFunction,
                    deserializationFunction: ContentDeserializationFunction);

            // Assert
            actualTEntity.Should().BeEquivalentTo(expectedTEntity);
        }
    }
}