using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Product.Exceptions
{
    public class ProductServiceException : Exception
    {
        public ProductServiceException(Exception innerException)
            : base("Product service dependency error occurred, please contact support.", innerException) { }
    }
}
