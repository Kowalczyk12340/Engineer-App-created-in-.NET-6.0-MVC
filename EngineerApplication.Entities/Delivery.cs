using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class Delivery
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Delivery Name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Delivery Description")]
    public string? DeliveryDesc { get; set; }
  }
}
