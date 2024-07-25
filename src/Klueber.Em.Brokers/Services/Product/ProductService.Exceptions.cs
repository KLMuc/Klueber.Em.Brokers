using System;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Product.Exceptions;

namespace Klueber.Em.Brokers.Services.Product
{
    public partial class ProductService
    {
        private async ValueTask<T> TryCatch<T>(Func<ValueTask<T>> returningFunction)
        {
            try
            {
                return await returningFunction();
            }
            catch (Exception serviceException)
            {
                var failedProductServiceException =
                    new ProductServiceException(serviceException);

                throw CreateAndLogProductServiceException(failedProductServiceException);
            }
        }

        private Exception CreateAndLogProductServiceException(Exception exception)
        {
            var productServiceException =
                new ProductServiceException(exception);

            this.loggingBroker.LogError(productServiceException);

            return productServiceException;
        }
    }
}
