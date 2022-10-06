using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;

namespace U4.DevOps.Dashboard.UnitTests.UITests.ViewController
{
  internal static class DevelopmentStatusController
  {
    private static string ServiceName = "Invoice Recognition";
    private static string ServiceId = "44";
    private static string GroupName = "ERPx Apps";

    internal static void RunDevelopmentStatusTabTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("refreshDataButton")));
      driver.FindElement(By.CssSelector("[title='Development Status']")).Click();

      CheckExpandCollapseButton(driver);
      QuarterFilter(driver);
      GroupBy(driver);
      ServiceModal(driver, ServiceName);
      ServiceRefresh(driver, ServiceId);
      ServiceGroupCollapseButton(driver, GroupName);
    }

    internal static void CheckExpandCollapseButton(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("collapseExpand")));
      driver.FindElement(By.Id("collapseExpand")).Click();
      Thread.Sleep(1500);
      wait.Until(drv => drv.TryFindElement(By.Id("collapseExpand")));
      driver.FindElement(By.Id("collapseExpand")).Click();
    }

    internal static void QuarterFilter(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("quarterSelect")));
      driver.FindElement(By.Id("quarterSelect")).Click();
      SelectClick(driver, "quarterSelect");
    }

    internal static void GroupBy(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("groupBySelect")));
      driver.FindElement(By.Id("groupBySelect")).Click();
      SelectClick(driver, "groupBySelect");
    }

    internal static void ServiceModal(IWebDriver driver, string service)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id(service)));
      driver.FindElement(By.Id(service)).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("Modal" + service)));
      driver.FindElement(By.CssSelector("[title='Close Modal']")).Click();
    }

    internal static void ServiceGroupCollapseButton(IWebDriver driver, string group)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id(group + "CollapseButton")));
      driver.FindElement(By.Id(group + "CollapseButton")).Click();
    }

    internal static void ServiceRefresh(IWebDriver driver, string serviceId)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("refreshServiceButton" + serviceId)));
      driver.FindElement(By.Id("refreshServiceButton" + serviceId)).Click();
    }

    private static void SelectClick(IWebDriver driver, string selectName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

      var element = driver.FindElement(By.Id(selectName));
      wait.Until(element => element.TryFindElement(By.Id(selectName + "Select-List")));

      var item = element.FindElement(By.Id(selectName + "Select-List"));

      item.FindElement(By.CssSelector(selectName == "groupBySelect" ? "[id*='option-1']" : "[id*='option-0']")).Click();
    }
  }
}
