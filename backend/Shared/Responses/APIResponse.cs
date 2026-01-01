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

        public static APIResponse Ok(string message)
           => new() { Success = true, Message = message };

        public static APIResponse Ok(object? data, string message)
            => new() { Success = true, Data = data, Message = message};

        public static APIResponse Fail(string message)
            => new() { Success = false, Message = message};
    }
}
