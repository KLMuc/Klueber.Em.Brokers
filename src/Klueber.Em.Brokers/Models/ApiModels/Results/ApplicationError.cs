using System.Diagnostics.CodeAnalysis;

namespace Klueber.Em.Brokers.Models.ApiModels.Results
{
    [ExcludeFromCodeCoverage]
    public class ApplicationError
    {
        public string ErrorCode { get; set; } = default!;
        public string ErrorMessage { get; set; } = default!;
    }
}