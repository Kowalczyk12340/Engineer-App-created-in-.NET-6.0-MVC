using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class Offer
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Offer Name")]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Count")]
    public int Count { get; set; }

    [Required]
    [Display(Name = "Offer Description")]
    public string? OfferDesc { get; set; }

    [Required]
    public int CommodityId { get; set; }

    [ForeignKey("CommodityId")]
    public Commodity? Commodity { get; set; }
  }
}
