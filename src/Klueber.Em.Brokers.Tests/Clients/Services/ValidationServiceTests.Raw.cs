using System.Net;
using FluentAssertions;
using Klueber.Em.Brokers.Clients.Services;
using Klueber.Em.Brokers.Models.Exceptions;

namespace Klueber.Em.Brokers.Tests.Clients.Services
{
    public partial class ValidationServiceTests
    {
        [Fact]
        private async Task ShouldThrowHttpResponseBadGatewayExceptionIfResponseStatusCodeWasBadGatewayAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage badGatewayResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.BadGateway, content);

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(badGatewayResponseMessage);

            // assert
            HttpResponseBadGatewayException httpResponseBadGatewayException =
                await Assert.ThrowsAsync<HttpResponseBadGatewayException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseBadGatewayException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }
       
        [Fact]
        private async Task ShouldThrowInternalServerErrorExceptionIfResponseStatusCodeWasInternalServerErrorAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage internalServerErrorResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.InternalServerError, content);

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(internalServerErrorResponseMessage);

            // assert
            HttpResponseInternalServerErrorException httpResponseInternalServerErrorException =
                await Assert.ThrowsAsync<HttpResponseInternalServerErrorException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseInternalServerErrorException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }
      
        [Fact]
        private async Task ShouldThrowServiceUnavailableExceptionIfResponseStatusCodeWasServiceUnavailableAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage serviceUnavailableResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.ServiceUnavailable, content);

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(serviceUnavailableResponseMessage);

            // assert
            HttpResponseServiceUnavailableException httpResponseServiceUnavailableException =
                await Assert.ThrowsAsync<HttpResponseServiceUnavailableException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseServiceUnavailableException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }

        [Fact]
        private async Task ShouldThrowHttpResponseGatewayTimeoutExceptionIfResponseStatusCodeWasGatewayTimeoutAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage gatewayTimeoutResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.GatewayTimeout, content);

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(gatewayTimeoutResponseMessage);

            // assert
            HttpResponseGatewayTimeoutException httpResponseGatewayTimeoutException =
                await Assert.ThrowsAsync<HttpResponseGatewayTimeoutException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseGatewayTimeoutException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }

        [Fact]
        private async Task ShouldThrowHttpResponseBadRequestExceptionIfResponseStatusCodeWasBadRequestAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage badRequestResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.BadRequest, content);

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(badRequestResponseMessage);

            // assert
            HttpResponseBadRequestException httpResponseBadRequestException =
                await Assert.ThrowsAsync<HttpResponseBadRequestException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseBadRequestException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }
     
        [Fact]
        private async Task ShouldThrowHttpResponseUnauthorizedExceptionIfResponseStatusCodeWasUnauthorizedAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage unauthorizedResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.Unauthorized, content);

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(unauthorizedResponseMessage);

            // assert
            HttpResponseUnauthorizedException httpResponseUnauthorizedException =
                await Assert.ThrowsAsync<HttpResponseUnauthorizedException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseUnauthorizedException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }
       
        [Fact]
        private async Task ShouldThrowHttpResponseForbiddenExceptionIfResponseStatusCodeWasForbiddenAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage forbiddenResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.Forbidden, content);

            // act 
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(forbiddenResponseMessage);

            // assert
            HttpResponseForbiddenException httpResponseForbiddenException =
                await Assert.ThrowsAsync<HttpResponseForbiddenException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseForbiddenException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }

        [Fact]
        private async Task ShouldThrowHttpResponseUrlNotFoundExceptionIfResponseStatusCodeWasNotFoundAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage notFoundResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.NotFound, content);

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(notFoundResponseMessage);

            // assert
            HttpResponseUrlNotFoundException httpResponseNotFoundException =
                await Assert.ThrowsAsync<HttpResponseUrlNotFoundException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseNotFoundException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }

        [Fact]
        private async Task ShouldThrowHttpResponseNotFoundExceptionIfResponseStatusCodeWasNotFoundAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;

            HttpResponseMessage notFoundResponseMessage =
                CreateHttpResponseMessage(HttpStatusCode.NotFound, content);

            notFoundResponseMessage.Content.Headers.Add("Content-Type", "text/json");

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(notFoundResponseMessage);

            // assert
            HttpResponseNotFoundException httpResponseNotFoundException =
                await Assert.ThrowsAsync<HttpResponseNotFoundException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseNotFoundException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }

        [Fact]
        private async Task ShouldThrowHttpResponseRequestTimeoutExceptionIfResponseStatusCodeWasRequestTimeoutAsync()
        {
            // arrange
            string randomContent = GetRandomContent();
            string content = randomContent;
            string expectedExceptionMessage = content;
            var requestTimeoutResponseMessage = CreateHttpResponseMessage(HttpStatusCode.RequestTimeout, content);

            // act
            ValueTask validateHttpResponseTask =
                ValidationService.ValidateHttpResponseAsync(requestTimeoutResponseMessage);

            // assert
            HttpResponseRequestTimeoutException httpResponseRequestTimeoutException =
                await Assert.ThrowsAsync<HttpResponseRequestTimeoutException>(() =>
                    validateHttpResponseTask.AsTask());

            httpResponseRequestTimeoutException.Message.Should().BeEquivalentTo(expectedExceptionMessage);
        }
    }
}
