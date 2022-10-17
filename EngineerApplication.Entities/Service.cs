using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class Service
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nazwa usługi")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Cena")]
    public double Price { get; set; }

    [Display(Name = "Opis usługi")]
    public string? LongDesc { get; set; }

    [DataType(DataType.ImageUrl)]
    [Display(Name = "Zdjęcie")]
    public string? ImageUrl { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }

    [Required]
    public int PaymentId { get; set; }

    [ForeignKey("PaymentId")]
    public Payment? Payment { get; set; }
  }
}
