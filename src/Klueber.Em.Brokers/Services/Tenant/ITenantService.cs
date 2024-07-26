using System.Collections.Generic;
using System.Threading.Tasks;

namespace Klueber.Em.Brokers.Services.Tenant;

public interface ITenantService
{
    ValueTask<List<Models.ApiModels.Tenant.Tenant>> GetTenantsAsync();
}