using System.Linq.Expressions;
using Klueber.Em.Brokers.Brokers.Apis;
using Klueber.Em.Brokers.Brokers.Loggings;
using Klueber.Em.Brokers.Models.ApiModels.Luca;
using Klueber.Em.Brokers.Services.Luca;
using Moq;
using Tynamix.ObjectFiller;

namespace Klueber.Em.Brokers.Tests.Services
{
    public partial class LucaServiceTests
    {
        private readonly Mock<IApiBroker> apiBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ILucaService lucaService;

        public LucaServiceTests()
        {
            this.apiBrokerMock = new Mock<IApiBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();

            this.lucaService = new LucaService(
                apiBrokerMock.Object,
                loggingBrokerMock.Object);
        }

        private static Expression<Func<Exception, bool>> SameExceptionAs(Exception expectedException)
        {
            return actualException => actualException.Message == expectedException.Message
                                      && actualException.InnerException.Message == expectedException.InnerException.Message
                                      ;
        }

        private Request CreateRandomLucaRequest(int labelNumber,
            LaboratoryService service,
            string manufacturer,
            string productName,
            string lubricationPoint,
            DateTimeOffset samplingDate)
        {
            return new Request
            {
                LabelNumber = labelNumber,
                Service = service,
                ArticleManufacturer = manufacturer,
                ArticleName = productName,
                ArticleNumber = new MnemonicString().GetValue(),
                LubricationPoint = lubricationPoint,
                SamplingDate = samplingDate,
                LaboratoryId = 1001,
                LaboratoryAddress = $"{new StreetName(City.London)}\r {new CityName()}",
                CustomerNumber = new MnemonicString().GetValue(),
                CustomerKlx = new MnemonicString().GetValue(),
                IsFcsProduct = true,
                Charge = null,
                IsFresh = true,
                OperatingHours = 100,
                OperatingTemperatureInCelsius = 100,
                LastProductChange = DateTime.Now,
                FillingQuantityInMilliliter = 100,
                SpecialInfluences = new MnemonicString().GetValue(),
                TechnicalElement = TechnicalElement.BearingPlain,
                TreeName = new MnemonicString().GetValue(),
                TreeId = 5,
                IsSubscription = true,
                Language = RequestLanguage.English,
                SalesComment = new MnemonicString().GetValue(),
                HasLaboratoryValues = true,
                Recommendation = Recommendation.Normal,
            };
        }
    }
}
