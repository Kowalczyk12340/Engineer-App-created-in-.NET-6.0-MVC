using EngineerApplication.ContextStructure.Data.Service.Interfaces;

namespace EngineerApplication.ContextStructure.Data.Service
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly EngineerDbContext _db;

    public UnitOfWork(EngineerDbContext db)
    {
      _db = db;
      Category = new CategoryService(_db);
      Payment = new PaymentService(_db);
      Commodity = new CommodityService(_db);
      Service = new ServiceService(_db);
      Supplier = new SupplierService(_db);
      Delivery = new DeliveryService(_db);
      Employee = new EmployeeService(_db);
      OrderHeader = new OrderHeaderService(_db);
      OrderDetails = new OrderDetailsService(_db);
      User = new UserService(_db);
      SP_Call = new SP_Call(_db);
    }

    public ICategoryService Category { get; private set; }
    public IPaymentService Payment { get; private set; }
    public ISupplierService Supplier { get; private set; }
    public IEmployeeService Employee { get; private set; }
    public ICommodityService Commodity { get; private set; }
    public IServiceService Service { get; private set; }
    public IOrderHeaderService OrderHeader { get; private set; }
    public IOrderDetailsService OrderDetails { get; private set; }
    public IUserService User { get; private set; }
    public ISP_Call SP_Call { get; private set; }
    public IDeliveryService Delivery { get; private set; }

    public void Dispose()
    {
      _db.Dispose();
    }

    public void Save()
    {
      _db.SaveChanges();
    }
  }
}
