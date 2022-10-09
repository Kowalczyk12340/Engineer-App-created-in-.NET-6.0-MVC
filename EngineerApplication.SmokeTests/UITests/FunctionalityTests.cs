using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using U4.DevOps.Dashboard.UnitTests.UITests.Helpers;
using SeleniumExtras.WaitHelpers;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;

namespace U4.DevOps.Dashboard.UnitTests.UITests
{
  public class FunctionalityTests
  {
    private static string categoryName = "CategoryTest";
    private static string commodityName = "CommodityTest";
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
        DeleteService(driver, categoryName);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error occured: {ex.Message}");
      }
    }

    [Test]
    public void ApplicationTests()
    {
      var ChromeService = ChromeDriverService.CreateDefaultService();

      using IWebDriver driver = new ChromeDriver(ChromeService, new ChromeOptions { }, TimeSpan.FromSeconds(5));
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
      LoginHelper.LoginToApplication(driver, TestContext);

      ApplicationCategoryTests(driver);

      wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("goHome_")));

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");

      ApplicationCommodityTests(driver);

      Assert.IsTrue(driver.PageSource.Contains(appName), $"App {appName} should be visible on screen after being created.");
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

    internal static void CreateNewService(IWebDriver driver, string serviceName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("topbar__toolbar")));
      driver.FindElement(By.Id("topbar__toolbar")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("manageServices")));
      driver.FindElement(By.Id("manageServices")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-service-button")));
      driver.FindElement(By.Id("add-service-button")).Click();

      driver.FindElement(By.Id("name")).SendKeys(serviceName);

      driver.FindElement(By.Id("projectId")).Click();

      SelectClick(driver, "projectId");

      driver.FindElement(By.Id("serviceType")).Click();

      SelectClick(driver, "serviceType");

      driver.FindElement(By.Id("engineeringAreaId")).Click();

      SelectClick(driver, "engineeringAreaId");

      //Quarter Scope
      driver.FindElement(By.CssSelector("[for='isCurrentDevelopment']")).Click();

      driver.FindElement(By.CssSelector("[for='isCurrentDevelopmentCompleted']")).Click();

      driver.FindElement(By.CssSelector("[for='plannedForNextQuarterDevelopment']")).Click();

      //Technical Details

      driver.FindElement(By.Id("branchingStrategy")).Click();

      SelectClick(driver, "branchingStrategy");

      driver.FindElement(By.Id("masterBranchLink")).SendKeys("https://www.google.com");

      driver.FindElement(By.CssSelector("[for='ciPipelineAsPartOfCD']")).Click();

      driver.FindElement(By.Id("nonProdSupport")).SendKeys("Non prod support");

      driver.FindElement(By.Id("prodSupport")).SendKeys("Prod support");

      //Pipelines
      driver.FindElement(By.Id("addPipeline")).Click();

      driver.FindElement(By.Id("pipelines[0].pipelineName")).SendKeys("TestPipeline");

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

      wait.Until(drv => drv.TryFindElement(By.Id("add-service-button")));
      driver.FindElement(By.Id("goToDashboard")).Click();

    }

    internal static void EditServiceCriteria(IWebDriver driver, string serviceName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("topbar__toolbar")));
      driver.FindElement(By.Id("topbar__toolbar")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("manageServices")));
      driver.FindElement(By.Id("manageServices")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-service-button")));

      var element = driver.FindElements(By.ClassName("table__row")).First(x => x.Text.Contains(serviceName));
      element.FindElement(By.CssSelector("[id*='edit-criterias']")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("definedCriteria[0].value")));

      driver.FindElement(By.Id("definedCriteria[0].value")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("definedCriteria[1].value")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("definedCriteria[2].value")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("definedCriteria[3].value")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("definedCriteria[4].value")).SendKeys("https://www.google.com");
      driver.FindElement(By.Id("definedCriteria[5].value")).SendKeys("https://www.google.com");

      driver.FindElement(By.CssSelector("[for='definedCriteria[6].value']")).Click();
      driver.FindElement(By.CssSelector("[for='definedCriteria[7].value']")).Click();
      driver.FindElement(By.CssSelector("[for='definedCriteria[8].value']")).Click();
      driver.FindElement(By.CssSelector("[for='definedCriteria[9].value']")).Click();
      driver.FindElement(By.CssSelector("[for='definedCriteria[10].value']")).Click();
      driver.FindElement(By.CssSelector("[for='definedCriteria[11].value']")).Click();
      driver.FindElement(By.CssSelector("[for='definedCriteria[12].value']")).Click();
      driver.FindElement(By.CssSelector("[for='definedCriteria[13].value']")).Click();
      driver.FindElement(By.CssSelector("[for='definedCriteria[14].value']")).Click();

      driver.FindElement(By.Id("saveCriteria")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-service-button")));
      driver.FindElement(By.Id("goToDashboard")).Click();

    }
    internal static void DeleteService(IWebDriver driver, string serviceName)
    {
      WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
      wait.Until(drv => drv.TryFindElement(By.Id("topbar__toolbar")));
      driver.FindElement(By.Id("topbar__toolbar")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("manageServices")));
      driver.FindElement(By.Id("manageServices")).Click();
      wait.Until(drv => drv.TryFindElement(By.Id("add-service-button")));

      var element = driver.FindElements(By.ClassName("table__row")).First(x => x.Text.Contains(serviceName));
      if (element != null)
      {
        element.FindElement(By.Id("deleteService")).Click();
        driver.FindElement(By.Id("deleteConfirm")).Click();
      }

      Thread.Sleep(1000);

      wait.Until(drv => drv.ElementDoesNotExists(By.Id("deleteConfirm")));
      wait.Until(drv => drv.TryFindElement(By.Id("goToDashboard")));
      driver.FindElement(By.Id("goToDashboard")).Click();
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