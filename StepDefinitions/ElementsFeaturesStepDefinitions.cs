using BoDi;
using NUnit.Framework;
using SpecflowOctoberBatchPOM.Drivers;
using SpecflowOctoberBatchPOM.Pages;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TechTalk.SpecFlow.Infrastructure;
using static SpecflowOctoberBatchPOM.EnumExtensions.EnumExtensions;

namespace SpecflowOctoberBatchPOM.StepDefinitions
{
    [Binding]
    public class ElementsFeaturesStepDefinitions
    {
        Homepage _homepage;
        public ElementsFeaturesStepDefinitions(IObjectContainer objectContainer)
        {
            _homepage = objectContainer.Resolve<Homepage>();    
        }

        [Given(@"I navigate to demoQa page")]
        public void GivenINavigateToDemoQaPage()
        {
            _homepage.NavigateToDemoQASite();
        }

        [When(@"I click Elements")]
        public void WhenIClickElements()
        {
            _homepage.ClickElements();
        }

        [When(@"I click Text Box")]
        public void WhenIClickTextBox()
        {
            _homepage.ClickTextbox();
        }

        [When(@"I enter the following data")]
        public void WhenIEnterTheFollowingData(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            _homepage.EnterContactDetails
                (data.FullName, data.Email, data.CurrentAddress,
                data.ParmanentAddress);
        }

        [When(@"I click submit button")]
        public void WhenIClickSubmitButton()
        {
           _homepage.ClicksubmitBtn();
        }

        [Then(@"following data has been added")]
        public void ThenFollowingDataHasBeenAdded(Table table)
        {
            //dynamic expected = table.CreateDynamicInstance();
            var expected = table.CreateInstance<tabledata>();
            var expected2 = table.CreateInstance<(string FullName, string Email,
                string CurrentAddress, string ParmanentAddress)>();

            //var expected = table.Rows
            //    .ToDictionary(key => key.Keys, value => value.Values);

            //var expected = table.Rows
            //    .ToDictionary(key => key.Keys.FirstOrDefault(), 
            //    value => value.Values.FirstOrDefault());

            //var expected = GetDictionaryValues(table);
            var actual = _homepage.getelementsValue();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expected.FullName,
                    actual?.FirstOrDefault()?.Text.Split(":")[(int)IntValue.One]);
                Assert.AreEqual(expected.Email,
                    actual?.ElementAtOrDefault(
                        (int)IntValue.One)?.Text.Split(":")[
                            (int)IntValue.One]);
                Assert.AreEqual(expected.CurrentAddress,
                    actual?.ElementAtOrDefault(
                        (int)IntValue.Two)?.Text.Split(":")[(int)IntValue.One]);
                Assert.AreEqual(expected.ParmanentAddress,
                    actual?.ElementAtOrDefault(
                        (int)IntValue.Three)?.Text.Split(":")[(int)IntValue.One]);
            });

    

            Dictionary<string, string> GetDictionaryValues(Table table)
            {
                var DictionaryData = new Dictionary<string, string>();
                foreach (var data in table.Rows)
                {
                    DictionaryData.Add(data.FirstOrDefault().Key,
                        data.FirstOrDefault().Value);
                    DictionaryData.Add(data.ElementAtOrDefault(1).Key,
                        data.ElementAtOrDefault(1).Value);
                    DictionaryData.Add(data.ElementAtOrDefault(2).Key,
                        data.ElementAtOrDefault(2).Value);
                    DictionaryData.Add(data.LastOrDefault().Key,
                        data.LastOrDefault().Value);
                }
                return DictionaryData;
            }
        }
        public class tabledata
        {
            public string? FullName { get; set; }
            public string? Email { get; set; }
            public string? CurrentAddress { get; set; }
            public string? ParmanentAddress { get; set; }
        }
    }
}
