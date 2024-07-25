using System.Collections.Generic;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca
{
    public class GetReportCommand
    {
        public int TreeId { get; set; }
        public int SubscriptionId { get; set; }
        public List<int> LabelNumbers { get; set; }
    }
}
