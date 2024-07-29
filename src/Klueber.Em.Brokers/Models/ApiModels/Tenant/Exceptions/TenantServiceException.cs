using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Tenant.Exceptions
{
    public class TenantServiceException : Exception
    {
        public TenantServiceException(Exception innerException)
            : base("Tenant service dependency error occurred, please contact support.", innerException) { }
    }
}
