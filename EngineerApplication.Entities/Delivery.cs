using System.ComponentModel.DataAnnotations;

namespace EngineerApplication.Entities
{
  public class Delivery
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nazwa dostawy")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Opis dostawy")]
    public string? DeliveryDesc { get; set; }
  }
}
