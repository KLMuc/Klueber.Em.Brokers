using System.Collections.Generic;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Brokers.Apis;
using Klueber.Em.Brokers.Brokers.Loggings;
using Klueber.Em.Brokers.Models.ApiModels.Product;

namespace Klueber.Em.Brokers.Services.Product
{
    public partial class ProductService : IProductService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IApiBroker apiBroker;

        public ProductService(ILoggingBroker loggingBroker, IApiBroker apiBroker)
        {
            this.loggingBroker = loggingBroker;
            this.apiBroker = apiBroker;
        }

        public async ValueTask<List<KlueberProduct>> GetKlueberProductsAsync() =>
        await TryCatch(async () =>
        {
            var result = await this.apiBroker.GetKlueberProductsAsync();
            return result.Data;

        });
    }
}
