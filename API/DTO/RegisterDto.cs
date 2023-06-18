using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTO;

public class RegisterDto
{
    [Required]
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    
    [Required]
    [JsonPropertyName("password")]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }
}