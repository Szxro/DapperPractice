using System.Text.Json;
using System.Text.Json.Serialization;

namespace DapperPractice.MiddleWares
{
    public sealed class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Message { get; set; } = null!;
    }
}
