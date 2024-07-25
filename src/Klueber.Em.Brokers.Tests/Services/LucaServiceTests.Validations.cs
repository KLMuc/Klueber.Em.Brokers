using Klueber.Em.Brokers.Models.ApiModels.Luca;
using Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions;
using Klueber.Em.Brokers.Models.ApiModels.Results;
using Klueber.Em.Brokers.Models.Exceptions;
using Moq;

namespace Klueber.Em.Brokers.Tests.Services
{
    public partial class LucaServiceTests
    {
        [Fact]
        public void ShouldThrowValidationException_OnCanUseThisLabel_IfNumberToLow_AndLogItAsync()
        {
            // Arrange
            var labelNumber = 0;
            var invalidRangeException = new LabelNumberInvalidRangeException();
            var expectedValidationException = new LucaValidationException(invalidRangeException);

            // Act
            this.lucaService.CanUseThisLabelAsync(labelNumber);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.Verify(broker =>
                    broker.CanUseLabelAsync(It.IsAny<int>()),
                Times.Never);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceException_OnGetRequests_IfApiResultNotSuccess_AndLogItAsync()
        {
            // Arrange
            var treeId = 5;
            var invalidRangeException = new LucaApiFailedMessageResultException("Error");
            var expectedValidationException = new LucaServiceException(invalidRangeException);

            this.apiBrokerMock.Setup(b =>
                    b.GetRequests(treeId, true))
                .ReturnsAsync(new GenericOperationResult<List<Request>>(false)
                {
                    Errors = new List<ApplicationError>
                    {
                        new ApplicationError()
                        {
                            ErrorMessage = "Error"
                        }
                    }
                });

            // Act
            this.lucaService.GetRequests(treeId);

            // Assert
            this.apiBrokerMock.Verify(broker =>
                broker.GetRequests(treeId, true),
                Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfRequestIsNull_AndLogItAsync()
        {
            // arrange
            CreateRequest request = null;
            var expectedException = new CreateRequestNullException();
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(5, 1, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfSubscriptionIdIsLower1_AndLogItAsync()
        {
            // Arrange
            CreateRequest request = new CreateRequest() { };
            var expectedException = new IndexOutOfRangeException("the Subscription id is lower 1 that can't be possible.");
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(5, 0, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfTreeIdIsWrong_AndLogItAsync()
        {
            // Arrange
            CreateRequest request = new CreateRequest() { };
            var expectedException = new IndexOutOfRangeException("Tree element id is lower 1 that can't be possible.");
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(0, 0, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfLabelNumberIsWrong_AndLogItAsync()
        {
            // Arrange
            CreateRequest request = new CreateRequest();
            var expectedException = new LabelNumberInvalidRangeException();
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(5, 1, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfArticleManufacturerIsNull_AndLogItAsync()
        {
            // Arrange
            CreateRequest request = new CreateRequest() { LabelNumber = 1000000001, Service = LaboratoryService.GreaseAnalysis };
            var expectedException = new RequestFieldNullException(nameof(request.Manufacturer));
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(5, 1, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfArticleNameIsNull_AndLogItAsync()
        {
            // Arrange
            CreateRequest request = new CreateRequest() { LabelNumber = 1000000001, Manufacturer = "Klüber", Service = LaboratoryService.GreaseAnalysis };
            var expectedException = new RequestFieldNullException(nameof(request.ProductName));
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(5, 1, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfLubricationPointIsNull_AndLogItAsync()
        {
            // Arrange
            CreateRequest request = new CreateRequest() { LabelNumber = 1000000001, Manufacturer = "Klüber", ProductName = "Hotemp", Service = LaboratoryService.GreaseAnalysis };
            var expectedException = new RequestFieldNullException(nameof(request.LubricationPoint));
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(5, 1, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfOperatingTemperatureWithoutUnit_AndLogItAsync()
        {
            // Arrange
            CreateRequest request = new CreateRequest()
            {
                LabelNumber = 1000000001,
                Manufacturer = "Klüber",
                ProductName = "Hotemp",
                LubricationPoint = "TestPoint",
                SamplingDate = DateTimeOffset.Now,
                Service = LaboratoryService.GreaseAnalysis,
                OperatingTemperatureValue = 5
            };

            var expectedException = new OperatingTemperatureException();
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            var sut = this.lucaService.CreateRequestAsync(5, 1, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreateRequest_IfLabServiceMissing_AndLogItAsync()
        {
            // Arrange
            CreateRequest request = new CreateRequest()
            {
                LabelNumber = 1000000001,
                Manufacturer = "Klüber",
                ProductName = "Hotemp",
                LubricationPoint = "TestPoint",
                SamplingDate = DateTimeOffset.Now,
                OperatingTemperatureValue = 5
            };

            var expectedException = new LabServiceException();
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(5, 1, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        public static IEnumerable<object[]> SamplingOffsets =>
            new List<object[]>
            {
                new object[] { DateTimeOffset.MinValue },
                new object[] { DateTimeOffset.Now.AddDays(1) }
            };

        [Theory]
        [MemberData(nameof(SamplingOffsets))]
        public void ShouldThrowValidationException_OnCreateRequest_IfSamplingDateIsWrong_AndLogItAsync(DateTimeOffset samplingDate)
        {
            // Arrange
            CreateRequest request = new CreateRequest()
            {
                LabelNumber = 1000000001,
                Manufacturer = "Klüber",
                ProductName = "Hotemp",
                LubricationPoint = "TestPoint",
                SamplingDate = samplingDate,
                Service = LaboratoryService.GreaseAnalysis
            };
            var expectedException = new RequestSamplingDateException();
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            this.lucaService.CreateRequestAsync(5, 1, request);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowServiceException_OnCreate_IfApiResultIsFalse_AndLogItAsync()
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
            var expectedResult = new GenericOperationResult<Request>(false)
            {
                Errors = new List<ApplicationError>
                {
                    new ApplicationError()
                    {
                        ErrorMessage = "Error"
                    }
                }
            };
            var expectedInnerException = new LucaApiFailedMessageResultException(
                expectedResult
                    .Errors
                    .FirstOrDefault()?
                    .ErrorMessage);

            var expectedValidationException = new LucaServiceException(expectedInnerException);

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

            // Assert
            this.apiBrokerMock.Verify(broker =>
                broker.CreateRequestAsync(It.Is<int>(t => t == 5),
                    It.IsAny<CreateRequestModel>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.Is(SameExceptionAs(expectedValidationException))), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnCreate_IfApiResultIsFalse_AndLogItAsync()
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

            var expectedInnerException = new HttpResponseBadGatewayException();
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
                .ThrowsAsync(expectedInnerException);

            // Act
            var sut = this.lucaService.CreateRequestAsync(5, 2, request);

            // Assert
            this.apiBrokerMock.Verify(broker =>
                broker.CreateRequestAsync(It.Is<int>(t => t == 5),
                    It.IsAny<CreateRequestModel>()), Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(
                    It.IsAny<LucaServiceException>()), Times.Once);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.apiBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnGetReport_IfNoLabelNumbersAvailable_AndLogItAsync()
        {
            // Arrange
            var command = new GetReportCommand()
            {
                TreeId = 5,
                SubscriptionId = 2,
            };

            var expectedException = new LabelNumberNullException();
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            var sut = this.lucaService.GetPdfReport(command);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowValidationException_OnGetReport_IfOneOfTheLabelNumbersToLow_AndLogItAsync()
        {
            // Arrange
            var command = new GetReportCommand()
            {
                TreeId = 5,
                SubscriptionId = 2,
                LabelNumbers = new List<int>()
                {
                    1000000001,
                    100000000,
                    10000000,
                }
            };

            var expectedException = new LabelNumberInvalidRangeException();
            var expectedValidationException = new LucaValidationException(expectedException);

            // Act
            var sut = this.lucaService.GetPdfReport(command);

            // Assert
            this.loggingBrokerMock.Verify(broker =>
                    broker.LogError(
                        It.Is(SameExceptionAs(expectedValidationException))),
                Times.Once);

            this.apiBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}