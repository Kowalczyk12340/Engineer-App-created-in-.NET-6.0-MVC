using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerApplication.Entities
{
  public class Payment
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Payment Name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Payment Code")]
    public string? Code { get; set; }
  }
}
