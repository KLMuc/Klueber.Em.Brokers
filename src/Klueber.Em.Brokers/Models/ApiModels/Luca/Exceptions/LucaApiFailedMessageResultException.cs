using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class LucaApiFailedMessageResultException : Exception
    {
        public LucaApiFailedMessageResultException(string message) : base(message)
        {

        }
    }
}
