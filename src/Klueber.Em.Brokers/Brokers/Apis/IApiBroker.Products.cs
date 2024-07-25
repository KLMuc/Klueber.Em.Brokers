using System.Collections.Generic;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Product;
using Klueber.Em.Brokers.Models.ApiModels.Results;

namespace Klueber.Em.Brokers.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<GenericOperationResult<List<KlueberProduct>>> GetKlueberProductsAsync();
    }
}
