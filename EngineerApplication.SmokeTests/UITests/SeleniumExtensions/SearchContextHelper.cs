using OpenQA.Selenium;

namespace U4.DevOps.Dashboard.UnitTests.UITests.SeleniumExtensions
{
  public static class SearchContextHelper
  {
    public static IWebElement TryFindElement(this ISearchContext searchContext, By by)
    {
      try { return searchContext.FindElement(by); }
      catch (NoSuchElementException) { return null; }
    }

    public static IWebElement ElementDoesNotExists(this ISearchContext searchContext, By by)
    {
      try
      {
        var element = searchContext.FindElement(by);
        return null;
      }
      catch (NoSuchElementException) { return new WebElement(null, "null"); }
    }
  }
}
