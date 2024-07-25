using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class LucaValidationException : Exception
    {
        public LucaValidationException(Exception innerException)
         : base("LuCA validation failed", innerException) { }
    }
}
