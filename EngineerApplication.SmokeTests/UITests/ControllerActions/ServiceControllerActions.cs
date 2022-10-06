using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions;

namespace U4.DevOps.Dashboard.UnitTests.UITests.ControllerActions
{
  internal static class ServiceControllerActions
  {
    private static string devManager = "Grzegorz Sobański";
    private static string devTechLead = "Michał Smyk";
    private static string opsManager = "Piotr Lewicki";
    private static string opsEngineer = "Michał Smyk";
    private static string projectManager = "Grzegorz Sobański";

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

      //Ownership
      driver.FindElement(By.CssSelector("[aria-label='Dev - Engineering Manager']")).SendKeys(devManager);

      SelectClick(driver, "devEngineeringManager");

      driver.FindElement(By.CssSelector("[aria-label='Dev - Technical lead']")).SendKeys(devTechLead);

      SelectClick(driver, "devTechnicalLead");

      driver.FindElement(By.CssSelector("[aria-label='Ops - Engineer']")).SendKeys(opsEngineer);

      SelectClick(driver, "opsEngineer");

      driver.FindElement(By.CssSelector("[aria-label='Ops - Manager']")).SendKeys(opsManager);

      SelectClick(driver, "opsServiceManager");

      driver.FindElement(By.CssSelector("[aria-label='Project Manager']")).SendKeys(projectManager);

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
      wait.Until(element => element.TryFindElement(By.Id(selectName + "Select-List")));

      var item = element.FindElement(By.Id(selectName + "Select-List"));

      item.FindElement(By.CssSelector("[id*='option-0']")).Click();
    }

  }
}
