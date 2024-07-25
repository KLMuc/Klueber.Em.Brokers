using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Klueber.Em.Brokers.Models.ApiModels.Results
{
    [ExcludeFromCodeCoverage]
    public class GenericOperationResult<T> : GenericOperationResult where T : class
    {
        public GenericOperationResult() : base() { }
        public GenericOperationResult(bool isSuccess) : base(isSuccess) { }
        public T Data { get; set; } = default!;
    }

    [ExcludeFromCodeCoverage]
    public class GenericOperationResult
    {
        public GenericOperationResult()
        {
            IsSuccess = false;
        }
        public GenericOperationResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        public bool IsSuccess { get; set; }
        public IEnumerable<ApplicationError> Errors { get; set; } = default!;
    }
}
