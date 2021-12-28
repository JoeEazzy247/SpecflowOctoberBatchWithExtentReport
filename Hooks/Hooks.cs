using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecflowOctoberBatchPOM.Drivers;
using SpecflowOctoberBatchPOM.Extensions;
using SpecflowTestOctoberBatch.Extensions;
using System.Diagnostics;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecflowOctoberBatchPOM.Hooks
{
    [Binding]
    public sealed class Hooks 
    {
        private static ScenarioContext? _scenarioContext;
        private static ExtentReports? _extentReport;
        private static ExtentHtmlReporter? _extentHtmlReporter;
        private static ExtentTest? _feature;
        private static ExtentTest? _scenario;

        public IWebDriver? Driver;
        private readonly IContextManager objectContainer;

        public Hooks(IContextManager _objectContainer)
        {
            objectContainer = _objectContainer;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _extentHtmlReporter =
                new ExtentHtmlReporter(
                CustomExtensions.GetCurrentDirectory());
            _extentReport = new ExtentReports();
            _extentReport.AttachReporter(_extentHtmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            if (null != featureContext)
            {
                _feature = _extentReport?.CreateTest<Feature>(featureContext.FeatureInfo.Title,
                    featureContext.FeatureInfo.Description);
            }
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            var options = new ChromeOptions();
            options
                .AddArguments("start-maximized",
                "incognito");
            Driver = new ChromeDriver(options);
            objectContainer.ScenarioContext
                .ScenarioContainer?.RegisterInstanceAs(Driver);
            if (null != scenarioContext)
            {
                _scenarioContext = scenarioContext;
                _scenario = _feature?.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title,
                    scenarioContext.ScenarioInfo.Description);
            }
        }

        [AfterStep]
        public void AfterStep()
        {
            ExtentTest? stepNode = null;
            //Method to add take screen shot when test fail
            var mediaEntity =
                Driver?.CaptureScreenShotAndReturnModel(_scenarioContext?.ScenarioInfo.Title.Trim());

            stepNode = _scenario?.CreateNode(
                new GherkinKeyword(_scenarioContext?.StepContext.StepInfo.StepDefinitionType.ToString()),
                 _scenarioContext?.StepContext.StepInfo.Text);

            //Add tables to report
            if (_scenarioContext?.StepContext.StepInfo.Table != null)
            {
                stepNode?.Log(Status.Info, $"{string.Join("|", _scenarioContext.StepContext.StepInfo.Table.Header)}");
                foreach (var row in _scenarioContext.StepContext.StepInfo.Table.Rows)
                {
                    stepNode?.Log(Status.Info, string.Join("|", row.Values));
                }
            }

            
            //Conditional statement to check what error was thrown then state the error in report
            if (_scenarioContext?.ScenarioExecutionStatus != ScenarioExecutionStatus.OK)
            {
                List<ScenarioExecutionStatus> failTypes = new List<ScenarioExecutionStatus>()
                {
                    ScenarioExecutionStatus.BindingError,
                    ScenarioExecutionStatus.TestError,
                    ScenarioExecutionStatus.UndefinedStep,
                    ScenarioExecutionStatus.StepDefinitionPending
                };

                if (failTypes.Any())
                {
                    stepNode?.Fail("This step failed", mediaEntity);
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            waitExtension.wait(2000);
            Driver?.Quit();
            using (var process = Process.GetCurrentProcess())
            {
                if (process.ToString() == "chromedriver")
                {
                    process.Kill();
                }
                else if (process.ToString() == "geckodriver")
                {
                    process.Kill();
                }
                Driver?.Dispose(); Driver = null;
            }
        }

        [AfterTestRun]
        public static void AftertestRun()
        {
            //Flushes to ensure the report is printed 
            _extentReport?.Flush();
        }
    }
}