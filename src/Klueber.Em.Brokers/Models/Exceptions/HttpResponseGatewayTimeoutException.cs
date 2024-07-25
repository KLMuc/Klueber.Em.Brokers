using System.Net.Http;

namespace Klueber.Em.Brokers.Models.Exceptions
{
    public class HttpResponseGatewayTimeoutException : HttpResponseException
    {
        public HttpResponseGatewayTimeoutException(HttpResponseMessage responseMessage, string message)
            : base(responseMessage, message) { }

    }
}

