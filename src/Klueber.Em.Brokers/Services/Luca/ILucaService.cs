using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Models.ApiModels.Luca;

namespace Klueber.Em.Brokers.Services.Luca
{
    public interface ILucaService
    {
        ValueTask<bool> CanUseThisLabelAsync(int labelNumber);
        ValueTask<List<Request>> GetRequests(int treeId, bool wholeSubscription = true);
        ValueTask<Request> CreateRequestAsync(int treeId, int subscriptionId, CreateRequest request);
        ValueTask<Stream> GetPdfReport(GetReportCommand command);
    }
}

