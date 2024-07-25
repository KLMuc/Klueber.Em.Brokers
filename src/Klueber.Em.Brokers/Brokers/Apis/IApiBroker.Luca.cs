using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Luca;
using Klueber.Em.Brokers.Models.ApiModels.Results;

namespace Klueber.Em.Brokers.Brokers.Apis
{
    public partial interface IApiBroker
    {
        ValueTask<GenericOperationResult> CanUseLabelAsync(int number);
        ValueTask<GenericOperationResult<List<Request>>> GetRequests(int treeId, bool wholeSubscription = true);
        ValueTask<GenericOperationResult<Request>> CreateRequestAsync(int treeId, CreateRequestModel content);
        ValueTask<Stream> GetPdfReport(GetReportCommand command, CancellationToken token = default);
    }
}
