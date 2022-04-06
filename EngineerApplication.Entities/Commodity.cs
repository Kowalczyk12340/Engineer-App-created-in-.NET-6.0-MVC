using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class Commodity
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Commodity Name")]
    public string? Name { get; set; }

    [Required]
    public double Price { get; set; }

    [Display(Name = "Description")]
    public string? LongDesc { get; set; }

    [Display(Name = "Amount")]
    public int Amount { get; set; } = 1;

    [DataType(DataType.ImageUrl)]
    [Display(Name = "Image")]
    public string? ImageUrl { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
  }
}
