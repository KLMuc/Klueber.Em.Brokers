using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.Exceptions;

namespace Klueber.Em.Brokers.Clients.Services
{
    public static class ValidationService
    {
        public static async ValueTask ValidateHttpResponseAsync(HttpResponseMessage httpResponseMessage)
        {
            string content = await httpResponseMessage.Content.ReadAsStringAsync();
            var statusCode = httpResponseMessage.StatusCode;
            switch (statusCode)
            {
                case HttpStatusCode.BadGateway:
                    throw new HttpResponseBadGatewayException(httpResponseMessage, content);
                case HttpStatusCode.InternalServerError:
                    throw new HttpResponseInternalServerErrorException(httpResponseMessage, content);
                case HttpStatusCode.ServiceUnavailable:
                    throw new HttpResponseServiceUnavailableException(httpResponseMessage, content);
                case HttpStatusCode.GatewayTimeout:
                    throw new HttpResponseGatewayTimeoutException(httpResponseMessage, content);

                case HttpStatusCode.BadRequest:
                    throw new HttpResponseBadRequestException(httpResponseMessage, content);
                case HttpStatusCode.Unauthorized:
                    throw new HttpResponseUnauthorizedException(httpResponseMessage, content);
                case HttpStatusCode.Forbidden:
                    throw new HttpResponseForbiddenException(httpResponseMessage, content);
                case HttpStatusCode.NotFound when NotFoundWithNoContent(httpResponseMessage):
                    throw new HttpResponseUrlNotFoundException(httpResponseMessage, content);
                case HttpStatusCode.NotFound:
                    throw new HttpResponseNotFoundException(httpResponseMessage, content);
                case HttpStatusCode.RequestTimeout:
                    throw new HttpResponseRequestTimeoutException(httpResponseMessage, content);
            }
        }

        private static bool NotFoundWithNoContent(HttpResponseMessage httpResponseMessage) =>
            httpResponseMessage.Content.Headers.Contains("Content-Type") == false
            && httpResponseMessage.StatusCode == HttpStatusCode.NotFound;
    }
}
