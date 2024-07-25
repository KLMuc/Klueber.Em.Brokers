using System.Net;
using System.Text;
using Tynamix.ObjectFiller;

namespace Klueber.Em.Brokers.Tests.Clients.Services
{
    public partial class ValidationServiceTests
    {
        private HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode statusCode, string content)
        {
            return new HttpResponseMessage(statusCode)
            {
                Content = CreateStreamContent(content)
            };
        }

        private string GetRandomContent() => new MnemonicString().GetValue();

        private StreamContent CreateStreamContent(string content)
        {
            byte[] contentBytes = Encoding.ASCII.GetBytes(content);
            var stream = new MemoryStream(contentBytes);

            return new StreamContent(stream);
        }
    }
}
