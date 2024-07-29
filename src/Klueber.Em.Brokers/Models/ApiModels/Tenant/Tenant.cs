using System.Collections.Generic;

namespace Klueber.Em.Brokers.Models.ApiModels.Tenant
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Subscription.Subscription> Subscriptions { get; set; }
    }
}
