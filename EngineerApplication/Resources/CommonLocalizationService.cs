using System.Reflection;
using Microsoft.Extensions.Localization;

namespace EngineerApplication.Resources
{
  public class CommonLocalizationService
  {
    private readonly IStringLocalizer localizer;
    public CommonLocalizationService(IStringLocalizerFactory factory)
    {
      var assemblyName = new AssemblyName(typeof(Resources).GetTypeInfo().Assembly.FullName);
      localizer = factory.Create(nameof(Resources), assemblyName.Name);
    }

    public string Get(string key)
    {
      return localizer[key];
    }
  }
}
