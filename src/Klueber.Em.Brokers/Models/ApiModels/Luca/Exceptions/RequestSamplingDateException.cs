using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class RequestSamplingDateException : Exception
    {
        public RequestSamplingDateException() : base("SamplingDate could not be correct")
        {

        }
    }
}
