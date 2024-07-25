using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class CreateRequestNullException : Exception
    {
        public CreateRequestNullException() : base("CreateRequest should not be null") { }

    }
}
