namespace EngineerApplication.ContextStructure.Data.Service.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    ICategoryService Category { get; }
    IPaymentService Payment { get; }
    ISupplierService Supplier { get; }
    IDeliveryService Delivery { get; }
    IEmployeeService Employee { get; }
    IServiceService Service { get; }
    ICommodityService Commodity { get; }
    IOrderHeaderService OrderHeader { get; }
    IOrderDetailsService OrderDetails { get; }
    ISP_Call SP_Call { get; }
    IUserService User { get; }
    Task SaveAsync();
  }
}
