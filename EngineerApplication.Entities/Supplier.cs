using System.ComponentModel.DataAnnotations;

namespace EngineerApplication.Entities
{
  public class Supplier
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Nazwa dostawcy")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Miejscowość")]
    public string? City { get; set; }

    [Display(Name = "Ulica")]
    public string? Street { get; set; }

    [Display(Name = "Kod pocztowy")]
    public string? PostalCode { get; set; }

    [Required]
    [Display(Name = "Numer telefonu")]
    public string? PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Adres Email")]
    public string? EmailAddress { get; set; }
  }
}
