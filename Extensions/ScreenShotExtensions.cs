using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowOctoberBatchPOM.Extensions
{
    public static class ScreenShotExtensions
    {
        public static MediaEntityModelProvider CaptureScreenShotAndReturnModel(this IWebDriver driver, string ScreenShotName)
        {
            var Screenshot = ((ITakesScreenshot)driver)
                .GetScreenshot().AsBase64EncodedString;
            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(Screenshot, ScreenShotName).Build();
        }
    }
}
