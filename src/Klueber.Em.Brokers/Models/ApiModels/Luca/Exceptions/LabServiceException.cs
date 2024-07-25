using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class LabServiceException : Exception
    {
        public LabServiceException() : base("An error occurred lab service are not correct.") { }
    }
}
