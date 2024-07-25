using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions
{
    public class LabelNumberNullException : Exception
    {
        public LabelNumberNullException() : base("Label number is needed") { }
    }
}
