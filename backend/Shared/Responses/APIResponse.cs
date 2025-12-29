using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Responses
{
    public sealed class APIResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public string? TraceId { get; set; }

        public static APIResponse Ok(object data, string? traceId, string message = "Success")
            => new() { Success = true, Data = data, Message = message, TraceId = traceId };

        public static APIResponse Fail(string message, string? traceId)
            => new() { Success = false, Message = message, TraceId = traceId };
    }
}
