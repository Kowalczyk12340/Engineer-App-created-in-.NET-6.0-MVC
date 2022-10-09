using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using U4.DevOps.Dashboard.UnitTests.UITests.ControllerActions;
using U4.DevOps.Dashboard.UnitTests.UITests.Helpers;
using SeleniumExtras.WaitHelpers;
using U4.DevOps.Dashboard.SmokeTests.UITests.ControllerActions;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;

namespace U4.DevOps.Dashboard.UnitTests.UITests
{
  public class FunctionalityTests
  {
    private static string stampName = "SmokeTestStamp";
    private static string serviceName = "SmokeTestService";
    private static string engineeringDirector = "Marcin Kowalczyk";

    public TestContext TestContext { get; set; }

    [SetUp]
    public void PrepareSmokeTests()
    {
      try
      {
        new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
        using IWebDriver driver = new ChromeDriver();
        LoginHelper.LoginToApplication(driver, TestContext);
        ServiceControllerActions.DeleteService(driver, serviceName);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error occured: {ex.Message}");
      }
    }

    [Test]
    public void EngineeringAreasOperationTests()
    {
      var ChromeService = ChromeDriverService.CreateDefaultService();

      using IWebDriver driver = new ChromeDriver(ChromeService, new ChromeOptions { }, TimeSpan.FromSeconds(300));
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(45));
      LoginHelper.LoginToApplication(driver, TestContext);

      EngineeringAreaControllerAction.CreateNewEngineeringArea(driver);
      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshDataButton")));
      Assert.IsFalse(driver.PageSource.Contains(serviceName), $"Service {serviceName} should not be visible on the dashboard after being deleted.");

      EngineeringAreaControllerAction.DeleteEngineeringArea(driver, engineeringDirector);

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshDataButton")));

      Assert.IsFalse(driver.PageSource.Contains(serviceName), $"Service {serviceName} should not be visible on the dashboard after being deleted.");
    }

    [Test]
    public void ServiceOperationTests()
    {
      var ChromeService = ChromeDriverService.CreateDefaultService();

      using IWebDriver driver = new ChromeDriver(ChromeService, new ChromeOptions { }, TimeSpan.FromSeconds(300));
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(55));
      LoginHelper.LoginToApplication(driver, TestContext);

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshDataButton")));

      Assert.IsFalse(driver.PageSource.Contains(serviceName), $"Service {serviceName} should not exists at the beginning of the test.");

      ServiceControllerActions.CreateNewService(driver, serviceName);

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshDataButton")));

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshDataButton")));

      Assert.IsTrue(driver.PageSource.Contains(serviceName), $"Service {serviceName} should be visible on the dashboard after being created.");

      Assert.IsTrue(driver.PageSource.Contains(stampName), $"Stamp {stampName} should be visible on the dashboard after being created.");

      ServiceControllerActions.EditServiceCriteria(driver, serviceName);

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshDataButton")));

      Assert.IsTrue(driver.PageSource.Contains(stampName), $"Stamp {stampName} should be visible on the dashboard after being created.");

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshDataButton")));

      Assert.IsFalse(driver.PageSource.Contains(stampName), $"Stamp {stampName} should not be visible on the dashboard after being deleted.");

      ServiceControllerActions.DeleteService(driver, serviceName);

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("refreshDataButton")));

      Assert.IsFalse(driver.PageSource.Contains(serviceName), $"Service {serviceName} should not be visible on the dashboard after being deleted.");
    }
  }
}