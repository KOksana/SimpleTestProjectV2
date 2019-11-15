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

        //private const string SEARCH_FIELD_XPATH = "//input[@class = 'gLFyf gsfi']";
        private const string SEARCH_FIELD_CSS = "input[class = 'gLFyf gsfi']";

       // private const string SEARCH_BUTTON_XPATH = "//div[@class = 'VlcLAe']//input[@class = 'gNO89b']";
        private const string SEARCH_BUTTON_CSS = "div[class = 'FPdoLc tfB0Bf'] input[class = 'gNO89b']";

       // private const string WIKI_RESULT_XPATH = "//h3/span[text()='XPath - Wikipedia']/../..";
        private const string WIKI_RESULT_CSS = "div[class = 'srg'] a[href $= 'wiki/XPath']";

        //private const string CONTEXT_LIST_XPATH = "//div[@id= 'toc']/ul//a";
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

            //var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 5));
            //wait.Until(drv => drv.FindElement(By.XPath(SEARCH_FIELD_XPATH)));

           // _driver.FindElement(By.CssSelector(SEARCH_FIELD_CSS)).SendKeys("xpath");

            _driver.TextField(Find.BySelector(SEARCH_FIELD_CSS)).TypeText("xpath");

            //wait.Until(d => _driver.FindElement(By.CssSelector(SEARCH_BUTTON_CSS)).Displayed);

            //var searchButton = _driver.FindElement(By.CssSelector(SEARCH_BUTTON_CSS));
            //var actions = new Actions(_driver);
            //actions.MoveToElement(searchButton).Build().Perform();

            //_driver.FindElement(By.XPath(SEARCH_BUTTON_XPATH)).Click();
            _driver.Element(Find.BySelector(SEARCH_BUTTON_CSS)).Click();

            ////wait.Until(drv => drv.FindElement(By.XPath(WIKI_RESULT_XPATH)));

            //_driver.FindElements(By.CssSelector(WIKI_RESULT_CSS))[0].Click();

            _driver.Element(Find.BySelector(WIKI_RESULT_CSS)).Click();

            ////wait.Until(drv => drv.FindElement(By.ClassName("toctitle")));

            //var allContextElements = _driver.FindElements(By.CssSelector(CONTEXT_LIST_CSS));

            //var contextListLength = allContextElements.Count;
            //Assert.IsTrue(contextListLength == 36, $"Number of elements in context should be 36, but it is {contextListLength}");
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Close();
        }
    }
}