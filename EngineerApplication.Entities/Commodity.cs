using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class Commodity
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nazwa towaru")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Cena")]
    public double Price { get; set; }

    [Display(Name = "Opis")]
    public string? LongDesc { get; set; }

    [Display(Name = "Ilość")]
    public int Amount { get; set; }

    [DataType(DataType.ImageUrl)]
    [Display(Name = "Zdjęcie")]
    public string? ImageUrl { get; set; }

    [Required]
    [Display(Name = "Wybierz kategorie")]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
  }
}
