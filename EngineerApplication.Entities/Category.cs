using System.ComponentModel.DataAnnotations;

namespace EngineerApplication.Entities
{
  public class Category
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nazwa kategorii")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Kolejność wyświetlania")]
    public int DisplayOrder { get; set; }
  }
}