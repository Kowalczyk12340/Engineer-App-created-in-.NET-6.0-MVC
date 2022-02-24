using System;
using System.Collections.Generic;
using System.Text;

namespace EngineerApplication.Entities.ViewModels
{
  public class OrderViewModel
  {
    public OrderHeader? OrderHeader { get; set; }
    public IEnumerable<OrderDetails>? OrderDetails { get; set; }
  }
}
