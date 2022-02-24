using System.ComponentModel.DataAnnotations;

namespace EngineerApplication.Entities
{
  public class WebImages
  {
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public byte[]? Picture { get; set; }
  }
}