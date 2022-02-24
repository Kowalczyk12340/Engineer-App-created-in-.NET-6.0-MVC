using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EngineerApplication.Entities
{

  public class Frequency
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public int FrequencyCount { get; set; }

    [Required]
    [Display(Name = "Frequency Name")]
    public string? Name { get; set; }
  }
}
