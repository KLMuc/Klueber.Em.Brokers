﻿using System.Net.Http;

namespace Klueber.Em.Brokers.Models.Exceptions
{
    public class HttpResponseUnauthorizedException : HttpResponseException
    {
        public HttpResponseUnauthorizedException(HttpResponseMessage responseMessage, string message)
            : base(responseMessage, message) { }

    }
}

