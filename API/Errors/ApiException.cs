using System.Text.Json.Serialization;

namespace API.Errors;

public class ApiException
{
    public ApiException(int statusCode, string message, string? details)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }

    [JsonPropertyName("status_code")]
    public int StatusCode { get; private set; }
    
    [JsonPropertyName("message")]
    public string Message { get; private set; }
    
    [JsonPropertyName("details")]
    public string? Details { get; private set; }
}