using FluentAssertions;
using Klueber.Em.Brokers.Models.ApiModels.Luca;
using Klueber.Em.Brokers.Models.ApiModels.Results;
using Moq;

namespace Klueber.Em.Brokers.Tests.Services
{
    public partial class LucaServiceTests
    {
        [Fact]
        public async Task ShouldBeTrue_OnCanUseThisLabelAsync_IfLabelNumberUseAble()
        {
            // Arrange
            var labelNumber = 1000000001;
            var expectedResult = new GenericOperationResult(true);

            this.apiBrokerMock.Setup(b => 
                    b.CanUseLabelAsync(It.Is<int>(l => l == labelNumber)))
                .ReturnsAsync(expectedResult);

            // Act
            ValueTask<bool> sut = this.lucaService.CanUseThisLabelAsync(labelNumber);
            var result = await sut;

            // Assert
            this.apiBrokerMock.Verify(broker =>
                broker.CanUseLabelAsync(It.Is<int>(l => l == labelNumber)), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ShouldReturnRequest_OnCreateRequest()
        {
            // Arrange
            var request = new CreateRequest()
            {
                Service = LaboratoryService.GreaseAnalysis,
                LabelNumber = 1000000001,
                Manufacturer = "Kluber",
                ProductName = "Hotemp",
                LubricationPoint = "TestPoint",
                SamplingDate = DateTimeOffset.Now.AddDays(-1)
            };

            var expectedResult = new GenericOperationResult<Request>(true)
            {
                Data = CreateRandomLucaRequest(
                    request.LabelNumber,
                    request.Service,
                    request.Manufacturer,
                    request.ProductName,
                    request.LubricationPoint,
                    request.SamplingDate)
            };

            this.apiBrokerMock.Setup(b =>
                    b.CreateRequestAsync(It.Is<int>(t => t == 5), 
                        It.Is<CreateRequestModel>(model =>
                        model.SubscriptionId == 2 &&
                        model.TreeId == 5 &&
                        model.Data.LabelNumber == request.LabelNumber &&
                        model.Data.Manufacturer == request.Manufacturer &&
                        model.Data.ProductName == request.ProductName &&
                        model.Data.LubricationPoint == request.LubricationPoint &&
                        model.Data.SamplingDate == request.SamplingDate)))
                .ReturnsAsync(expectedResult);

            // Act
            var sut = this.lucaService.CreateRequestAsync(5, 2, request);
            var result = await sut;

            // Assert
            this.apiBrokerMock.Verify(broker =>
                broker.CreateRequestAsync(It.Is<int>(t => t == 5),
                    It.IsAny<CreateRequestModel>()), Times.Once);
            
            result.LabelNumber.Should().Be(expectedResult.Data.LabelNumber);
            result.Should().BeEquivalentTo(expectedResult.Data);
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldReturnRequests_OnGetRequest()
        {
            // Arrange
            var treeId = 5;
            var request = CreateRandomLucaRequest(1000000001, LaboratoryService.GreaseAnalysis, "Manufacturer",
                "Product", "LubPoint", DateTimeOffset.UtcNow);
            var expectedResult = new GenericOperationResult<List<Request>>(true)
            {
                Data = new List<Request>()
                {
                    request
                }
            };

            this.apiBrokerMock.Setup(b =>
                    b.GetRequests(It.Is<int>(t => t == treeId), 
                        It.Is<bool>(t => t == true)))
                .ReturnsAsync(expectedResult);

            // Act
            var sut = this.lucaService.GetRequests(treeId, true);
            var result = await sut;

            // Assert
            this.apiBrokerMock.Verify(broker =>
                broker.GetRequests(It.Is<int>(t => t == treeId),
                    It.Is<bool>(t => t == true)), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            
            result.Should().HaveCount(1);
            result.First().Should().BeEquivalentTo(expectedResult.Data[0]);
        }

        [Fact]
        public async Task ShouldReturnStream_OnGetPdfReport()
        {
            // Arrange
            var cmd = new GetReportCommand()
            {
                LabelNumbers = new List<int>() { 1000000001, 1000000002 },
                SubscriptionId = 2, TreeId = 5
            };
            var expectedResult = new MemoryStream();
            
            this.apiBrokerMock.Setup(b =>
                    b.GetPdfReport(cmd,default))
                .ReturnsAsync(expectedResult);

            // Act
            var sut = this.lucaService.GetPdfReport(cmd);
            var result = await sut;
            
            // Assert
            this.apiBrokerMock.Verify(broker =>
                broker.GetPdfReport(cmd,default), Times.Once);
            
            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
            result.Should().BeSameAs(expectedResult);
        }
    }
}