using System.Linq;
using Klueber.Em.Brokers.Models.ApiModels.Results;
using Klueber.Em.Brokers.Models.ApiModels.Tenant.Exceptions;

namespace Klueber.Em.Brokers.Services.Tenant
{
    public partial class TenantService
    {
        private static void ValidateApiResult(GenericOperationResult apiResult)
        {
            if (apiResult.IsSuccess == false)
            {
                throw new TenantApiFailedMessageResultException(apiResult.Errors.FirstOrDefault()?.ErrorMessage);
            }
        }
    }
}
