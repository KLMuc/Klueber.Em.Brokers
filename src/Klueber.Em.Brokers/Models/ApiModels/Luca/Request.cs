using System;
using Klueber.Em.Brokers.Models.Shared;

namespace Klueber.Em.Brokers.Models.ApiModels.Luca
{
    public class Request
    {
        public int LabelNumber { get; set; }
        public LaboratoryService Service { get; set; }
        public int LaboratoryId { get; set; }
        public string LaboratoryAddress { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerKlx { get; set; }
        public bool IsFcsProduct { get; set; }
        public string ArticleNumber { get; set; }
        public string ArticleName { get; set; }
        public string ArticleManufacturer { get; set; }
        public string Charge { get; set; }
        public DateTimeOffset SamplingDate { get; set; }
        public bool IsFresh { get; set; }
        public Decimal? OperatingHours { get; set; }
        public Decimal? OperatingTemperatureInCelsius { get; set; }
        public DateTime? LastProductChange { get; set; }
        public Decimal? FillingQuantityInMilliliter { get; set; }
        public string SpecialInfluences { get; set; }
        public TechnicalElement TechnicalElement { get; set; }
        public string LubricationPoint { get; set; }
        public string TreeName { get; set; }
        public int? TreeId { get; set; }
        public bool IsSubscription { get; set; }
        public RequestLanguage Language { get; set; }
        public string SalesComment { get; set; }
        public bool HasLaboratoryValues { get; set; }
        public Recommendation? Recommendation { get; set; }
        public DateTimeOffset? PlannedDeliveryDate { get; set; }
        public ColorIndicator RecommendationColorIndicator => GetColorIndicator();
        private ColorIndicator GetColorIndicator()
        {
            return this.Recommendation switch
            {
                Luca.Recommendation.Normal => ColorIndicator.Green,
                Luca.Recommendation.Change => ColorIndicator.Red,
                Luca.Recommendation.Deviation => ColorIndicator.Yellow,
                _ => ColorIndicator.Gray
            };
        }
    }
}
