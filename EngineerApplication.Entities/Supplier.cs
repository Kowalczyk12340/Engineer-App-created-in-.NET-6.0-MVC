using System.ComponentModel.DataAnnotations;

namespace EngineerApplication.Entities
{
  public class Supplier
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Supplier Name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "City")]
    public string? City { get; set; }

    [Display(Name = "Street")]
    public string? Street { get; set; }

    [Display(Name = "Postal Code")]
    public string? PostalCode { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Email Address")]
    public string? EmailAddress { get; set; }
  }
}
