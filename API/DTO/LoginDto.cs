using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTO;

public class LoginDto
{
    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    
    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; }
}