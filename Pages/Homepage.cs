using NUnit.Framework;
using OpenQA.Selenium;
using SpecflowOctoberBatchPOM.Drivers;
using SpecflowOctoberBatchPOM.Extensions;
using SpecflowTestOctoberBatch.Extensions;

namespace SpecflowOctoberBatchPOM.Pages
{
    public class Homepage 
    {
        private readonly IWebDriver Driver;
        public Homepage(IWebDriver _driver)
        {
            this.Driver = _driver;
        }

        private static string? url => TestContext.Parameters["DemoQaurl"];

        private IWebElement? element => Driver?.FindthisElement(
                By.XPath(
                    "//div[contains(@class, 'card mt-')][.='Elements']")).single;

        private IWebElement? TextBox =>
                Driver?.FindthisElement(
                    By.XPath("//li[@id='item-0'][.='Text Box']")).single;

        private IList<IWebElement>? Output =>
                Driver?.FindthisElement(By.XPath("//div[@id='output']//p")).multiple;


        private IWebElement? FullName =>
                Driver?.FindElement(By.Id("userName"));

        private IWebElement? Email =>
                Driver?.FindElement(By.Id("userEmail"));

        private IWebElement? CAddress =>
                Driver?.FindthisElement(By.Id("currentAddress")).single;

        private IWebElement? PAddress =>
                Driver?.FindthisElement(By.Id("permanentAddress")).single;

        private IWebElement? submitbtn =>
            Driver?.FindthisElement(By.Id("submit")).single;

        public void NavigateToDemoQASite() =>
            Driver?.Navigate()
            .GoToUrl(url);

        public void ClickElements()=>
            element?.Click();

        public void ClickTextbox() =>
            TextBox?.Click();


        public void EnterContactDetails(string fullName, 
            string email,string cAddress, string pAddress)
        {
            FullName?.EnterText(fullName);
            Email?.EnterText(email);
            CAddress?.EnterText(cAddress);
            PAddress?.EnterText(pAddress);
        }

        public void ClicksubmitBtn() => 
            submitbtn?.ClickViaJs(Driver);

        public IList<IWebElement>? getelementsValue()=>
            Output?.ToList();
    }
}
