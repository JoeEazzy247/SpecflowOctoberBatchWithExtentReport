using OpenQA.Selenium;
using SpecflowOctoberBatchPOM.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecflowTestOctoberBatch.Extensions
{
    public class CustomExtensions : DriverHelper
    {
        private readonly ScenarioContext scenarioContext;

        public CustomExtensions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        public void AddNumbersToContext(string key, int value)
        {
            scenarioContext?.Add(key, value);
        }

        public int GetNumbersFromContext(string key)
        {
            return scenarioContext.Get<int>(key);
        }

        public static string GetCurrentDirectory()
        {
            return Path.GetFullPath
                (Path.Combine(AppDomain.CurrentDomain.BaseDirectory, 
                "..\\..\\..\\"));
        }
    }
}
