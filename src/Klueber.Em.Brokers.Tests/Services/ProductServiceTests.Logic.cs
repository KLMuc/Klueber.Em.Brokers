using FluentAssertions;
using Klueber.Em.Brokers.Models.ApiModels.Product;
using Klueber.Em.Brokers.Models.ApiModels.Results;
using Moq;

namespace Klueber.Em.Brokers.Tests.Services
{
    public partial class ProductServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAllKlueberProducts_OnGetKlueberProductsAsync()
        {
            // Arrange
            List<dynamic> dynamicProductCollection = CreateRandomKlueberProducts();
            List<KlueberProduct> expectedProducts = dynamicProductCollection
                .Select(dynamicProduct => new KlueberProduct
                {
                    Id = dynamicProduct.Id,
                    Number = dynamicProduct.Number,
                    Term = dynamicProduct.Term
                }).ToList();

            this.apiBrokerMock.Setup(broker => broker.GetKlueberProductsAsync())
                 .ReturnsAsync(new GenericOperationResult<List<KlueberProduct>>(true)
                 {
                     Data = expectedProducts
                 });

            // Act
            var actualProducts = await productService.GetKlueberProductsAsync();

            // Assert
            this.apiBrokerMock.Verify(broker => broker.GetKlueberProductsAsync(), Times.Once);
            actualProducts.Should().BeEquivalentTo(expectedProducts);
            actualProducts.Should().AllSatisfy(product =>
            {
                product.Id.Should().NotBe(0);
                product.Number.Should().NotBeNullOrWhiteSpace();
                product.Term.Should().NotBeNullOrWhiteSpace();
            });
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
