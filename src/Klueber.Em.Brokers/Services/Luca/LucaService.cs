using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Brokers.Apis;
using Klueber.Em.Brokers.Brokers.Loggings;
using Klueber.Em.Brokers.Models.ApiModels.Luca;
using Klueber.Em.Brokers.Models.ApiModels.Results;

namespace Klueber.Em.Brokers.Services.Luca
{
    public partial class LucaService : ILucaService
    {
        private readonly IApiBroker apiBroker;
        private readonly ILoggingBroker loggingBroker;

        public LucaService(IApiBroker apiBroker, ILoggingBroker loggingBroker)
        {
            this.apiBroker = apiBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<bool> CanUseThisLabelAsync(int labelNumber) =>
            await TryCatch(async () =>
            {
                ValidateLucaLabelNumber(labelNumber);
                var apiResult = await this.apiBroker.CanUseLabelAsync(labelNumber);
                return apiResult.IsSuccess;
            });

        public async ValueTask<List<Request>> GetRequests(int treeId, bool wholeSubscription = true) =>
            await TryCatch(async () =>
            {
                ValidateTreeId(treeId);
                var apiResult = await this.apiBroker.GetRequests(treeId, wholeSubscription);
                ValidateGetRequestApiResult(apiResult);
                return apiResult.Data;
            });

        public ValueTask<Request> CreateRequestAsync(int treeId, int subscriptionId, CreateRequest request) =>
        TryCatch(async () =>
        {
            var createCommand = new CreateRequestModel()
            {
                SubscriptionId = subscriptionId,
                TreeId = treeId,
                Data = request
            };
            ValidateCreateRequest(createCommand);
            GenericOperationResult<Request> apiResult = await this.apiBroker.CreateRequestAsync(treeId, createCommand);
            ValidateApiResult(apiResult);
            return apiResult.Data;
        });

        public ValueTask<Stream> GetPdfReport(GetReportCommand command) =>
        TryCatch(async () =>
        {
            ValidateGetReportCommand(command);
            var apiResult = await this.apiBroker.GetPdfReport(command);
            return apiResult;
        });
    }
}