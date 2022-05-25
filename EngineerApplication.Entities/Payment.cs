using System.ComponentModel.DataAnnotations;

namespace EngineerApplication.Entities
{
  public class Payment
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Payment Name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Payment Code")]
    public string? Code { get; set; }
  }
}
