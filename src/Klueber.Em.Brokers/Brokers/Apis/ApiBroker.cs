using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Clients;

namespace Klueber.Em.Brokers.Brokers.Apis
{
    [ExcludeFromCodeCoverage]
    public partial class ApiBroker : IApiBroker
    {
        private readonly HttpClient httpClient;
        private readonly IRestApiClient apiClient;

        public ApiBroker(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.apiClient = GetApiClient();
        }

        private async ValueTask<T> GetAsync<T>(string relativeUrl) =>
            await this.apiClient.GetContentAsync<T>(relativeUrl);

        private async ValueTask<TResult> PostAsync<TContent, TResult>(string relativeUrl, TContent content) =>
            await apiClient.PostContentAsync<TContent, TResult>(relativeUrl, content);

        private async ValueTask<Stream> PostContentWithStreamResponseAsync<T>(
            string relativeUrl,
            T content,
            CancellationToken cancellationToken,
            string mediaType = "text/json") =>
            await apiClient.PostContentWithStreamResponseAsync<T>(relativeUrl, content, cancellationToken, mediaType);

        private IRestApiClient GetApiClient()
        {
            return new RestApiClient(this.httpClient);
        }
    }
}
