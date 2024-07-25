using FluentAssertions;
using Klueber.Em.Brokers.Tests.Models;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace Klueber.Em.Brokers.Tests.Clients
{
    public partial class RestApiClientTests
    {
        [Fact]
        private async Task ShouldGetContentWithDeserializationFunctionAsync()
        {
            // Arrange
            TEntity randomTEntity = GetRandomTEntity();
            TEntity expectedResponseEntity = randomTEntity;

            this.wiremockServer.Given(Request.Create()
                    .WithPath(relativeUrl)
                    .UsingGet())
                .RespondWith(Response.Create()
                    .WithStatusCode(200)
                    .WithBodyAsJson(expectedResponseEntity));

            // Act
            TEntity actualTEntity =
                await this.restApiClient.GetContentAsync<TEntity>(
                    relativeUrl: relativeUrl,
                    deserializationFunction: ContentDeserializationFunction);

            // Assert
            actualTEntity.Should().BeEquivalentTo(expectedResponseEntity);
        }

        [Fact]
        private async Task ShouldCancelGetContentDeserializationIfCancellationInvokedAsync()
        {
            // Arrange
            TEntity someContent = GetRandomTEntity();
            var expectedCanceledTaskException = new TaskCanceledException();
            var taskCancelInvoked = new CancellationToken(canceled: true);

            this.wiremockServer.Given(Request.Create()
                    .WithPath(relativeUrl)
                    .UsingGet())
                        .RespondWith(Response.Create()
                            .WithBodyAsJson(someContent));

            // Act
            var actualCanceledTask =
                await Assert.ThrowsAsync<TaskCanceledException>(async () =>
                    await this.restApiClient.GetContentAsync<TEntity>(
                        relativeUrl: relativeUrl,
                        cancellationToken: taskCancelInvoked,
                        deserializationFunction: ContentDeserializationFunction));

            // Assert
            actualCanceledTask.GetType().Should().Be(expectedCanceledTaskException.GetType());
        }
    }
}
