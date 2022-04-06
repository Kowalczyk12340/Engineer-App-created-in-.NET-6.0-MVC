using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class Service
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Service Name")]
    public string? Name { get; set; }

    [Required]
    public double Price { get; set; }

    [Display(Name = "Description")]
    public string? LongDesc { get; set; }

    [DataType(DataType.ImageUrl)]
    [Display(Name = "Image")]
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
