using NUnit.Framework;
using WatiN.Core;
using System.Threading;

using System;

namespace Tests
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class Tests
    {
        private IE _driver;
        private const string URL = "https://www.google.com/";

        private Uri URI = new Uri(URL);

        private const string SEARCH_FIELD_CSS = "input[class = 'gLFyf gsfi']";

        private const string SEARCH_BUTTON_CSS = "div[class = 'FPdoLc tfB0Bf'] input[class = 'gNO89b']";

        private const string WIKI_RESULT_CSS = "div[class = 'srg'] a[href $= 'wiki/XPath']";

        private const string CONTEXT_LIST_CSS = "div[id= 'toc']>ul a";

        [OneTimeSetUp]
        public void Setup()
        {
            _driver = new IE();
        }

        [Test]
        public void Test1()
        {
            _driver.NativeBrowser.NavigateTo(URI);
            WaitUntilDriverIsFree();

            _driver.TextField(Find.BySelector(SEARCH_FIELD_CSS)).TypeText("xpath");
            WaitUntilDriverIsFree();

            _driver.Element(Find.BySelector(SEARCH_BUTTON_CSS)).Click();
            WaitUntilDriverIsFree();

            _driver.Element(Find.BySelector(WIKI_RESULT_CSS)).Click();
            WaitUntilDriverIsFree();

            var allContextElements = _driver.Links.Filter(Find.BySelector(CONTEXT_LIST_CSS));

            var contextListLength = allContextElements.Count;
            Assert.IsTrue(contextListLength == 36, $"Number of elements in context should be 36, but it is {contextListLength}");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Close();
        }

        private void WaitUntilDriverIsFree()
        {
            while (((SHDocVw.InternetExplorerClass)(_driver.InternetExplorer)).Busy)
            {
                Thread.Sleep(2000);
            }
        }
    }
}