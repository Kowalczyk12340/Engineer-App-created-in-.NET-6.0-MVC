using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using U4.DevOps.Dashboard.UnitTests.UITests.Helpers;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace U4.DevOps.Dashboard.UnitTests.UITests
{
  public class FunctionalityTests
  {
    private static string categoryName = "CategoryTest";
    private static string commodityName = "CommodityTest";
    private static string filterName = "chwytak";
    private static string employeeName = "EmployeeTest";
    private static string employeeNumber = "693 456 232";
    private static string employeeEmail = "marcinkowalczyk24.7@gmail.com";
    private static string appName = "Aplikacja Dropshipping";
    private static int categoryOrder = 99;
    private static int commodityPrice = 243;
    private static string commodityDesc = "Desc Desc Desc Test";

    public TestContext TestContext { get; set; }

    [SetUp]
    public void PrepareSmokeTests()
    {
      try
      {
        new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
        using IWebDriver driver = new ChromeDriver();
        LoginHelper.LoginToApplication(driver, TestContext);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Wystąpił błąd zbyt długiego łączenia z localhostem: {ex.Message}");
      }
    }

    [Test]
    public void ApplicationTests()
    {
      var ChromeService = ChromeDriverService.CreateDefaultService();

      using IWebDriver driver = new ChromeDriver(ChromeService, new ChromeOptions { }, TimeSpan.FromSeconds(15));
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
      LoginHelper.LoginToApplication(driver, TestContext);

      ApplicationCommodityTests(driver);

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      Thread.Sleep(500);

      ApplicationDeliveryTests(driver);

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      Thread.Sleep(500);

      ApplicationEmployeeTests(driver);

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      Thread.Sleep(500);

      ApplicationOrderTests(driver);

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      Thread.Sleep(500);

      ApplicationUsersTests(driver);

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      Thread.Sleep(500);

      ApplicationOfferTests(driver);

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      Thread.Sleep(500);

      ApplicationOfferFilterTests(driver);

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      Thread.Sleep(500);

      ApplicationCategoryTests(driver);

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("goHome_")));

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      Thread.Sleep(500);
    }

    internal static void ApplicationCategoryTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("navbarDropDown")));
      driver.FindElement(By.Id("navbarDropDown")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("categoryItem_")));
      driver.FindElement(By.Id("categoryItem_")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("createCategory_")));
      driver.FindElement(By.Id("createCategory_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("categoryName_")));
      driver.FindElement(By.Id("categoryName_")).SendKeys(categoryName);

      wait.Until(drv => drv.TryFindElement(By.Id("categoryName_")));
      driver.FindElement(By.Id("categoryOrder_")).SendKeys(categoryOrder.ToString());

      wait.Until(drv => drv.TryFindElement(By.Id("goHome_")));
      driver.FindElement(By.Id("goHome_")).Click();
    }

    internal static void ApplicationOrderTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("orderItem_")));
      driver.FindElement(By.Id("orderItem_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("pending_")));
      driver.FindElement(By.Id("pending_")).Click();

      Thread.Sleep(500);

      wait.Until(drv => drv.TryFindElement(By.Id("approved_")));
      driver.FindElement(By.Id("approved_")).Click();

      Thread.Sleep(500);

      wait.Until(drv => drv.TryFindElement(By.Id("rejected_")));
      driver.FindElement(By.Id("rejected_")).Click();

      Thread.Sleep(500);

      wait.Until(drv => drv.TryFindElement(By.Id("all_")));
      driver.FindElement(By.Id("all_")).Click();

      Thread.Sleep(500);

      wait.Until(drv => drv.TryFindElement(By.Id("orderDetails_")));
      driver.FindElement(By.Id("orderDetails_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("backToOrders_")));
      driver.FindElement(By.Id("backToOrders_")).Click();
    }

    internal static void ApplicationUsersTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("userItem_")));
      driver.FindElement(By.Id("userItem_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("lock_")));
      driver.FindElement(By.Id("lock_")).Click();

      Thread.Sleep(2000);

      wait.Until(drv => drv.TryFindElement(By.Id("unlock_")));
      driver.FindElement(By.Id("unlock_")).Click();
    }

    internal static void ApplicationOfferTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

      wait.Until(drv => drv.TryFindElement(By.Id("offer_")));
      driver.FindElement(By.Id("offer_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("offerCommodity_")));
      driver.FindElement(By.Id("offerCommodity_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("backToOffer_")));
      driver.FindElement(By.Id("backToOffer_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("offerService_")));
      driver.FindElement(By.Id("offerService_")).Click();
    }

    internal static void ApplicationOfferFilterTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

      wait.Until(drv => drv.TryFindElement(By.Id("offer_")));
      driver.FindElement(By.Id("offer_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("offerCommodity_")));
      driver.FindElement(By.Id("offerCommodity_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("searchString")));
      driver.FindElement(By.Id("searchString")).SendKeys(filterName);

      wait.Until(drv => drv.TryFindElement(By.Id("searchButton_")));
      driver.FindElement(By.Id("searchButton_")).Click();

      Thread.Sleep(500);

      wait.Until(drv => drv.TryFindElement(By.Id("returnOffer_")));
      driver.FindElement(By.Id("returnOffer_")).Click();
    }

    internal static void ApplicationCommodityTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("navbarDropDown")));
      driver.FindElement(By.Id("navbarDropDown")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("commodityItem_")));
      driver.FindElement(By.Id("commodityItem_")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("createCommodity_")));
      driver.FindElement(By.Id("createCommodity_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("commodityName_")));
      driver.FindElement(By.Id("commodityName_")).SendKeys(commodityName);

      wait.Until(drv => drv.TryFindElement(By.Id("commodityPrice_")));
      driver.FindElement(By.Id("commodityPrice_")).SendKeys(commodityPrice.ToString());

      wait.Until(drv => drv.TryFindElement(By.Id("commodityDesc_")));
      driver.FindElement(By.Id("commodityDesc_")).SendKeys(commodityDesc);
    }

    internal static void ApplicationDeliveryTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("navbarDropDown")));
      driver.FindElement(By.Id("navbarDropDown")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("deliveryItem_")));
      driver.FindElement(By.Id("deliveryItem_")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("createDelivery_")));
      driver.FindElement(By.Id("createDelivery_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("deliveryName_")));
      driver.FindElement(By.Id("deliveryName_")).SendKeys(commodityName);

      wait.Until(drv => drv.TryFindElement(By.Id("deliveryDesc_")));
      driver.FindElement(By.Id("deliveryDesc_")).SendKeys(commodityDesc);
    }

    internal static void ApplicationEmployeeTests(IWebDriver driver)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("navbarDropDown")));
      driver.FindElement(By.Id("navbarDropDown")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("employeeItem_")));
      driver.FindElement(By.Id("employeeItem_")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("createEmployee_")));
      driver.FindElement(By.Id("createEmployee_")).Click();

      wait.Until(drv => drv.TryFindElement(By.Id("employeeName_")));
      driver.FindElement(By.Id("employeeName_")).SendKeys(employeeName);

      wait.Until(drv => drv.TryFindElement(By.Id("employeeNumber_")));
      driver.FindElement(By.Id("employeeNumber_")).SendKeys(employeeNumber);

      wait.Until(drv => drv.TryFindElement(By.Id("employeeEmail_")));
      driver.FindElement(By.Id("employeeEmail_")).SendKeys(employeeEmail);

      wait.Until(drv => drv.TryFindElement(By.Id("employeeDesc_")));
      driver.FindElement(By.Id("employeeDesc_")).SendKeys(commodityDesc);
    }

    internal static void CreateNewService(IWebDriver driver, string serviceName)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

      driver.FindElement(By.Id("serviceType")).Click();

      SelectClick(driver, "serviceType");

      driver.FindElement(By.Id("engineeringAreaId")).Click();

      SelectClick(driver, "engineeringAreaId");

      driver.FindElement(By.CssSelector("[for='plannedForNextQuarterDevelopment']")).Click();

      driver.FindElement(By.Id("masterBranchLink")).SendKeys("https://www.google.com");

      driver.FindElement(By.CssSelector("[for='ciPipelineAsPartOfCD']")).Click();

      driver.FindElement(By.Id("nonProdSupport")).SendKeys("Non prod support");

      //Pipelines
      driver.FindElement(By.Id("addPipeline")).Click();

      driver.FindElement(By.Id("pipelines[0].pipelineUri")).SendKeys("https://www.google.com/unit4-global/U4ERP/_test?definitionId=375");

      driver.FindElement(By.CssSelector("[for='pipelines[0].isUnifiedPipeline']")).Click();

      //Component definitions
      driver.FindElement(By.Id("addComponent")).Click();

      driver.FindElement(By.Id("componentDefinitions[0].name")).SendKeys("Component name");

      driver.FindElement(By.Id("componentDefinitions[0].type")).Click();

      SelectClick(driver, "componentDefinitions[0].type");

      //SelectClick(driver, "opsServiceManager");

      //driver.FindElement(By.CssSelector("[aria-label='Project Manager']")).SendKeys(projectManager);

      SelectClick(driver, "productManager");

      driver.FindElement(By.Id("saveService")).Click();
    }

    private static void SelectClick(IWebDriver driver, string selectName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

      var element = driver.FindElement(By.Id(selectName));
      wait.Until(element => element.TryFindElement(By.Id(selectName)));

      var item = element.FindElement(By.Id(selectName));
      item.FindElement(By.CssSelector("[id*='option-1']")).Click();
    }
  }
}