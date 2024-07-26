using Klueber.Em.Brokers.Models.ApiModels.Results;
using Klueber.Em.Brokers.Models.ApiModels.Tenant;
using Klueber.Em.Brokers.Models.ApiModels.Tenant.Exceptions;
using Moq;

namespace Klueber.Em.Brokers.Tests.Services
{
    public partial class TenantServiceTests
    {
        [Fact]
        public async Task ShouldThrowServiceException_OnGetTenantAsync_IfApiResultNotSuccess_AndLogItAsync()
        {
            // arrange
            var tenantApiException = new TenantApiFailedMessageResultException("Error");
            var expectedTenantServiceException =
                new TenantServiceException(tenantApiException);

            this.apiBrokerMock.Setup(broker =>
                    broker.GetTenantsAsync())
                .ReturnsAsync(new GenericOperationResult<List<Tenant>>(false)
                {
                    Errors = new List<ApplicationError>
                    {
                        new ApplicationError() { ErrorMessage = "Error" }
                    }
                });

            // Act
            this.tenantService.GetTenantsAsync();

            // Assert
            this.apiBrokerMock.Verify(broker => broker.GetTenantsAsync(), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(expectedTenantServiceException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();

        }
    }
}
