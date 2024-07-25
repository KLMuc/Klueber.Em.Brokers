using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class LucaServiceException : Exception
    {
        public LucaServiceException(Exception innerException)
            : base(message: "Luca service error occurred.", innerException) { }
    }
}
