using System.Net.Http;

namespace Klueber.Em.Brokers.Models.Exceptions
{
    public class HttpResponseNotFoundException : HttpResponseException
    {
       public HttpResponseNotFoundException(HttpResponseMessage responseMessage, string message)
            : base(responseMessage, message) { }

    }
}

