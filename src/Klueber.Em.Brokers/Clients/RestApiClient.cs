using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Klueber.Em.Brokers.Clients.Services;

namespace Klueber.Em.Brokers.Clients
{
    public partial class RestApiClient : IRestApiClient
    {
        private readonly HttpClient httpClient;
        private static readonly JsonSerializerOptions DefaultSerializerOptions =
            new JsonSerializerOptions(JsonSerializerDefaults.Web);

        public RestApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async ValueTask<T> GetContentAsync<T>(string relativeUrl, Func<string, ValueTask<T>> deserializationFunction = null)
        {
            HttpResponseMessage responseMessage = await this.httpClient.GetAsync(relativeUrl);
            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return await DeserializeResponseContent<T>(
                responseMessage, deserializationFunction);
        }

        public async ValueTask<T> GetContentAsync<T>(
            string relativeUrl,
            CancellationToken cancellationToken,
            Func<string, ValueTask<T>> deserializationFunction = null)
        {
            HttpResponseMessage responseMessage =
                await this.httpClient.GetAsync(relativeUrl, cancellationToken);
            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return await DeserializeResponseContent<T>(
                responseMessage, deserializationFunction);
        }
     
        public async ValueTask<TResult> PostContentAsync<TContent, TResult>(
            string relativeUrl,
            TContent content,
            string mediaType = "text/json",
            bool ignoreDefaultValues = false,
            Func<TContent, ValueTask<string>> serializationFunction = null,
            Func<string, ValueTask<TResult>> deserializationFunction = null)
        {
            HttpContent contentString =
                await ConvertToHttpContent(
                    content,
                    mediaType,
                    ignoreDefaultValues,
                    serializationFunction);

            HttpResponseMessage responseMessage =
                await this.httpClient.PostAsync(relativeUrl, contentString);

            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return await DeserializeResponseContent<TResult>(
                responseMessage, deserializationFunction);
        }

        public async ValueTask<Stream> PostContentWithStreamResponseAsync<T>(
            string relativeUrl,
            T content,
            CancellationToken cancellationToken,
            string mediaType = "application/octet-stream",
            bool ignoreDefaultValues = false,
            Func<T, ValueTask<string>> serializationFunction = null)
        {
            HttpContent contentString =
                await ConvertToHttpContent(
                    content,
                    mediaType,
                    ignoreDefaultValues,
                    serializationFunction);

            HttpResponseMessage responseMessage =
                await this.httpClient.PostAsync(
                    relativeUrl,
                    contentString,
                    cancellationToken);

            await ValidationService.ValidateHttpResponseAsync(responseMessage);

            return await responseMessage.Content.ReadAsStreamAsync();
        }

        private static async ValueTask<T> DeserializeResponseContent<T>(
            HttpResponseMessage responseMessage,
            Func<string, ValueTask<T>> deserializationFunction = null)
        {
            string responseString =
                await responseMessage.Content.ReadAsStringAsync();

            return deserializationFunction == null
                ? System.Text.Json.JsonSerializer.Deserialize<T>(responseString, DefaultSerializerOptions)
                : await deserializationFunction(responseString);
        }
    }
}

