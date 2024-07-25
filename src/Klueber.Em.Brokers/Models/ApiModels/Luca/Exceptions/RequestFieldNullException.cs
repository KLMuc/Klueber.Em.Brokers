using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class RequestFieldNullException : Exception
    {
        public RequestFieldNullException(string fieldName) : base($"the Field {fieldName} should not be null or empty")
        { }
    }
}
