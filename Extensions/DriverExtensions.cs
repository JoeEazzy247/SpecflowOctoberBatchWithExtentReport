using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowTestOctoberBatch.Extensions
{
    public static class DriverExtensions
    {
        public static void ClickElement(this IWebDriver driver,
            IWebElement? element, bool condition)
        {
            ((IJavaScriptExecutor)driver)
                .ExecuteScript($"arguments[0].scrollIntoView(" +
                $"{condition.ToString().ToLower()})", element);
            element?.Click();
        }

        public static void EnterText(this IWebElement element, string value) => 
            element.SendKeys(value);

        public static void ClickViaJs(this IWebElement element, IWebDriver? Driver) =>
           ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
    }
}
