using EngineerApplication.Areas.Admin.Controllers;
using EngineerApplication.ContextStructure.Data.Service.Interfaces;
using EngineerApplication.Entities;
using EngineerApplication.Tests.Controller;
using Moq;
using NUnit.Framework;
using System.Net.Http.Json;
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
      var response = await Client.GetAsync("/Admin/Login");
      //response.EnsureSuccessStatusCode();
      var responseString = await response.Content.ReadAsStringAsync();
      var result = responseString != null && responseString.ToLower().Contains("email");
      Assert.That(result);
    }
  }
}
