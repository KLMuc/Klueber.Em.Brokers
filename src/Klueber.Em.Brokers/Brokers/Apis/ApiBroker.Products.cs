using System.Collections.Generic;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Product;
using Klueber.Em.Brokers.Models.ApiModels.Results;

namespace Klueber.Em.Brokers.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string ProductsRelativeUrl = "api/v2/GlobalManufacturers";

        public async ValueTask<GenericOperationResult<List<KlueberProduct>>> GetKlueberProductsAsync() =>
            await this.apiClient
                .GetContentAsync<GenericOperationResult<List<KlueberProduct>>>(
                    $"{ProductsRelativeUrl}/1/products");
    }
}
