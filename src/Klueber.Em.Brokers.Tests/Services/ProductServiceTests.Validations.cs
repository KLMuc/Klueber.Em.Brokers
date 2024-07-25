using Klueber.Em.Brokers.Models.ApiModels.Product;
using Klueber.Em.Brokers.Models.ApiModels.Product.Exceptions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Klueber.Em.Brokers.Tests.Services
{
    public partial class ProductServiceTests
    {
        [Fact]
        public void GetKlueberProductsAsync_ThrowsException_InvokesLogError()
        {
            // Arrange
            var exception = new BadHttpRequestException("Bad Request",500);
            
            this.apiBrokerMock.Setup(broker => broker.GetKlueberProductsAsync())
                .ThrowsAsync(exception);

            // Act
            this.productService.GetKlueberProductsAsync();

            // Assert 
            this.loggingBrokerMock.Verify();

            this.apiBrokerMock.Verify(broker =>
                broker.GetKlueberProductsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.IsAny<ProductServiceException>()), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
