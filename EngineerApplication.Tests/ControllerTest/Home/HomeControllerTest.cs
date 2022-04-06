using NUnit.Framework;
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
  }
}
