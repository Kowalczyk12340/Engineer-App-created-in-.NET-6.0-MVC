using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class Employee
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Employee Name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Email Address")]
    public string? EmailAddress { get; set; }

    [Required]
    [Display(Name = "Employee Description")]
    public string? EmployeeDesc { get; set; }

    [Required]
    public int ServiceId { get; set; }

    [ForeignKey("ServiceId")]
    public Service? Service { get; set; }
  }
}
