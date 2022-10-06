using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;

namespace U4.DevOps.Dashboard.UnitTests.UITests.ControllerActions
{
  internal static class StampControllerActions
  {
    private static string serviceName = "SmokeTestService";

    internal static void CreateNewStamp(IWebDriver driver, string stampName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("topbar__toolbar")));
      driver.FindElement(By.Id("topbar__toolbar")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("manageStamps")));
      driver.FindElement(By.Id("manageStamps")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-stamp-button")));

      driver.FindElement(By.CssSelector("[aria-label='Select service']")).SendKeys(serviceName);
      SelectClick(driver, "select-service");

      driver.FindElement(By.Id("add-stamp-button")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("displayName")));
      driver.FindElement(By.Id("displayName")).SendKeys(stampName);

      wait.Until(drv => drv.TryFindElement(By.Id("serviceId")).Text.Contains("SmokeTestService"));
      if (!driver.FindElement(By.Id("serviceId")).Text.Contains("SmokeTestService"))
      {
        var element = driver.FindElement(By.Id("serviceId"));
        element.Click();
        wait.Until(element => element.TryFindElement(By.Id("serviceIdSelect-List")));

        var item = element.FindElement(By.Id("serviceIdSelect-List"));

        wait.Until(element => element.TryFindElement(By.CssSelector("[id*='option']")));
        item.FindElements(By.CssSelector("[id*='option']")).FirstOrDefault(x => x.Text.Contains("SmokeTestService")).Click();
      }

      driver.FindElement(By.Id("technicalName")).SendKeys("TechnicalNameTest");
      driver.FindElement(By.Id("azureResourceName")).SendKeys("AzureTest");
      driver.FindElement(By.Id("stampUri")).SendKeys("https://www.google.com");

      driver.FindElement(By.Id("region")).Click();
      SelectClick(driver, "region");

      driver.FindElement(By.Id("environmentType")).Click();
      SelectClick(driver, "environmentType");

      driver.FindElement(By.Id("healthCheckUri")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("blueGreenHealthCheckUri")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("dashboardUri")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("discoverySourceSystem")).SendKeys("3");

      driver.FindElement(By.CssSelector("[for='isDisabled']")).Click();
      driver.FindElement(By.CssSelector("[for='isDisabled']")).Click();

      driver.FindElement(By.CssSelector("[class='jodit-wysiwyg']")).SendKeys("Description");

      driver.FindElement(By.Id("add-new-tenant")).Click();
      driver.FindElement(By.Id("tenants[0].name")).SendKeys("TenantTestName");
      driver.FindElement(By.Id("tenants[0].uri")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("tenants[0].description")).SendKeys("TenantTestDescription");

      driver.FindElement(By.Id("saveStamp")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("add-stamp-button")));
      driver.FindElement(By.Id("goToDashboard")).Click();
    }

    internal static bool CheckIfStampExist(IWebDriver driver, string stampName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("topbar__toolbar")));
      driver.FindElement(By.Id("topbar__toolbar")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("manageStamps")));
      driver.FindElement(By.Id("manageStamps")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-stamp-button")));

      driver.FindElement(By.CssSelector("[aria-label='Select service']")).SendKeys(serviceName);
      SelectClick(driver, "select-service");

      var elements = driver.FindElements(By.CssSelector("[role='cell']"));
      var item = elements.FirstOrDefault(x => x.Text.Contains(stampName));

      if (item != null)
      {
        wait.Until(drv => drv.TryFindElement(By.Id("goToDashboard")));
        driver.FindElement(By.Id("goToDashboard")).Click();
        return true;
      }
      return false;
    }

    internal static void DeleteStamp(IWebDriver driver, string stampName)
    {

      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("topbar__toolbar")));
      driver.FindElement(By.Id("topbar__toolbar")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("manageStamps")));
      driver.FindElement(By.Id("manageStamps")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-stamp-button")));

      driver.FindElement(By.CssSelector("[aria-label='Select service']")).SendKeys(serviceName);

      Thread.Sleep(1000);

      SelectClick(driver, "select-service");

      wait.Until(drv => drv.TryFindElement(By.CssSelector("tbody .table__row")));

      var element = driver.FindElements(By.ClassName("table__row")).First(x => x.Text.Contains(stampName));
      element.FindElement(By.Id("deleteStamp")).Click();
      driver.FindElement(By.Id("deleteConfirm")).Click();

      Thread.Sleep(1000);

      wait.Until(drv => drv.ElementDoesNotExists(By.Id("deleteConfirm")));
      wait.Until(drv => drv.TryFindElement(By.Id("goToDashboard")));
      driver.FindElement(By.Id("goToDashboard")).Click();
    }

    private static void SelectClick(IWebDriver driver, string selectName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));

      var element = driver.FindElement(By.Id(selectName));
      wait.Until(element => element.TryFindElement(By.Id(selectName + "Select-List")));

      var item = element.FindElement(By.Id(selectName + "Select-List"));

      wait.Until(element => element.TryFindElement(By.CssSelector("[id*='option-0']")));
      item.FindElement(By.CssSelector("[id*='option-0']")).Click();
    }
  }
}
