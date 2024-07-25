using System.Collections.Generic;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Product;

namespace Klueber.Em.Brokers.Services.Product
{
    public partial interface IProductService
    {
        ValueTask<List<KlueberProduct>> GetKlueberProductsAsync();
    }
}
