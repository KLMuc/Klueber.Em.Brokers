using System;
using System.Collections.Generic;
using System.Linq;
using Klueber.Em.Brokers.Models.ApiModels.Luca;
using Klueber.Em.Brokers.Models.ApiModels.Luca.Exceptions;
using Klueber.Em.Brokers.Models.ApiModels.Results;

namespace Klueber.Em.Brokers.Services.Luca
{
    public partial class LucaService
    {
        private static void ValidateCreateRequest(CreateRequestModel request)
        {
            if (request.Data == null)
            {
                throw new CreateRequestNullException();
            }

            ValidateTreeId(request.TreeId);
            ValidateSubscriptionId(request.SubscriptionId);
            ValidateLucaLabelNumber(request.Data.LabelNumber);
            ValidateService(request.Data.Service);
            ValidateStrings(request.Data.Manufacturer, nameof(request.Data.Manufacturer));
            ValidateStrings(request.Data.ProductName, nameof(request.Data.ProductName));
            ValidateStrings(request.Data.LubricationPoint, nameof(request.Data.LubricationPoint));
            ValidateSamplingDate(request.Data.SamplingDate);
            ValidateOperatingTemperature(request.Data.OperatingTemperatureValue, request.Data.OperatingTemperatureUnit);
        }
        
        private void ValidateGetReportCommand(GetReportCommand command)
        {
            ValidateTreeId(command.TreeId);
            ValidateSubscriptionId(command.SubscriptionId);
            if (command.LabelNumbers == null || command.LabelNumbers.Count == 0)
            {
                throw new LabelNumberNullException();
            }

            foreach (var number in command.LabelNumbers)
            {
                ValidateLucaLabelNumberGet(number);
            }
        }

        private static void ValidateLucaLabelNumberGet(int labelNumber)
        {
            if (labelNumber < 100000000)
            {
                throw new LabelNumberInvalidRangeException();
            }
        }

        private static void ValidateLucaLabelNumber(int labelNumber)
        {
            if (labelNumber < 1000000000)
            {
                throw new LabelNumberInvalidRangeException();
            }
        }

        private static void ValidateTreeId(int treeId)
        {
            if (treeId < 1)
            {
                throw new IndexOutOfRangeException("Tree element id is lower 1 that can't be possible.");
            }
        }

        private static void ValidateSubscriptionId(int subscriptionId)
        {
            if (subscriptionId < 1)
            {
                throw new IndexOutOfRangeException("the Subscription id is lower 1 that can't be possible.");
            }
        }

        private static void ValidateGetRequestApiResult(GenericOperationResult<List<Request>> apiResult)
        {
            if (apiResult.IsSuccess == false)
            {
                throw new LucaApiFailedMessageResultException(apiResult.Errors.FirstOrDefault()?.ErrorMessage);
            }
        }

        private static void ValidateApiResult(GenericOperationResult apiResult)
        {
            if (apiResult.IsSuccess == false)
            {
                throw new LucaApiFailedMessageResultException(apiResult.Errors.FirstOrDefault()?.ErrorMessage);
            }
        }

        private static void ValidateStrings(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new RequestFieldNullException(fieldName);
        }

        private static void ValidateOperatingTemperature(decimal? value, TemperatureUnit unit)
        {
            if (value is not null && !Enum.IsDefined(typeof(TemperatureUnit), unit))
            {
                throw new OperatingTemperatureException();
            }
        }

        private static void ValidateService(LaboratoryService service)
        {
            if (!Enum.IsDefined(typeof(LaboratoryService), service)) throw new LabServiceException();
        }

        private static void ValidateSamplingDate(DateTimeOffset dataSamplingDate)
        {
            if (dataSamplingDate == DateTimeOffset.MinValue || dataSamplingDate > DateTimeOffset.Now)
            {
                throw new RequestSamplingDateException();
            }
        }
    }
}
