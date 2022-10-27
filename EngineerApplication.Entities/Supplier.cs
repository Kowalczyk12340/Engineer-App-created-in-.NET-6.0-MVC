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
    [DataType(DataType.PostalCode)]
    public string? PostalCode { get; set; }

    [Required]
    [Display(Name = "Numer telefonu")]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Adres Email")]
    [DataType(DataType.EmailAddress)]
    public string? EmailAddress { get; set; }
  }
}
