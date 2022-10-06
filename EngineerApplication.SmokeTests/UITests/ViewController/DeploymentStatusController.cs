using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;

namespace U4.DevOps.Dashboard.UnitTests.UITests.ViewController
{
  internal static class DeploymentStatusController
  {
    private static string ServiceName = "Invoice Recognition";
    private static string ServiceId = "44";
    private static string GroupName = "ERPx Apps";
    private static string StampName = "Development (nightly)";
    private static string StampId = "353";

    internal static void RunDeploymentStatusTabTests(IWebDriver driver)
    {
      CheckExpandCollapseButton(driver);
      StampModal(driver, StampId, StampName);
      Search(driver, ServiceName);
      QuarterFilter(driver);
      GroupBy(driver);
      ServiceModal(driver, ServiceName);
      ServiceRefresh(driver, ServiceId);
      ServiceGroupCollapseButton(driver, GroupName);
    }

    internal static void CheckExpandCollapseButton(IWebDriver driver)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("collapseExpand")));
      driver.FindElement(By.Id("collapseExpand")).Click();
      Thread.Sleep(1500);
      wait.Until(drv => drv.TryFindElement(By.Id("collapseExpand")));
      driver.FindElement(By.Id("collapseExpand")).Click();
    }

    internal static void Search(IWebDriver driver, string service)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("serviceSearch")));
      driver.FindElement(By.Id("serviceSearch")).SendKeys(service);
    }

    internal static void QuarterFilter(IWebDriver driver)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("quarterSelect")));
      driver.FindElement(By.Id("quarterSelect")).Click();
      SelectClick(driver, "quarterSelect");
    }

    internal static void GroupBy(IWebDriver driver)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("groupBySelect")));
      driver.FindElement(By.Id("groupBySelect")).Click();
      SelectClick(driver, "groupBySelect");
    }

    internal static void ServiceModal(IWebDriver driver, string service)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id(service)));
      driver.FindElement(By.Id(service)).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("Modal" + service)));
      driver.FindElement(By.CssSelector("[title='Close Modal']")).Click();
    }

    internal static void StampModal(IWebDriver driver, string stampId, string stampName)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id(stampName + stampId)));
      driver.FindElement(By.Id(stampName + stampId)).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("stampModal" + stampId)));
      driver.FindElement(By.CssSelector("[title='Close Modal']")).Click();
    }

    internal static void ServiceGroupCollapseButton(IWebDriver driver, string group)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id(group + "CollapseButton")));
      driver.FindElement(By.Id(group + "CollapseButton")).Click();
    }

    internal static void ServiceRefresh(IWebDriver driver, string serviceId)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("refreshServiceButton" + serviceId)));
      driver.FindElement(By.Id("refreshServiceButton" + serviceId)).Click();
    }

    private static void SelectClick(IWebDriver driver, string selectName)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

      var element = driver.FindElement(By.Id(selectName));
      wait.Until(element => element.TryFindElement(By.Id(selectName + "Select-List")));

      var item = element.FindElement(By.Id(selectName + "Select-List"));

      item.FindElement(By.CssSelector(selectName == "groupBySelect" ? "[id*='option-1']" : "[id*='option-0']")).Click();
    }
  }
}
