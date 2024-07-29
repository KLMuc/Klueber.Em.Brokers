using System.Collections.Generic;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Brokers.Apis;
using Klueber.Em.Brokers.Brokers.Loggings;

namespace Klueber.Em.Brokers.Services.Tenant
{
    public partial class TenantService : ITenantService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public TenantService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<List<Models.ApiModels.Tenant.Tenant>> GetTenantsAsync() =>
        await TryCatch(async () =>
        {
            var result = await this.apiBroker.GetTenantsAsync();
            ValidateApiResult(result);
            return result.Data;
        });
    }
}
