using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")]
public class Photo
{
    public long Id { get; set; }
    
    public string Url { get; set; }
    
    public bool IsMain { get; set; }
    
    public string PublicId { get; set; }
    
    public long AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}