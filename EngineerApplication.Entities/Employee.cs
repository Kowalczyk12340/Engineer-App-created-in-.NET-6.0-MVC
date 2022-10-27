using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class Employee
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Imię i nazwisko")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Numer telefonu")]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Adres email")]
    [DataType(DataType.EmailAddress)]
    public string? EmailAddress { get; set; }

    [Required]
    [Display(Name = "Opis pracownika")]
    public string? EmployeeDesc { get; set; }

    [Required]
    public int ServiceId { get; set; }

    [ForeignKey("ServiceId")]
    public Service? Service { get; set; }
  }
}
