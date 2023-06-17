using System.Text.Json.Serialization;

namespace API.DTO;

public class UserDto
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    
    [JsonPropertyName("token")]
    public string Token { get; set; }
}