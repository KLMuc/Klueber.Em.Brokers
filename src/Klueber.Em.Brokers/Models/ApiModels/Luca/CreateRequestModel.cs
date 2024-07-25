using System;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca
{
    public class CreateRequestModel
    {
        public int TreeId { get; set; }
        public int SubscriptionId { get; set; }
        public CreateRequest Data { get; set; }
    }

    public class CreateRequest
    {
        public LaboratoryService Service { get; set; }
        public int LabelNumber { get; set; }
        public bool IsFresh { get; set; }

        public string ArticleNumber { get; set; }
        public string ProductName { get; set; }
        public string Manufacturer { get; set; }
        public string Batch { get; set; }

        public TechnicalElement TechnicalElement { get; set; }
        public string LubricationPoint { get; set; }

        public DateTimeOffset SamplingDate { get; set; }
        public DateTime? LastProductChange { get; set; }
        public RequestLanguage Language { get; set; }
        public string SpecialInfluences { get; set; }
        public bool ShowSpecialInfluences { get; set; }
        public Decimal? OperatingHours { get; set; }
        public decimal? FillingQuantityInLiter { get; set; }

        public decimal? OperatingTemperatureValue { get; set; }
        public TemperatureUnit OperatingTemperatureUnit { get; set; }
    }

    public enum TemperatureUnit
    {
        Celsius = 1,
        Fahrenheit = 2
    }
}
