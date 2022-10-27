using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;

namespace U4.DevOps.Dashboard.UnitTests.UITests.Helpers
{
  public static class LoginHelper
  {
    public static string AppUrl = "https://localhost:7215/";
    public static string TestAccountLogin = "marcinkowalczyk24.7@gmail.com";
    public static string TestAccountPassword = "Marcingrafik1#";

    public static void LoginToApplication(IWebDriver driver, TestContext context)
    {
      LoginLocalhost(driver);
    }

    private static void LoginLocalhost(IWebDriver driver)
    {
      var loginInputId = "Input_Email";
      var passwordInputId = "Input_Password";
      var loginButtonId = "_loginButton";

      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
      driver.Navigate().GoToUrl(AppUrl + "Identity/Account/Login");
      wait.Until(drv => drv.TryFindElement(By.Id(loginInputId)));
      driver.FindElement(By.Id(loginInputId)).SendKeys(TestAccountLogin);
      wait.Until(drv => drv.TryFindElement(By.Id(loginInputId)));
      driver.FindElement(By.Id(passwordInputId)).SendKeys(TestAccountPassword);
      wait.Until(drv => drv.TryFindElement(By.Id(loginButtonId)));
      driver.FindElement(By.Id(loginButtonId)).Click();
    }
  }
}