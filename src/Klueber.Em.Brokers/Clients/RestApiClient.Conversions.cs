using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Klueber.Em.Brokers.Clients
{
    public partial class RestApiClient
    {
        private static async ValueTask<HttpContent> ConvertToHttpContent<T>(
            T content,
            string mediaType,
            bool ignoreDefaultValues,
            Func<T, ValueTask<string>> serializationFunction = null)
        {
            return mediaType switch
            {
                "text/json" => await ConvertToJsonStringContent(
                    content, mediaType, ignoreDefaultValues, serializationFunction),

                "application/octet-stream" => ConvertToStreamContent(content as Stream, mediaType),
                _ => ConvertToStringContent(content, mediaType)
            };
        }

        private static StringContent ConvertToStringContent<T>(T content, string mediaType)
        {
            return new StringContent(
                content: content.ToString(),
                encoding: Encoding.UTF8,
                mediaType);
        }

        private static StreamContent ConvertToStreamContent<T>(T content, string mediaType)
            where T : Stream
        {
            var contentStream = new StreamContent(content);
            contentStream.Headers.ContentType = new MediaTypeHeaderValue(mediaType);

            return contentStream;
        }

        private static async ValueTask<StringContent> ConvertToJsonStringContent<T>(
            T content,
            string mediaType,
            bool ignoreDefaultValues,
            Func<T, ValueTask<string>> serializationFunction = null)
        {
            string serializedRestrictionRequest = serializationFunction == null
                ? ConvertToJsonStringContent<T>(content, ignoreDefaultValues)
                : await serializationFunction(content);

            var contentString =
                new StringContent(
                    content: serializedRestrictionRequest,
                    encoding: Encoding.UTF8,
                    mediaType);

            return contentString;
        }

        private static string ConvertToJsonStringContent<T>(T content, bool ignoreDefaultValues)
        {
            var jsonSerializerOptions = new System.Text.Json.JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                DefaultIgnoreCondition = ignoreDefaultValues ? JsonIgnoreCondition.WhenWritingDefault : JsonIgnoreCondition.Never
            };

            return System.Text.Json.JsonSerializer.Serialize(content, jsonSerializerOptions);
        }
    }
}
