using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class OperatingTemperatureException : Exception
    {
        public OperatingTemperatureException() : base("please specify an Unit if the value is not null.")
        {

        }
    }
}
