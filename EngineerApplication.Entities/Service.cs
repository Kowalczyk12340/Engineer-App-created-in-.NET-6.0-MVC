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
    public int FrequencyId { get; set; }

    [ForeignKey("FrequencyId")]
    public Frequency? Frequency { get; set; }

    [Required]
    public int DeliveryId { get; set; }

    [ForeignKey("DeliveryId")]
    public Delivery? Delivery { get; set; }

    [Required]
    public int SupplierId { get; set; }

    [ForeignKey("SupplierId")]
    public Supplier? Supplier { get; set; }
  }
}
