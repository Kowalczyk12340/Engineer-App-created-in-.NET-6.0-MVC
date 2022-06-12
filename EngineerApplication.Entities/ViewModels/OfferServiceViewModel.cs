namespace EngineerApplication.Entities.ViewModels
{
  public class OfferServiceViewModel
  {
    public IEnumerable<Category>? CategoryList { get; set; }
    public IEnumerable<Service>? ServiceList { get; set; }
    public IEnumerable<Employee>? EmployeeList { get; set; }
    public IQueryable<Service>? Services { get; set; }
    public string? SearchBox { get; set; }
  }
}
