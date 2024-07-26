using System.Collections.Generic;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Results;
using Klueber.Em.Brokers.Models.ApiModels.Tenant;

namespace Klueber.Em.Brokers.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<GenericOperationResult<List<Tenant>>> GetTenantsAsync();
    }
}
