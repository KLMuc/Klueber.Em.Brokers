using FluentAssertions;
using Klueber.Em.Brokers.Models.ApiModels.Results;
using Klueber.Em.Brokers.Models.ApiModels.Tenant;
using Moq;

namespace Klueber.Em.Brokers.Tests.Services
{
    public partial class TenantServiceTests
    {
        [Fact]
        public async Task ShouldHaveItems_OnGetTenantsAsync()
        {
            // Arrange
            var expectedItem = CreateRandomTenantList();

            this.apiBrokerMock.Setup(x => x.GetTenantsAsync())
                .ReturnsAsync(new GenericOperationResult<List<Tenant>>(true)
                {
                    Data = expectedItem
                });

            // Act
            var result = await this.tenantService.GetTenantsAsync();

            // Assert
            this.apiBrokerMock.Verify(x => x.GetTenantsAsync(), Times.Once);
            
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            
            result.Should().BeEquivalentTo(expectedItem);
        }
    }
}
