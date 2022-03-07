using EngineerApplication.Tests.Controller;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EngineerApplication.Tests.Home
{
  public class HomeControllerTest : BaseControllerTest
  {
    [Test]
    public async Task TestGetMethodForHomePage()
    {
      var response = await Client.GetAsync("/");
      response.EnsureSuccessStatusCode();
      var responseString = await response.Content.ReadAsStringAsync();
      var result = responseString != null && responseString.Contains("Bluzy Sportowe");
      Assert.That(result);
    }

    [Test]
    public async Task TestLoginMethodForHomePage()
    {
      var response = await Client.GetAsync("/Account/Login");
      var responseString = await response.Content.ReadAsStringAsync();
      var result = responseString != null || responseString.ToLower().Contains("email");
      Assert.That(result);
    }

    [Test]
    public async Task TestCartMethodForHomePage()
    {
      var response = await Client.GetAsync("/Cart");
      var responseString = await response.Content.ReadAsStringAsync();
      var result = (responseString != null);
      Assert.That(result);
    }
  }
}
