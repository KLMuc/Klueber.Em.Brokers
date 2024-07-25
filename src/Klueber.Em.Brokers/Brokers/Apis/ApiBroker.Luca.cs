using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Luca;
using Klueber.Em.Brokers.Models.ApiModels.Results;

namespace Klueber.Em.Brokers.Brokers.Apis
{
    public partial class ApiBroker
    {
        private const string LucaRelativeUrl = "api/v1/Luca";

        public async ValueTask<GenericOperationResult> CanUseLabelAsync(int number) =>
            await this.GetAsync<GenericOperationResult>($"{LucaRelativeUrl}/LabelNumbers/{number}");

        public async ValueTask<GenericOperationResult<List<Request>>> GetRequests(int treeId, bool wholeSubscription = true) =>
            await this.GetAsync<GenericOperationResult<List<Request>>>($"{LucaRelativeUrl}/Requests/Subscription/{treeId}/{wholeSubscription}");

        public async ValueTask<GenericOperationResult<Request>> CreateRequestAsync(
            int treeId, CreateRequestModel content) =>
            await this.PostAsync<CreateRequestModel, GenericOperationResult<Request>>($"{LucaRelativeUrl}/Requests/{treeId}",
                content);

        public async ValueTask<Stream> GetPdfReport(GetReportCommand command, CancellationToken token = default) =>
            await this.PostContentWithStreamResponseAsync($"{LucaRelativeUrl}/Report/{command.TreeId}", command, token);
    }
}
