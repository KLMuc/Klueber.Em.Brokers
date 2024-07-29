using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Tenant.Exceptions
{
    public class TenantApiFailedMessageResultException : Exception
    {
        public TenantApiFailedMessageResultException(string message) : base(message)
        {

        }
    }
}
