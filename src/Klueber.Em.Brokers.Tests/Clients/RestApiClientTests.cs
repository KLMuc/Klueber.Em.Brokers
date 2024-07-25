using Klueber.Em.Brokers.Clients;
using Klueber.Em.Brokers.Tests.Models;
using Tynamix.ObjectFiller;
using WireMock.Server;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Klueber.Em.Brokers.Tests.Clients
{
    public partial class RestApiClientTests : IDisposable
    {
        private readonly IRestApiClient restApiClient;
        //private readonly HttpClient httpClient;
        private readonly WireMockServer wiremockServer;
        private const string relativeUrl = "/tests";

        public RestApiClientTests()
        {
            wiremockServer = WireMockServer.Start();
            var httpClient = new HttpClient() { BaseAddress = new Uri(wiremockServer.Urls.First()) };
            this.restApiClient = new RestApiClient(httpClient);
        }

        private static TEntity GetRandomTEntity() =>
            CreateTEntityFiller(GetTestDateTimeOffset()).Create();

        private static DateTimeOffset GetTestDateTimeOffset() =>
            new DateTimeOffset(DateTime.Now);

        private static Filler<TEntity> CreateTEntityFiller(DateTimeOffset dates)
        {
            var filler = new Filler<TEntity>();
            filler.Setup()
                .OnType<DateTimeOffset>().Use(dates);

            return filler;
        }

        private static Func<string, ValueTask<TEntity>> ContentDeserializationFunction
        {
            get
            {
                return async (entityContent) =>
                    await Task.FromResult(JsonSerializer.Deserialize<TEntity>(
                        entityContent));
            }
        }
        
        private static string CreateRandomContent()
        {
            var randomContent = new Lipsum(
                LipsumFlavor.LoremIpsum,
                minWords: 3,
                maxWords: 10);

            return randomContent.ToString();
        }

        private async Task<string> ReadStreamToEndAsync(Stream result)
        {
            var reader = new StreamReader(result, leaveOpen: false);

            return await reader.ReadToEndAsync();
        }
        
        private static ValueTask<string> SerializationContentFunction<TEntity>(TEntity entityContent)
        {
            string jsonContent = JsonSerializer.Serialize(entityContent);

            return new ValueTask<string>(jsonContent);
        }

        public void Dispose() => this.wiremockServer.Stop();
    }
}
