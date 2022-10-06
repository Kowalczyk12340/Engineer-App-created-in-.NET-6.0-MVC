using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;

namespace U4.DevOps.Dashboard.SmokeTests.UITests.ControllerActions
{
  internal static class EngineeringAreaControllerAction
  {
    private static string engineeringDirector = "Marcin Kowalczyk";

    internal static void CreateNewEngineeringArea(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("topbar__toolbar")));
      driver.FindElement(By.Id("topbar__toolbar")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("admin")));
      driver.FindElement(By.Id("admin")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("manageEngineeringAreas")));
      driver.FindElement(By.Id("manageEngineeringAreas")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-engineering-button")));
      driver.FindElement(By.Id("add-engineering-button")).Click();

      driver.FindElement(By.CssSelector("[aria-label='Engineering Director']")).SendKeys(engineeringDirector);
      SelectClick(driver, "engineeringDirector");

      driver.FindElement(By.Id("owner")).Click();
      SelectClick(driver, "owner");

      driver.FindElement(By.Id("saveEngineeringArea")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("goToDashboard")));
      driver.FindElement(By.Id("goToDashboard")).Click();
    }

    internal static void DeleteEngineeringArea(IWebDriver driver, string engineeringDirector)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("topbar__toolbar")));
      driver.FindElement(By.Id("topbar__toolbar")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("admin")));
      driver.FindElement(By.Id("admin")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("manageEngineeringAreas")));
      driver.FindElement(By.Id("manageEngineeringAreas")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-engineering-button")));

      var element = driver.FindElements(By.ClassName("table__row")).First(x => x.Text.Contains(engineeringDirector));
      if (element != null)
      {
        element.FindElement(By.Id("deleteEngineeringArea")).Click();
        wait.Until(drv => drv.TryFindElement(By.Id("deleteConfirm")));
        driver.FindElement(By.Id("deleteConfirm")).Click();
      }

      Thread.Sleep(1000);

      wait.Until(drv => drv.ElementDoesNotExists(By.Id("deleteConfirm")));
      wait.Until(drv => drv.TryFindElement(By.Id("goToDashboard")));
      driver.FindElement(By.Id("goToDashboard")).Click();
    }

    private static void SelectClick(IWebDriver driver, string selectName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

      var element = driver.FindElement(By.Id(selectName));
      wait.Until(element => element.TryFindElement(By.Id(selectName + "Select-List")));

      var item = element.FindElement(By.Id(selectName + "Select-List"));

      item.FindElement(By.CssSelector("[id*='option-0']")).Click();
    }
  }
}
