using EngineerApplication.Entities;
using NUnit.Framework;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.ControllerTest.Home
{
  public class HomeControllerTest : BaseControllerTest
  {
    [Test]
    public async Task TestGetMethodForOfferPage()
    {
      var response = await Client.GetAsync("/");
      response.EnsureSuccessStatusCode();
      var responseString = await response.Content.ReadAsStringAsync();
      var result = responseString != null && responseString.Contains("Home - Dropshipping Application");
      Assert.That(result);
    }

    [Test]
    public async Task TestExportToPdfMethod()
    {
      var homeItem = new Delivery()
      {
        Name = "Dostawa Kurierska Pobraniowa",
      };
      var home = await Client.PostAsJsonAsync("/Customer/Home/export", homeItem);
      Assert.IsNotNull(home.RequestMessage);
    }
  }
}
