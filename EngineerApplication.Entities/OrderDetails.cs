using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EngineerApplication.Entities
{
  public class OrderDetails
  {
    public int Id { get; set; }

    [Required]
    public int OrderHeaderId { get; set; }

    [ForeignKey("OrderHeaderId")]
    public OrderHeader? OrderHeader { get; set; }

    [Required]
    public int CommodityId { get; set; }

    [ForeignKey("CommodityId")]
    public Commodity? Commodity { get; set; }

    [Required]
    public string? CommodityName { get; set; }

    [Required]
    public double Price { get; set; }

  }
}
