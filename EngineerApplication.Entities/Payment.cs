using System.ComponentModel.DataAnnotations;

namespace EngineerApplication.Entities
{
  public class Payment
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nazwa płatności")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Kod płatności")]
    public string? Code { get; set; }
  }
}
