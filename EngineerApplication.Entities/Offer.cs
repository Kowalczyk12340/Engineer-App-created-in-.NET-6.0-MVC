using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerApplication.Entities
{
  public class Offer
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Offer Name")]
    public string? Name { get; set; }

    public List<Commodity>? Commodities { get; set; }
  }
}
