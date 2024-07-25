using System.Net.Http;

namespace Klueber.Em.Brokers.Models.Exceptions
{
    public class HttpResponseInternalServerErrorException : HttpResponseException
    {
        public HttpResponseInternalServerErrorException(HttpResponseMessage responseMessage, string message)
            : base(responseMessage, message) { }

    }
}

