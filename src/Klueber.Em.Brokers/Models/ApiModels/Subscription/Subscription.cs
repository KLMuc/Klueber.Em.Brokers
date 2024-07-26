namespace Klueber.Em.Brokers.Models.ApiModels.Subscription
{
    public class Subscription
    {
        public int Id { get; set; }
        public int TreeId { get; set; }
        public string Name { get; set; }
        public string Klx { get; set; }
        public string Customernumber { get; set; }
    }
}
