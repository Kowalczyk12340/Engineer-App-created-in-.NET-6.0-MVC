using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Diagnostics;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;

namespace U4.DevOps.Dashboard.UnitTests.UITests.Helpers
{
  public static class LoginHelper
  {
    public static string AppUrl;
    public static string TestAccountLogin;
    public static string TestAccountPassword;

    public static void LoginToApplication(IWebDriver driver, TestContext context)
    {
      if (AppUrl.Contains("localhost"))
      {
        LoginLocalhost(driver);
      }
      else
      {
        LoginTestAccount(driver, false, context);
      }
    }

    private static void LoginLocalhost(IWebDriver driver)
    {
      string loginInputId = "i0116";
      string loginButtonId = "idSIButton9";

      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
      driver.Navigate().GoToUrl(AppUrl);
      wait.Until(drv => drv.TryFindElement(By.Id(loginInputId)));
      driver.FindElement(By.Id(loginInputId)).SendKeys(TestAccountLogin);
      wait.Until(drv => drv.TryFindElement(By.Id(loginButtonId)));
      driver.FindElement(By.Id(loginButtonId)).Click();

      // Put breakpoint here and finish logging in until you'll see main application
      Console.Write("Breakpoint me!");
    }

    private static void LoginTestAccount(IWebDriver driver, bool secondTake, TestContext context)
    {
      if (string.IsNullOrEmpty(TestAccountLogin) || string.IsNullOrEmpty(TestAccountPassword))
      {
        throw new ConfigurationErrorsException("Login/password has not been provided.");
      }

      string loginInputId = "i0116";
      string loginButtonId = "idSIButton9";
      string passwordInputId = "i0118";
      string passwordButtonId = loginButtonId;

      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      driver.Navigate().GoToUrl(AppUrl);
      wait.Until(drv => drv.TryFindElement(By.Id(loginInputId)));
      driver.FindElement(By.Id(loginInputId)).SendKeys(TestAccountLogin);
      wait.Until(drv => drv.TryFindElement(By.Id(loginButtonId)));
      driver.FindElement(By.Id(loginButtonId)).Submit();
      Thread.Sleep(2000);
      wait.Until(drv => drv.TryFindElement(By.Id(passwordInputId)));
      var passwordButton = driver.FindElement(By.Id(passwordInputId));
      passwordButton.SendKeys(TestAccountPassword);
      Thread.Sleep(2000);
      wait.Until(drv => drv.TryFindElement(By.Id(passwordButtonId)).Displayed);
      driver.FindElement(By.Id(passwordButtonId)).Submit();
      Thread.Sleep(3000);
      if (driver.Url != AppUrl && !secondTake)
      {
        Trace.WriteLine("Login process faced some issues - re-running login.");
        Trace.WriteLine(driver.PageSource);
        var screenshotPath = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss.fffff}.png";
        driver.TakeScreenshot().SaveAsFile(screenshotPath);
        LoginTestAccount(driver, true, context);
      }
    }
  }
}