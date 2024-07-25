using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Klueber.Em.Brokers.Clients
{
    public interface IRestApiClient
    {
        ValueTask<T> GetContentAsync<T>(
            string relativeUrl,
            Func<string, ValueTask<T>> deserializationFunction = null);

        ValueTask<T> GetContentAsync<T>(
            string relativeUrl,
            CancellationToken cancellationToken,
            Func<string, ValueTask<T>> deserializationFunction = null);

        ValueTask<TResult> PostContentAsync<TContent, TResult>(
             string relativeUrl,
             TContent content,
             string mediaType = "text/json",
             bool ignoreDefaultValues = false,
             Func<TContent, ValueTask<string>> serializationFunction = null,
             Func<string, ValueTask<TResult>> deserializationFunction = null);

        ValueTask<Stream> PostContentWithStreamResponseAsync<T>(
            string relativeUrl,
            T content,
            CancellationToken cancellationToken,
            string mediaType = "text/json",
            bool ignoreDefaultValues = false,
            Func<T, ValueTask<string>> serializationFunction = null);
    }
}
