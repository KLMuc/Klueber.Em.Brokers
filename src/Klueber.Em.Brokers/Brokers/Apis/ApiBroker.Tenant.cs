using System.Collections.Generic;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Results;
using Klueber.Em.Brokers.Models.ApiModels.Tenant;


namespace Klueber.Em.Brokers.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string TenantsRelativeUrl = "api/v1/Tenants";

        public async ValueTask<GenericOperationResult<List<Tenant>>> GetTenantsAsync() =>
            await this.apiClient
                .GetContentAsync<GenericOperationResult<List<Tenant>>>(
                    $"{TenantsRelativeUrl}");
    }
}
