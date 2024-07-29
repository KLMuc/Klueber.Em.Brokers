using System;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Tenant.Exceptions;

namespace Klueber.Em.Brokers.Services.Tenant
{
    public partial class TenantService
    {
        private async ValueTask<T> TryCatch<T>(Func<ValueTask<T>> returningFunction)
        {
            try
            {
                return await returningFunction();
            }
            catch (TenantApiFailedMessageResultException e)
            {
                throw CreateAndLogTenantServiceException(e);
            }
            catch (Exception serviceException)
            {
                var tenantServiceException =
                    new TenantServiceException(serviceException);

                throw CreateAndLogTenantServiceException(tenantServiceException);
            }

        }

        private Exception CreateAndLogTenantServiceException(Exception exception)
        {
            var tenantServiceException =
                new TenantServiceException(exception);

            this.loggingBroker.LogError(tenantServiceException);

            return tenantServiceException;
        }
    }
}
