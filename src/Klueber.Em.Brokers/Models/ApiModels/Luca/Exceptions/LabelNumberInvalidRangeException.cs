using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class LabelNumberInvalidRangeException : Exception
    {
        public LabelNumberInvalidRangeException() : base("Invalid range for a label") { }
    }
}
