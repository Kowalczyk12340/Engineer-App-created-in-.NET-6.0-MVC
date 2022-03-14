using System.Globalization;

namespace EngineerApplication.Resources
{
  public class CultureSwitcherModel
  {
    public CultureInfo? CurrentUICulture { get; set; }
    public List<CultureInfo>? SupportedCultures { get; set; }
  }
}
